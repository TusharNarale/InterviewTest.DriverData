namespace InterviewTest.DriverData.Interfaces
{
    /// <summary>
    /// Provides mechanism for reading data from given data source
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Reads data from given data source and returns as a string
        /// </summary>
        /// <param name="source">Data source URL or path</param>
        /// <returns>Data as a string</returns>
        string ReadData(string source);
    }
}
