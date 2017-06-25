using System.Globalization;
using RadioNetwork;

namespace FileReaders
{
    #region Usings

    using System;
    using System.IO;

    #endregion

    public static class CsvReader
    {
        #region Public Methods

        public static void ReadCsvFile(string pathToFile)
        {
            using (var streamReader = new StreamReader(pathToFile))
            {
                var line = streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    Console.WriteLine(ParseCsvLineForNetworkModel(line).NetworkName);
                }

            }
        }

        #endregion

        #region Private Methods

        private static RadioNetworkModel ParseCsvLineForNetworkModel(string line)
        {
            string[] networkElements = line.Split(',');

            var radioNetworkModel = new RadioNetworkModel()
            {
                NetworkName = networkElements[0],
                NetworkType = networkElements[1],
                Parameters = new NetworkParameters()
                {
                    ThroughputInMbps = float.Parse(networkElements[2], CultureInfo.InvariantCulture),
                    BitErrorRate = float.Parse(networkElements[3], CultureInfo.InvariantCulture),
                    BurstErrorRate = float.Parse(networkElements[4], CultureInfo.InvariantCulture),
                    PacketLossPercentage = float.Parse(networkElements[5], CultureInfo.InvariantCulture),
                    DelayInMsec = float.Parse(networkElements[6], CultureInfo.InvariantCulture),
                    ResponseTimeInMsec = float.Parse(networkElements[7], CultureInfo.InvariantCulture),
                    JitterInMsec = float.Parse(networkElements[8], CultureInfo.InvariantCulture),
                    CostInUnitsPerByte = float.Parse(networkElements[8], CultureInfo.InvariantCulture),
                    SecurityLevel = float.Parse(networkElements[10], CultureInfo.InvariantCulture),
                }

            };
            return radioNetworkModel;
        }

        #endregion
    }
}
