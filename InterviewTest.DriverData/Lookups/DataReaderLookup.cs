using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Lookups
{
    /// <summary>
    /// Provides mechanism for getting appropriate data reader instance based on reader type text value
    /// </summary>
    public static class DataReaderLookup
    {
        /// <summary>
        /// Returns appropriate data reader based on input text value of reader type
        /// </summary>
        /// <param name="type">A text value of parser type</param>
        /// <returns>An instance of data reader</returns>
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
