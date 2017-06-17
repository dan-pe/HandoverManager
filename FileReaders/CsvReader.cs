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
                while (!streamReader.EndOfStream)
                {
                    // tempporary mock to read line

                    var line = streamReader.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }

        #endregion
    }
}
