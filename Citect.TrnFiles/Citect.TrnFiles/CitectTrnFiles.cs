using System;
using System.IO;
using System.Text;

namespace Citect.TrnFiles
{
    /// <summary>
    /// Provide methods for reading Citect Trends Files
    /// </summary>
    public static class CitectTrnFiles
    {
        /// <summary>
        /// Read HST Trend File
        /// </summary>
        /// <param name="path">The file to open</param>
        /// <returns></returns>
        public static HstFile ReadHstFile(string path)
        {
            var hstFile = new HstFile();

            using (var stream = File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    ReadHstMasterHeader(hstFile, reader);
                    ReadHstFileHeaders(hstFile, reader);
                }
            }

            return hstFile;
        }

        /// <summary>
        /// Read Master Header of the HST Trend File
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstMasterHeader(HstFile hstFile, BinaryReader reader)
        {
            hstFile.Master.Title = reader.ReadString(128);
            hstFile.Master.Id = reader.ReadString(8);
            hstFile.Master.Type = reader.ReadInt16();
            hstFile.Master.Version = (TrnFileVersions)reader.ReadInt16();
            var alignment1 = reader.ReadString(4);
            hstFile.Master.Mode = reader.ReadInt32();
            hstFile.Master.MaxFiles = reader.ReadInt16();
            hstFile.Master.Files = reader.ReadInt16();
            hstFile.Master.NextFile = reader.ReadInt16();
            hstFile.Master.UserFiles = reader.ReadInt16();
            var alignment2 = reader.ReadString(20);
        }

        /// <summary>
        /// Read File Headers of the HST Trend File
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileHeaders(HstFile hstFile, BinaryReader reader)
        {
            for (int i = 0; i < hstFile.Master.Files + hstFile.Master.UserFiles; i++)
            {
                switch (hstFile.Master.Version)
                {
                    case TrnFileVersions.TwoByteOriginal:
                        throw new NotImplementedException();
                    case TrnFileVersions.TwoBytePreV500:
                        throw new NotImplementedException();
                    case TrnFileVersions.TwoByteV500:
                        throw new NotImplementedException();
                    case TrnFileVersions.TwoByteV531:
                        throw new NotImplementedException();
                    case TrnFileVersions.EightByteV531:
                        throw new NotImplementedException();
                    case TrnFileVersions.TwoByteV600:
                        ReadHstFileHeader5(hstFile, reader);
                        break;
                    case TrnFileVersions.EightByteV600:
                        ReadHstFileHeader6(hstFile, reader);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Read File Headers of the HST Trend File (<see cref="TrnFileVersions.TwoByteV600"/>)
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileHeader5(HstFile hstFile, BinaryReader reader)
        {
            var hstHeader = hstFile.Files.AddLast(new HstFileHeader());
            hstHeader.Value.Name = reader.ReadString(144);
            hstHeader.Value.Id = reader.ReadString(8);
            hstHeader.Value.Type = reader.ReadInt16();
            hstHeader.Value.Version = (TrnFileVersions)reader.ReadInt16();
            hstHeader.Value.StartEvent = reader.ReadInt32();
            hstHeader.Value.TrnName = reader.ReadString(80);
            hstHeader.Value.Mode = reader.ReadInt32();
            hstHeader.Value.Area = reader.ReadInt16();
            hstHeader.Value.Privilege = reader.ReadInt16();
            hstHeader.Value.TrnType = (TrnTypes)reader.ReadInt16();
            hstHeader.Value.SamplePeriod = reader.ReadInt32();
            hstHeader.Value.Units = reader.ReadString(8);
            hstHeader.Value.Format = reader.ReadInt32();
            hstHeader.Value.StartTime = DateTimeOffset.FromUnixTimeSeconds(reader.ReadInt32());
            hstHeader.Value.EndTime = DateTimeOffset.FromUnixTimeSeconds(reader.ReadInt32());
            hstHeader.Value.Length = reader.ReadInt32();
            hstHeader.Value.Ptr1 = reader.ReadInt32();
            hstHeader.Value.Ptr2 = reader.ReadInt32();
            var alignment1 = reader.ReadString(2);
        }

        /// <summary>
        /// Read File Headers of the HST Trend File (<see cref="TrnFileVersions.EightByteV600"/>)
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileHeader6(HstFile hstFile, BinaryReader reader)
        {
            var hstHeader = hstFile.Files.AddLast(new HstFileHeader());
            hstHeader.Value.Name = reader.ReadString(272);
            hstHeader.Value.Id = reader.ReadString(8);
            hstHeader.Value.Type = reader.ReadInt16();
            hstHeader.Value.Version = (TrnFileVersions)reader.ReadInt16();
            hstHeader.Value.StartEvent = reader.ReadInt64();
            var alignment1 = reader.ReadString(12);
            hstHeader.Value.TrnName = reader.ReadString(80);
            hstHeader.Value.Mode = reader.ReadInt32();
            hstHeader.Value.Area = reader.ReadInt16();
            hstHeader.Value.Privilege = reader.ReadInt16();
            hstHeader.Value.TrnType = (TrnTypes)reader.ReadInt16();
            hstHeader.Value.SamplePeriod = reader.ReadInt32();
            hstHeader.Value.Units = reader.ReadString(8);
            hstHeader.Value.Format = reader.ReadInt32();
            hstHeader.Value.StartTime = DateTimeOffset.FromFileTime(reader.ReadInt64());
            hstHeader.Value.EndTime = DateTimeOffset.FromFileTime(reader.ReadInt64());
            hstHeader.Value.Length = reader.ReadInt32();
            hstHeader.Value.Ptr1 = reader.ReadInt32();
            hstHeader.Value.Ptr2 = reader.ReadInt64();
            var alignment2 = reader.ReadString(6);
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

                    var title = reader.ReadString(112);
                    var rawZero = reader.ReadSingle();
                    var rawFull = reader.ReadSingle();
                    var engZero = reader.ReadSingle();
                    var engFull = reader.ReadSingle();
                    var id = reader.ReadString(8);
                    var fileType = reader.ReadInt16();
                    var version = reader.ReadInt16();
                    var startEvNo = reader.ReadInt64();
                    var alignment1 = reader.ReadString(12);
                    var logName = reader.ReadString(80);
                    var mode = reader.ReadInt32();
                    var area = reader.ReadInt16();
                    var priv = reader.ReadInt16();
                    var history = reader.ReadInt16();
                    var samplePeriod = reader.ReadInt32();
                    var egu = reader.ReadString(8);
                    var format = reader.ReadInt32();
                    var startTime = reader.ReadInt64();
                    var startTimeUtc = DateTime.FromFileTimeUtc(startTime);
                    var endTime = reader.ReadInt64();
                    var endTimeUtc = DateTime.FromFileTimeUtc(endTime);
                    var dataLength = reader.ReadInt32();
                    var filePointer = reader.ReadInt32();
                    var endEvNo = reader.ReadInt64();
                    var alignment2 = reader.ReadString(6);

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
