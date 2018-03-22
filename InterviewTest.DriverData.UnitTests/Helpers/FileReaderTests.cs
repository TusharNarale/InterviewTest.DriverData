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
            // Arrange
            string filePath = ConfigurationManager.AppSettings.Get("HistoryFilePath");

            // Act
            var actualResult = _dataReader.ReadData(filePath);

            // Assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void ShouldThrowException_ForInvalidInputPath()
        {
            // Arrange
            string filePath = "invalidPath";

            // Act and Assert
            Assert.Throws<FileNotFoundException>(() => _dataReader.ReadData(filePath));
        }
    }
}
