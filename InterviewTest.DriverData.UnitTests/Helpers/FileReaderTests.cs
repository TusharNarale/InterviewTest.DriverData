using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Helpers
{
    [TestFixture]
    public class FileReaderTests
    {
        IDataReader _dataReader;

        [SetUp]
        public void Initialize()
        {
            _dataReader = new FileDataReader();
        }

        [TearDown]
        public void TearDown()
        {
            _dataReader = null;
        }

        [Test]
        public void ShouldReadFile_ForValidInputPath()
        {
            string filePath = ConfigurationManager.AppSettings.Get("HistoryFilePath");

            var actualResult = _dataReader.ReadData(filePath);

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void ShouldThrowException_ForInvalidInputPath()
        {
            string filePath = "invalidPath";

            Assert.Throws<FileNotFoundException>(() => _dataReader.ReadData(filePath));
        }
    }
}
