using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Lookups
{
    public static class DataReaderLookup
    {
        public static IDataReader GetReader(string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Null datareader type");

            if (!dataReaderList.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised datareader type");
            
            return dataReaderList[type]();
        }

        private static Dictionary<string, Func<IDataReader>> dataReaderList =
                    new Dictionary<string, Func<IDataReader>>
                {
                    { "file", FileDataReader }
                };

        private static IDataReader FileDataReader()
        {
            return new FileDataReader();
        }
    }
}
