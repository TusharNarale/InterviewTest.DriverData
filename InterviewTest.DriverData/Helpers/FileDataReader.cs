using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class FileDataReader : IDataReader
    {
        public string ReadData(string filePath)
        {
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            throw new FileNotFoundException($"File not found at given source location: {filePath}");
        }
    }
}