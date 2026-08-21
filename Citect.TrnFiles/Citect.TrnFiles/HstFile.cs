using System.Collections.Generic;
using System.Linq;

namespace Citect.TrnFiles
{
    /// <summary>
    /// HST Trend File
    /// </summary>
    public class HstFile
    {
        public HstMaster Master { get; set; }
            = new HstMaster();

        public LinkedList<HstHeader> Headers { get; set; }
            = new LinkedList<HstHeader>();
    }
}
