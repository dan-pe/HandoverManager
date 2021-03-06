﻿using MathTools;
using Profiler;
using RadioNetworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HandoverAlgorithmBase.NovelAlgorithm
{
    public class NovelHandoverAlgorithm : HandoverAlgorithmBase
    {
        /// <summary>
        /// Selected network profile.
        /// </summary>
        private readonly UserProfile _networkProfile;

        /// <summary>
        /// Instantiate Novel Handover Algorithm.
        /// </summary>
        /// <param name="radioNetworksList">
        /// Radio networks list.
        /// </param>
        /// <param name="userProfile">
        /// The user profile.
        /// </param>
        public NovelHandoverAlgorithm(List<RadioNetworkModel> radioNetworksList, UserProfile userProfile) : base(radioNetworksList)
        {
            this.NovelNetworkModels = new List<NovelNetworkModel>();
            this._networkProfile = userProfile;

            foreach (var radioNetworkModel in radioNetworksList)
            {
                var novelNetworkModel = new NovelNetworkModel()
                {
                    GrcFactor = 0.0f,
                    RadioNetworkModel = radioNetworkModel
                };

                this.NovelNetworkModels.Add(novelNetworkModel);
            }
        }

        /// <summary>
        /// List of GRC Factors
        /// </summary>
        public List<float> NetworkGraFactors { get; set; }

        /// <summary>
        /// List of Novel network models.
        /// </summary>
        public List<NovelNetworkModel> NovelNetworkModels { get; set; }

        /// <summary>
        /// The result network.
        /// </summary>
        public RadioNetworkModel ResultNetwork { get; set; }

        /// <summary>
        /// Run Selection.
        /// </summary>
        public override void RunSelection()
        {
            this.CalculateDecisiveFactors();
        }

        /// <summary>
        /// Select the result network
        /// </summary>
        /// <returns></returns>
        public NovelNetworkModel SelectResultNetwork()
        {
            this.RunSelection();
            NovelNetworkModel network;

            try
            {
                network = this.NovelNetworkModels.
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

        /// <summary>
        /// Calculate decisive factors for current networks.
        /// </summary>
        private void CalculateDecisiveFactors()
        {
            var througoutputs = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ThroughputInMbps).ToArray();
            var packtloses = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.PacketLossPercentage).ToArray();
            var responses = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ResponseTimeInMsec).ToArray();
            var sec = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.SecurityLevel).ToArray();

            AhpModel ahpModel = new AhpModel(this._networkProfile.ProfileWeights);

            var ahpWeights = ahpModel.GetOutputWeights();

            foreach (var networkModel in this.NovelNetworkModels)
            {
                double grc =
                    GraTools.NormalizeLargerTheBetter(througoutputs,
                        networkModel.RadioNetworkModel.Parameters.ThroughputInMbps) * ahpWeights[0] +
                    GraTools.NormalizeSmallerTheBetter(packtloses,
                        networkModel.RadioNetworkModel.Parameters.PacketLossPercentage) * ahpWeights[1] +
                    GraTools.NormalizeSmallerTheBetter(responses,
                        networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec) * ahpWeights[2] +
                    GraTools.NormalizeLargerTheBetter(sec,
                        networkModel.RadioNetworkModel.Parameters.SecurityLevel) * ahpWeights[3];

                networkModel.GrcFactor = grc;
            }
        }
    }
}