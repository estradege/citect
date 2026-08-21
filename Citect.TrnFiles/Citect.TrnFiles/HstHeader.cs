using System;

namespace Citect.TrnFiles
{
    /// <summary>
    /// HST Trend File
    /// </summary>
    public class HstHeader
    {
        /// <summary>
        /// History file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID for Citect files. It is set to "CITECT".
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of the Citect file. It is set to 0, which is FILE_TYPE_TREND.
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// Version number of the trends. (equal to 4 for this Storage Method)
        /// </summary>
        public Versions Version { get; set; }

        /// <summary>
        /// Starting Number for events in this history file. (Event Trends only.)
        /// </summary>
        public long StartEvNo { get; set; }

        /// <summary>
        /// Name of the trend record that owns the history file.
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// Indicates the mode of the history file. (For future use. Currently it is set to 0.)
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 2 The area that has access to the acquired data.
        /// </summary>
        public short Area { get; set; }

        /// <summary>
        /// The access rights required to execute the command.
        /// </summary>
        public short Priv { get; set; }

        /// <summary>
        /// History File Type. Set to 0 for Periodic and Periodic_Event Trends. Set to 4 for Event Trends.
        /// </summary>
        public HistoryTypes HistoryType { get; set; }

        /// <summary>
        /// Sample period of logging (in seconds)
        /// </summary>
        public int SamplePeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SamplePeriodRaw { get; set; }

        /// <summary>
        /// Units of scales. (Set to the units of the trend record.)
        /// </summary>
        public string Egu { get; set; }

        /// <summary>
        /// Format of scales. (Set to the format of the trend record.)
        /// </summary>
        public int Format { get; set; }

        /// <summary>
        /// The earliest time that a sample can have and be placed in this file. (in number of 100 nanosecond time units since 1st of January, 1601)
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// For Periodic Trends the EndTime is always set to the latest possible sample time that can be put into this file. For Event Trends the EndTime is the time of the newest sample stored in the file (one less than the StartTime if there are no samples in the file). (in number of 100 nanosecond time units since 1st of January, 1601)
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// Shows the number of data items. DataLength = (File Size - HeaderSize) / 8
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// The location of the newest sample in the file, in number of samples from the start of the data portion of the file.(Periodic Trends only).
        /// </summary>
        public int FilePointer { get; set; }

        /// <summary>
        /// The event number of the next sample after the last one in this file. (Event Trends only)
        /// </summary>
        public long EndEvNo { get; set; }
    }
}
