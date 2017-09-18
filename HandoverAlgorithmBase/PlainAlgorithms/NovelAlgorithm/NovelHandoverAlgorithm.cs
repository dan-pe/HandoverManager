﻿#region Usings

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

        public List<NovelNetworkModel> NovelNetworkModels { get; set; }

        public RadioNetworkModel ResultNetwork { get; set; }

        private NovelNetworkProfile networkProfile;


        #endregion

        #region Constructors

        /// <summary>
        /// Instantiate Novel Handover Algorithm.
        /// </summary>
        /// <param name="radioNetworksList"></param>
        public NovelHandoverAlgorithm(List<RadioNetworkModel> radioNetworksList, NovelNetworkProfile novelNetworkProfile) : base(radioNetworksList)
        {
            NovelNetworkModels = new List<NovelNetworkModel>();
            this.networkProfile = novelNetworkProfile;

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


            // TODO Just a mock, replace with user input values.
            Random random = new Random();

            double[,] coefficients = new double[9, 9];
            for (int i = 0; i < coefficients.GetLength(1); i++)
            {
                for (int j = 0; j < coefficients.GetLength(1); j++)
                {
                    coefficients[i, j] = random.Next(1, 10);

                }
            }

            coefficients = LoadProfile();

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
        public NovelNetworkModel SelectResultNetwork()
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

            return network;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Load user profiles from static class.
        /// </summary>
        /// <returns>
        /// Loaded user profile.
        /// </returns>
        private double[,] LoadProfile()
        {
            switch (this.networkProfile)
            {
                case NovelNetworkProfile.SomeProfile:
                    return NovelNetworkProfiles.GetSomeProfile();

                case NovelNetworkProfile.OtherProfile:
                    return NovelNetworkProfiles.GetOtherProfile();

                case NovelNetworkProfile.OddProfile:
                    return NovelNetworkProfiles.GetOddProfile();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion  

    }

}

