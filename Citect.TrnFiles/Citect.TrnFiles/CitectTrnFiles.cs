using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Citect.TrnFiles
{
    /// <summary>
    /// Provide methods for reading Citect Trends Files
    /// </summary>
    public static class CitectTrnFiles
    {
        /// <summary>
        /// Read HST Citect Trend File
        /// </summary>
        /// <param name="path">The file to open</param>
        /// <returns></returns>
        public static HstFile ReadHst(string path)
        {
            var hstFile = new HstFile();

            using (var stream = File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    ReadMaster(hstFile, reader);
                    ReadHeaders(hstFile, reader);
                }
            }

            return hstFile;
        }

        /// <summary>
        /// Read "Master" part of the file
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadMaster(HstFile hstFile, BinaryReader reader)
        {
            hstFile.Master.Title = new string(reader.ReadChars(128));
            hstFile.Master.Id = new string(reader.ReadChars(8)).Trim('\0');
            hstFile.Master.FileType = reader.ReadInt16();
            hstFile.Master.Version = (Versions)reader.ReadInt16();
            var alignment1 = new string(reader.ReadChars(4));
            hstFile.Master.Mode = reader.ReadInt32();
            hstFile.Master.History = reader.ReadInt16();
            hstFile.Master.Files = reader.ReadInt16();
            hstFile.Master.NextFile = reader.ReadInt16();
            hstFile.Master.AddOn = reader.ReadInt16();
            var alignment2 = new string(reader.ReadChars(20));
        }

        /// <summary>
        /// Read "Header" parts of the file
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHeaders(HstFile hstFile, BinaryReader reader)
        {
            for (int i = 0; i < hstFile.Master.Files + hstFile.Master.AddOn; i++)
            {
                switch (hstFile.Master.Version)
                {
                    case Versions.TwoByteOriginal:
                        throw new NotImplementedException();
                    case Versions.TwoBytePreV500:
                        throw new NotImplementedException();
                    case Versions.TwoByteV500:
                        throw new NotImplementedException();
                    case Versions.TwoByteV531:
                        throw new NotImplementedException();
                    case Versions.EightByteV531:
                        throw new NotImplementedException();
                    case Versions.TwoByteV600:
                        ReadHeaderV5(hstFile, reader);
                        break;
                    case Versions.EightByteV600:
                        ReadHeaderV6(hstFile, reader);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Read "Header" parts of the file
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHeaderV5(HstFile hstFile, BinaryReader reader)
        {
        }

        /// <summary>
        /// Read "Header" parts of the file
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHeaderV6(HstFile hstFile, BinaryReader reader)
        {
            var hstHeader = hstFile.Headers.AddLast(new HstHeader());
            hstHeader.Value.Name = new string(reader.ReadChars(272));
            hstHeader.Value.Id = new string(reader.ReadChars(8));
            hstHeader.Value.FileType = reader.ReadInt16();
            hstHeader.Value.Version = (Versions)reader.ReadInt16();
            hstHeader.Value.StartEvNo = reader.ReadInt64();
            var alignment1 = new string(reader.ReadChars(12));
            hstHeader.Value.LogName = new string(reader.ReadChars(80));
            hstHeader.Value.Mode = reader.ReadInt32();
            hstHeader.Value.Area = reader.ReadInt16();
            hstHeader.Value.Priv = reader.ReadInt16();
            hstHeader.Value.HistoryType = (HistoryTypes)reader.ReadInt16();
            hstHeader.Value.SamplePeriod = reader.ReadInt32();
            hstHeader.Value.Egu = new string(reader.ReadChars(8));
            hstHeader.Value.Format = reader.ReadInt32();
            hstHeader.Value.StartTime = reader.ReadInt64();
            hstHeader.Value.EndTime = reader.ReadInt64();
            hstHeader.Value.DataLength = reader.ReadInt32();
            hstHeader.Value.FilePointer = reader.ReadInt32();
            hstHeader.Value.EndEvNo = reader.ReadInt64();
            var alignment2 = new string(reader.ReadChars(6));
        }






        public static void Read()
        {
            var fileName = "C:\\ProgramData\\AVEVA Plant SCADA 2023 R2\\Data\\DFB\\Trends\\IO_AI_EX_0\\Out\\Out.002";


            using (var stream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    //var title = new string(reader.ReadChars(128));
                    //var id = new string(reader.ReadChars(8));
                    //var fileType = reader.ReadInt16();
                    //var version = reader.ReadInt16();
                    //var alignment1 = new string(reader.ReadChars(4));
                    //var mode = reader.ReadInt32();
                    //var history = reader.ReadInt16();
                    //var files = reader.ReadInt16();
                    //var next = reader.ReadInt16();
                    //var addon = reader.ReadInt16();
                    //var alignment2 = new string(reader.ReadChars(20));

                    var title = new string(reader.ReadChars(112));
                    var rawZero = reader.ReadSingle();
                    var rawFull = reader.ReadSingle();
                    var engZero = reader.ReadSingle();
                    var engFull = reader.ReadSingle();
                    var id = new string(reader.ReadChars(8));
                    var fileType = reader.ReadInt16();
                    var version = reader.ReadInt16();
                    var startEvNo = reader.ReadInt64();
                    var alignment1 = new string(reader.ReadChars(12));
                    var logName = new string(reader.ReadChars(80));
                    var mode = reader.ReadInt32();
                    var area = reader.ReadInt16();
                    var priv = reader.ReadInt16();
                    var history = reader.ReadInt16();
                    var samplePeriod = reader.ReadInt32();
                    var egu = new string(reader.ReadChars(8));
                    var format = reader.ReadInt32();
                    var startTime = reader.ReadInt64();
                    var startTimeUtc = DateTime.FromFileTimeUtc(startTime);
                    var endTime = reader.ReadInt64();
                    var endTimeUtc = DateTime.FromFileTimeUtc(endTime);
                    var dataLength = reader.ReadInt32();
                    var filePointer = reader.ReadInt32();
                    var endEvNo = reader.ReadInt64();
                    var alignment2 = new string(reader.ReadChars(6));

                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var value = reader.ReadDouble();
                        //if (double.IsNormal(value))
                        //{
                            Console.WriteLine($"{startTimeUtc} - {value}");
                        //}

                        startTimeUtc = startTimeUtc.AddMilliseconds(samplePeriod);
                    }
                }
            }

        }
    }
}
