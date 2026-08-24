using System.Collections.Generic;
using System.Linq;

namespace Citect.TrnFiles
{
    /// <summary>
    /// DATA Trend File.
    /// </summary>
    public class DataFile
    {
        /// <summary>
        /// DATA Trend File - Header.
        /// </summary>
        public DataFileHeader Header { get; set; }
            = new DataFileHeader();

        /// <summary>
        /// DATA Trend File - Samples.
        /// </summary>
        public LinkedList<DataFileSample> Samples { get; set; }
            = new LinkedList<DataFileSample>();

        /// <summary>
        /// DATA Trend File - Valid Samples.
        /// </summary>
        public IEnumerable<DataFileSample> ValidSamples 
            => Samples.Where(sample => !double.IsNaN(sample.Value));
    }
}
