using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Lookups
{
    /// <summary>
    /// Provides mechanism for getting appropriate parser instance based on parser type text value
    /// </summary>
    public static class DataParserLookup
    {
        /// <summary>
        /// Returns appropriate data parser based on input text value of parser type
        /// </summary>
        /// <param name="type">A text value of parser type</param>
        /// <returns>An instance of data parser</returns>
        public static IDataParser GetParser(string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Null parser type");

            if (!parserList.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised parser type");

            return parserList[type]();
        }

        private static Dictionary<string, Func<IDataParser>> parserList =
                    new Dictionary<string, Func<IDataParser>>
                {
                    { "json", JsonDataParser }
                };

        private static IDataParser JsonDataParser()
        {
            return new JsonDataParser();
        }
    }
}
