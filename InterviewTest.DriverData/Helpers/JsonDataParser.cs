using InterviewTest.DriverData.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    /// <summary>
    /// Provides mechanism for parsing json data into object of mentioned data type
    /// </summary>
    public class JsonDataParser : IDataParser
    {
        /// <summary>
        /// Parses json data into object of mentioned data type
        /// </summary>
        /// <typeparam name="T">Type to which input data should be parsed</typeparam>
        /// <param name="data">Input data to parse</param>
        /// <returns>Type to which input data should be parsed</returns>
        public T ParseData<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
                return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
