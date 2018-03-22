using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Interfaces
{
    public interface IDataParser
    {
        T ParseData<T>(string data);
    }
}