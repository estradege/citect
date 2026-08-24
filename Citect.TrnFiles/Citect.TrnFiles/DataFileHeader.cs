using System;

namespace Citect.TrnFiles
{
    /// <summary>
    /// Data Trend File - Header.
    /// </summary>
    public class DataFileHeader
    {
        /// <summary>
        /// Title of the file. It contains information, such as file version, type, start time and logging name.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// RawZero Scale
        /// </summary>
        public float RawZero { get; set; }

        /// <summary>
        /// RawFull Scale
        /// </summary>
        public float RawFull { get; set; }

        /// <summary>
        /// EngZero Scale
        /// </summary>
        public float EngZero { get; set; }

        /// <summary>
        /// EngFull Scale
        /// </summary>
        public float EngFull { get; set; }

        /// <summary>
        /// ID for Citect files. It is set to "CITECT".
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of the Citect file. It is set to 0, which is FILE_TYPE_TREND.
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// Version number of the trends.
        /// </summary>
        public TrnFileVersions Version { get; set; }

        /// <summary>
        /// Starting Number for events in this history file. (Event Trends only.)
        /// </summary>
        public long StartEvent { get; set; }

        /// <summary>
        /// Name of the trend record that owns the history file.
        /// </summary>
        public string TrnName { get; set; }

        /// <summary>
        /// Indicates the mode of the MASTER file. (For future use. Currently it is set to 0.)
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// The area that has access to the acquired data.
        /// </summary>
        public short Area { get; set; }

        /// <summary>
        /// The access rights required to execute the command.
        /// </summary>
        public short Privilege { get; set; }

        /// <summary>
        /// Trend Type.
        /// </summary>
        public TrnTypes TrnType { get; set; }

        /// <summary>
        /// Sample period of logging (in milliseconds)
        /// </summary>
        public int SamplePeriod { get; set; }

        /// <summary>
        /// Units of scales. (Set to the units of the trend record.)
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Format of scales. (Set to the format of the trend record.)
        /// </summary>
        public int Format { get; set; }

        /// <summary>
        /// The earliest time that a sample can have and be placed in this file.
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// For Periodic Trends the EndTime is always set to the latest possible sample time that can be put into this file. 
        /// For Event Trends the EndTime is the time of the newest sample stored in the file (one less than the StartTime if there are no samples in the file).
        /// </summary>
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// Shows the number of data items.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// The location of the newest sample in the file, in number of samples from the start of the data portion of the file. (Periodic Trends only).
        /// </summary>
        public int Ptr1 { get; set; }

        /// <summary>
        /// The event number of the next sample after the last one in this file. (Event Trends only)
        /// </summary>
        public long Ptr2 { get; set; }
    }
}
