using System.Collections.Generic;
using System.Collections.ObjectModel;
using RadioNetworks;

namespace FileReaders
{
    #region Usings

    using System;
    using System.IO;
    using System.Globalization;

    #endregion

    /// <summary>
    /// The CSV file reader.
    /// </summary>
    public static class CsvReader
    {
        #region Public Methods

        /// <summary>
        /// Reads the specified CSV file and prints
        /// output to console.
        /// </summary>
        /// <param name="pathToFile">
        /// Path to file.
        /// </param>
        public static List<RadioNetworkModel> ReadCsvFile(string pathToFile)
        {
            using (var streamReader = new StreamReader(pathToFile))
            {
                // Skips header line.
                var line = streamReader.ReadLine();

                List<RadioNetworkModel> radioNetworkModels = new List<RadioNetworkModel>();

                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();

                    //TODO: Actually do something with read line.
                    radioNetworkModels.Add(
                    
                        ParseCsvLineForNetworkModel(line));

                }

                return radioNetworkModels;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses CSV file line to Network Model.
        /// </summary>
        /// <param name="line">
        /// Single line.
        /// </param>
        /// <returns>
        /// The Radio Network model.
        /// </returns>
        private static RadioNetworkModel ParseCsvLineForNetworkModel(string line)
        {
            string[] networkElements = line.Split(',');

            var radioNetworkModel = new RadioNetworkModel()
            {
                NetworkName = networkElements[0],
                NetworkType = networkElements[1],
                Parameters = new NetworkParameters()
                {
                    ThroughputInMbps = double.Parse(networkElements[2], CultureInfo.InvariantCulture),
                    BitErrorRate = double.Parse(networkElements[3], CultureInfo.InvariantCulture),
                    BurstErrorRate = double.Parse(networkElements[4], CultureInfo.InvariantCulture),
                    PacketLossPercentage = double.Parse(networkElements[5], CultureInfo.InvariantCulture),
                    DelayInMsec = double.Parse(networkElements[6], CultureInfo.InvariantCulture),
                    ResponseTimeInMsec = double.Parse(networkElements[7], CultureInfo.InvariantCulture),
                    JitterInMsec = double.Parse(networkElements[8], CultureInfo.InvariantCulture),
                    CostInUnitsPerByte = double.Parse(networkElements[8], CultureInfo.InvariantCulture),
                    SecurityLevel = double.Parse(networkElements[10], CultureInfo.InvariantCulture),
                }

            };
            return radioNetworkModel;
        }

        #endregion
    }
}
