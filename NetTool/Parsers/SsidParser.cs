using System;

namespace NetworkMonitors.Parsers
{
    public static class SsidParser
    {
        public static string ParseFromBytes(byte[] inputBytes)
        {
            string result = string.Empty;
            foreach (var inputByte in inputBytes)
            {
                if (inputByte != 0)
                {
                    char character = (char)inputByte;
                    result = string.Concat(result, character);
                }
            }

            return result;
        }
    }
}