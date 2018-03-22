using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    /// <summary>
    /// Provides mechanism for reading data from given file path
    /// </summary>
    public class FileDataReader : IDataReader
    {
        /// <summary>
        /// Reads all file content in a string
        /// </summary>
        /// <param name="filePath">Input file path to read</param>
        /// <returns>File data as a string</returns>
        public string ReadData(string filePath)
        {
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            throw new FileNotFoundException($"File not found at given source location: {filePath}");
        }
    }
}