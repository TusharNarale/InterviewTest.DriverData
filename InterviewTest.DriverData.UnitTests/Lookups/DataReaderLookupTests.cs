using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Lookups;
using NUnit.Framework;
using System;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class DataReaderLookupTests
    {
        [Test]
        public void ShouldReturnFileReaderForValidInput()
        {
            // Arrange
            var readerType = "file";
            
            // Act
            var dataReader = DataReaderLookup.GetReader(readerType);

            // Assert
            Assert.IsInstanceOf(typeof(FileDataReader), dataReader);
        }

        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            // Arrange
            var readerType = "";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DataReaderLookup.GetReader(readerType));
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            // Arrange
            string readerType = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => DataReaderLookup.GetReader(readerType));
        }

        [Test]
        public void ShouldThrowExceptionForInvalidInput()
        {
            // Arrange
            var readerType = "apiFeed";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DataReaderLookup.GetReader(readerType));
        }
    }
}
