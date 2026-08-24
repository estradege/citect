namespace Citect.TrnFiles
{
    /// <summary>
    /// Data Trend File.
    /// </summary>
    public class DataFile
    {
        /// <summary>
        /// Header
        /// </summary>
        public DataFileHeader Header { get; set; }
            = new DataFileHeader();
    }
}
