#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using MathTools;
using RadioNetworks;

#endregion

namespace HandoverAlgorithmBase.NovelAlgorithm
{
    public class NovelHandoverAlgorithm : HandoverAlgorithmBase
    {
        #region Properties

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
        /// Selected network profile.
        /// </summary>
        private readonly NovelNetworkProfile _networkProfile;


        #endregion

        #region Constructors

        /// <summary>
        /// Instantiate Novel Handover Algorithm.
        /// </summary>
        /// <param name="radioNetworksList">
        /// 
        /// </param>
        public NovelHandoverAlgorithm(List<RadioNetworkModel> radioNetworksList, NovelNetworkProfile novelNetworkProfile) : base(radioNetworksList)
        {
            this.NovelNetworkModels = new List<NovelNetworkModel>();
            this._networkProfile = novelNetworkProfile;

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

        #endregion

        #region Public Methods

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

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculate decisive factors for current networks.
        /// </summary>
        private void CalculateDecisiveFactors()
        {
            var througoutputs = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ThroughputInMbps).ToArray();
            var packtloses = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.PacketLossPercentage).ToArray();
            var delays = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.DelayInMsec).ToArray();
            var responses = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ResponseTimeInMsec).ToArray();
            var sec = this.NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.SecurityLevel).ToArray();



            var coefficients = this.LoadProfile();



            AhpModel ahpModel = new AhpModel(coefficients);

            var ahpWeights = ahpModel.GetOutputWeights();

            foreach (var networkModel in this.NovelNetworkModels)
            {
                double grc =
                    GraTools.NormalizeLargerTheBetter(througoutputs,
                        networkModel.RadioNetworkModel.Parameters.ThroughputInMbps) * ahpWeights[0] +
                    GraTools.NormalizeSmallerTheBetter(packtloses,
                        networkModel.RadioNetworkModel.Parameters.PacketLossPercentage) * ahpWeights[1] +
                    GraTools.NormalizeSmallerTheBetter(delays,
                        networkModel.RadioNetworkModel.Parameters.DelayInMsec) * ahpWeights[2] +
                    GraTools.NormalizeSmallerTheBetter(responses,
                        networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec) * ahpWeights[3] +
                    GraTools.NormalizeLargerTheBetter(sec,
                        networkModel.RadioNetworkModel.Parameters.SecurityLevel) * ahpWeights[4]; 

                networkModel.GrcFactor = grc;
            }
        }

        /// <summary>
        /// Load user profiles from static class.
        /// </summary>
        /// <returns>
        /// Loaded user profile.
        /// </returns>
        private double[,] LoadProfile()
        {
            // TODO: Add logic for real profile loading.

            switch (this._networkProfile)
            {
                case NovelNetworkProfile.BalancedProfile:
                    return NovelNetworkProfiles.GetSomeProfile();

                case NovelNetworkProfile.Connectivity:
                    return NovelNetworkProfiles.GetOtherProfile();

                case NovelNetworkProfile.MaxEfficency:
                    return NovelNetworkProfiles.GetOddProfile();

                default:
                    Logger.Logger.AddMessage("Incorrect profile type", MessageThreshold.WARNING);
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion  
    }
}

