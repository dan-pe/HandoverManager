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
            var througoutputs = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ThroughputInMbps);
            var bers = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.BitErrorRate);
            var burs = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.BurstErrorRate);
            var packtloses = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.PacketLossPercentage);
            var delays = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.DelayInMsec);
            var responses = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ResponseTimeInMsec);
            var jitters = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.JitterInMsec);
            var sec = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.SecurityLevel);
            var cost = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.CostInUnitsPerByte);

            foreach (var networkModel in NovelNetworkModels)
            {
                float grc =
                    GraTools.NormalizeLargerTheBetter(througoutputs,
                        networkModel.RadioNetworkModel.Parameters.ThroughputInMbps) +
                    GraTools.NormalizateSmallerTheBetter(bers,
                        networkModel.RadioNetworkModel.Parameters.BitErrorRate) +
                    GraTools.NormalizateSmallerTheBetter(burs,
                        networkModel.RadioNetworkModel.Parameters.BurstErrorRate) +
                    GraTools.NormalizateSmallerTheBetter(packtloses,
                        networkModel.RadioNetworkModel.Parameters.PacketLossPercentage) +
                    GraTools.NormalizateSmallerTheBetter(delays,
                        networkModel.RadioNetworkModel.Parameters.DelayInMsec) +
                    GraTools.NormalizateSmallerTheBetter(responses,
                        networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec) +
                    GraTools.NormalizateSmallerTheBetter(jitters,
                        networkModel.RadioNetworkModel.Parameters.JitterInMsec) +
                    GraTools.NormalizeLargerTheBetter(sec,
                        networkModel.RadioNetworkModel.Parameters.SecurityLevel) +
                    GraTools.NormalizateSmallerTheBetter(cost,
                        networkModel.RadioNetworkModel.Parameters.CostInUnitsPerByte);

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

