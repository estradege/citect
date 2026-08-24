using System.Collections.Generic;

namespace Citect.TrnFiles
{
    /// <summary>
    /// HST Trend File.
    /// </summary>
    public class HstFile
    {
        /// <summary>
        /// Master Header.
        /// </summary>
        public HstMasterHeader Master { get; set; }
            = new HstMasterHeader();

        /// <summary>
        /// Files Header.
        /// </summary>
        public LinkedList<HstFileHeader> Files { get; set; }
            = new LinkedList<HstFileHeader>();
    }
}
