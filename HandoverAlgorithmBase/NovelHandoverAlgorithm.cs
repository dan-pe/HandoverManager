#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RadioNetwork;
using MathTools;

#endregion

namespace HandoverAlgorithmBase
{
    public class NovelHanoverAlgorithm : HandoverAlgorithmBase
    {
        #region Properties

        public List<float> NetworkGRAFactors { get; set; }

        protected List<NovelNetworkModel> NovelNetworkModels { get; set; }

        public RadioNetwork.RadioNetworkModel ResultNetwork { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiate Novel Handover Algoithm.
        /// </summary>
        /// <param name="radioNetworksList"></param>
        public NovelHanoverAlgorithm(List<RadioNetwork.RadioNetworkModel> radioNetworksList) : base(radioNetworksList)
        {
            NovelNetworkModels = new List<NovelNetworkModel>();

            foreach (var radioNetworkModel in radioNetworksList)
            {
                var novelNetworkModel = new NovelNetworkModel()
                {
                    GRCFactor = 0.0f,
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
        /// Calculate decive factors for current networks.
        /// </summary>
        private void CalculateDecisiveFactors()
        {
            IEnumerable<float> througoutputs = NovelNetworkModels.Select(p => p.RadioNetworkModel.Parameters.ThroughputInMbps);
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
                    GRATools.NormalizeLargerTheBetter(througoutputs,
                        networkModel.RadioNetworkModel.Parameters.ThroughputInMbps) +
                    GRATools.NormalizateSmallerTheBetter(bers,
                        networkModel.RadioNetworkModel.Parameters.BitErrorRate) +
                    GRATools.NormalizateSmallerTheBetter(burs,
                        networkModel.RadioNetworkModel.Parameters.BurstErrorRate) +
                    GRATools.NormalizateSmallerTheBetter(packtloses,
                        networkModel.RadioNetworkModel.Parameters.PacketLossPercentage) +
                    GRATools.NormalizateSmallerTheBetter(delays,
                        networkModel.RadioNetworkModel.Parameters.DelayInMsec) +
                    GRATools.NormalizateSmallerTheBetter(responses,
                        networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec) +
                    GRATools.NormalizateSmallerTheBetter(jitters,
                        networkModel.RadioNetworkModel.Parameters.JitterInMsec) +
                    GRATools.NormalizeLargerTheBetter(sec,
                        networkModel.RadioNetworkModel.Parameters.SecurityLevel) +
                    GRATools.NormalizateSmallerTheBetter(cost,
                        networkModel.RadioNetworkModel.Parameters.CostInUnitsPerByte);

                networkModel.GRCFactor = grc;
            }

        }



        public string SelectResultNetwork()
        {
            RunSelection();
            var netwrok =
                NovelNetworkModels.OrderByDescending(grc => grc.GRCFactor).First();
            return netwrok.RadioNetworkModel.NetworkName;
        }

        #endregion

    }

    public class NovelNetworkModel
    {
        public RadioNetwork.RadioNetworkModel RadioNetworkModel { get; set; }
        public float GRCFactor { get; set; }

    }
  
}

