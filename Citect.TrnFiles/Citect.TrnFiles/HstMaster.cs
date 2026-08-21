namespace Citect.TrnFiles
{
    /// <summary>
    /// HST Trend File
    /// </summary>
    public class HstMaster
    {
        /// <summary>
        /// Title of the file (ASCII). It contains information, such as file version, type, start time and logging name.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ID for Citect files. It is set to "CITECT".
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of the Citect file. It is set to 0, which is FILE_TYPE_TREND.
        /// </summary>
        public short FileType { get; set; }

        /// <summary>
        /// Version number of the trends. (3 or 4 depending on the storage method of the Trend)
        /// </summary>
        public Versions Version { get; set; }

        /// <summary>
        /// Indicates the mode of the MASTER file. (For future use. Currently it is set to 0.)
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Maximum Number of history files to be created. (not including AddOn)
        /// </summary>
        public short History { get; set; }

        /// <summary>
        /// Number of history files currently created. (not including AddOn)
        /// </summary>
        public short Files { get; set; }

        /// <summary>
        /// Next history file number. Internal use only, users MUST NOT use this entry to get the next history file.
        /// </summary>
        public short NextFile { get; set; }

        /// <summary>
        /// Shows the number of history files added onto the system through user functions. User can add old (backed up) history files to the system temporarily by using TrnAddHistory() and TrnDelHistory().
        /// </summary>
        public short AddOn { get; set; }
    }
}
