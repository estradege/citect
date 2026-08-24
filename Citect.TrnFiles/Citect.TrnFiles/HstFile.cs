using System.Collections.Generic;

namespace Citect.TrnFiles
{
    /// <summary>
    /// HST Trend File.
    /// </summary>
    public class HstFile
    {
        /// <summary>
        /// HST Trend File - Master Header.
        /// </summary>
        public HstFileMasterHeader Header { get; set; }
            = new HstFileMasterHeader();

        /// <summary>
        /// HST Trend File - Data File Headers.
        /// </summary>
        public LinkedList<HstFileDataHeader> Data { get; set; }
            = new LinkedList<HstFileDataHeader>();
    }
}
