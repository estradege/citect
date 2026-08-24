using System.IO;

namespace Citect.TrnFiles
{
    /// <summary>
    /// <see cref="BinaryReader"/> extensions
    /// </summary>
    internal static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads the specified number of characters from the current stream, returns the
        /// data as string, and advances the current position in accordance with
        /// the Encoding used and the specific character being read from the stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="count">The number of characters to read.</param>
        /// <returns></returns>
        public static string ReadString(this BinaryReader reader, int count)
        {
            var s = new string(reader.ReadChars(count));
            return s.Trim('\0', '\u001a').Trim().Replace("\n\r", "\r\n");
        }
    }
}