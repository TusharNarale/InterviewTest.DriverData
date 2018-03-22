using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Lookups
{
    public static class DataParserLookup
    {
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
