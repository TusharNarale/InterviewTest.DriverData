using InterviewTest.DriverData.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class JsonDataParser : IDataParser
    {
        public T ParseData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
