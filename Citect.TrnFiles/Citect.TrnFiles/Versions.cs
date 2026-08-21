namespace Citect.TrnFiles
{
    public enum Versions
    {
        /// <summary>
        /// Before v5.00
        /// </summary>
        TwoByteOriginal = 0,

        /// <summary>
        /// Before v5.00
        /// </summary>
        TwoBytePreV500 = 1,

        /// <summary>
        /// 5.00 - 5.30
        /// </summary>
        TwoByteV500 = 2,

        /// <summary>
        /// 5.31 - 5.50
        /// </summary>
        TwoByteV531 = 3,

        /// <summary>
        /// 5.31 - 5.50
        /// </summary>
        EightByteV531 = 4,

        /// <summary>
        /// 6.00 - ?
        /// </summary>
        TwoByteV600 = 5,

        /// <summary>
        /// 6.00 - ?
        /// </summary>
        EightByteV600 = 6
    }
}
