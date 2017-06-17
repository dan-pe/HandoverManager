using System.Globalization;
using RadioNetwork;

namespace FileReaders
{
    #region Usings

    using System;
    using System.IO;

    #endregion

    #region Fields

    CultureInfo.

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

        var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ".";
        float number = float.Parse("0.54", culture);

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
                    ThroughputInMbps = float.Parse(networkElements[2], ),
                    BitErrorRate = float.Parse(networkElements[3]),
                    BurstErrorRate = float.Parse(networkElements[4]),
                    PacketLossPercentage = float.Parse(networkElements[5]),
                    DelayInMsec = float.Parse(networkElements[6]),
                    ResponseTimeInMsec = float.Parse(networkElements[7]),
                    JitterInMsec = float.Parse(networkElements[8]),
                    CostInUnitsPerByte = float.Parse(networkElements[8]),
                    SecurityLevel = float.Parse(networkElements[10]),
                }

            };
            return radioNetworkModel;
        }

        #endregion
    }
}
