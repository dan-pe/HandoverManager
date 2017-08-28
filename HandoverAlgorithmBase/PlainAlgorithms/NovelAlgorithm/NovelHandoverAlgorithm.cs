#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using MathTools;
using RadioNetworks;

#endregion

namespace HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm
{
    public class NovelHandoverAlgorithm : HandoverAlgorithmBase
    {
        #region Properties

        public List<float> NetworkGraFactors { get; set; }

        protected List<NovelNetworkModel> NovelNetworkModels { get; set; }

        public RadioNetworkModel ResultNetwork { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiate Novel Handover Algorithm.
        /// </summary>
        /// <param name="radioNetworksList"></param>
        public NovelHandoverAlgorithm(List<RadioNetworkModel> radioNetworksList) : base(radioNetworksList)
        {
            NovelNetworkModels = new List<NovelNetworkModel>();

            foreach (var radioNetworkModel in radioNetworksList)
            {
                var novelNetworkModel = new NovelNetworkModel()
                {
                    GrcFactor = 0.0f,
                    RadioNetworkModel = radioNetworkModel
                };

                NovelNetworkModels.Add(novelNetworkModel);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Run Selection.
        /// </summary>
        public override void RunSelection()
        {
            CalculateDecisiveFactors();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculate decisive factors for current networks.
        /// </summary>
        private void CalculateDecisiveFactors()
        {
            var througoutputs = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ThroughputInMbps).ToArray();
            var bers = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.BitErrorRate).ToArray();
            var burs = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.BurstErrorRate).ToArray();
            var packtloses = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.PacketLossPercentage).ToArray();
            var delays = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.DelayInMsec).ToArray();
            var responses = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ResponseTimeInMsec).ToArray();
            var jitters = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.JitterInMsec).ToArray();
            var sec = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.SecurityLevel).ToArray();
            var cost = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.CostInUnitsPerByte).ToArray();

            // Just a mock, replace with user input values.
            double[,] coefficients = new double[8, througoutputs.Length];
            for (int i = 0; i < througoutputs.Length; i++)
            {
                coefficients[0, i] = througoutputs[i];
                coefficients[0, i] = bers[i];
                coefficients[0, i] = burs[i];
                coefficients[0, i] = packtloses[i];
                coefficients[0, i] = delays[i];
                coefficients[0, i] = responses[i];
                coefficients[0, i] = jitters[i];
                coefficients[0, i] = sec[i];
                coefficients[0, i] = cost[i];
            }

            AhpModel ahpModel = new AhpModel(coefficients);

            var ahpWeights = ahpModel.GetOutputWeights();

            foreach (var networkModel in NovelNetworkModels)
            {
                double grc =
                    GraTools.NormalizeLargerTheBetter(througoutputs,
                        networkModel.RadioNetworkModel.Parameters.ThroughputInMbps) * ahpWeights[0] +
                    GraTools.NormalizateSmallerTheBetter(bers,
                        networkModel.RadioNetworkModel.Parameters.BitErrorRate) * ahpWeights[1] +
                    GraTools.NormalizateSmallerTheBetter(burs,
                        networkModel.RadioNetworkModel.Parameters.BurstErrorRate) * ahpWeights[2] +
                    GraTools.NormalizateSmallerTheBetter(packtloses,
                        networkModel.RadioNetworkModel.Parameters.PacketLossPercentage) * ahpWeights[3] +
                    GraTools.NormalizateSmallerTheBetter(delays,
                        networkModel.RadioNetworkModel.Parameters.DelayInMsec) * ahpWeights[4] +
                    GraTools.NormalizateSmallerTheBetter(responses,
                        networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec) * ahpWeights[5] +
                    GraTools.NormalizateSmallerTheBetter(jitters,
                        networkModel.RadioNetworkModel.Parameters.JitterInMsec) * ahpWeights[6] +
                    GraTools.NormalizeLargerTheBetter(sec,
                        networkModel.RadioNetworkModel.Parameters.SecurityLevel) * ahpWeights[7] +
                    GraTools.NormalizateSmallerTheBetter(cost,
                        networkModel.RadioNetworkModel.Parameters.CostInUnitsPerByte) * ahpWeights[8];

                networkModel.GrcFactor = grc;
            }
        }

        /// <summary>
        /// Select the result network
        /// </summary>
        /// <returns></returns>
        public RadioNetworkModel SelectResultNetwork()
        {
            RunSelection();
            NovelNetworkModel network;

            try
            {
                network = NovelNetworkModels.
                    OrderByDescending(grc => grc.GrcFactor)
                    .First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return network.RadioNetworkModel;
        }

        #endregion

    }

}

