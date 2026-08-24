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
        /// Read HST Trend File.
        /// </summary>
        /// <param name="path">The file to open.</param>
        /// <returns></returns>
        public static HstFile ReadHstFile(string path)
        {
            var hstFile = new HstFile();

            using (var stream = File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    ReadHstFileMasterHeader(hstFile, reader);
                    ReadHstFileDataHeaders(hstFile, reader);
                }
            }

            return hstFile;
        }

        /// <summary>
        /// Read DATA Trend File.
        /// </summary>
        /// <param name="path">The file to open.</param>
        /// <returns></returns>
        public static DataFile ReadDataFile(string path)
        {
            var dataFile = new DataFile();

            using (var stream = File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    ReadDataFileHeader(dataFile, reader);
                    //ReadHstFileHeaders(hstFile, reader);
                }
            }

            return dataFile;
        }

        /// <summary>
        /// Read HST Trend File : Master Header.
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileMasterHeader(HstFile hstFile, BinaryReader reader)
        {
            hstFile.Header.Title = reader.ReadString(128);
            hstFile.Header.Id = reader.ReadString(8);
            hstFile.Header.Type = reader.ReadInt16();
            hstFile.Header.Version = (TrnFileVersions)reader.ReadInt16();
            var alignment1 = reader.ReadString(4);
            hstFile.Header.Mode = reader.ReadInt32();
            hstFile.Header.MaxFiles = reader.ReadInt16();
            hstFile.Header.Files = reader.ReadInt16();
            hstFile.Header.NextFile = reader.ReadInt16();
            hstFile.Header.UserFiles = reader.ReadInt16();
            var alignment2 = reader.ReadString(20);
        }

        /// <summary>
        /// Read HST Trend File : Data Header.
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileDataHeaders(HstFile hstFile, BinaryReader reader)
        {
            for (int i = 0; i < hstFile.Header.Files + hstFile.Header.UserFiles; i++)
            {
                switch (hstFile.Header.Version)
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
                        ReadHstFileDataHeader5(hstFile, reader);
                        break;
                    case TrnFileVersions.EightByteV600:
                        ReadHstFileDataHeader6(hstFile, reader);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// RRead HST Trend File : Data Header (<see cref="TrnFileVersions.TwoByteV600"/>).
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileDataHeader5(HstFile hstFile, BinaryReader reader)
        {
            var hstHeader = hstFile.Data.AddLast(new HstFileDataHeader());
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
        /// RRead HST Trend File : Data Header (<see cref="TrnFileVersions.EightByteV600"/>).
        /// </summary>
        /// <param name="hstFile"></param>
        /// <param name="reader"></param>
        private static void ReadHstFileDataHeader6(HstFile hstFile, BinaryReader reader)
        {
            var hstHeader = hstFile.Data.AddLast(new HstFileDataHeader());
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

        /// <summary>
        /// Read DATA Trend File : Header.
        /// </summary>
        /// <param name="dataFile"></param>
        /// <param name="reader"></param>
        private static void ReadDataFileHeader(DataFile dataFile, BinaryReader reader)
        {
            dataFile.Header.Title = reader.ReadString(112);
            dataFile.Header.RawZero = reader.ReadSingle();
            dataFile.Header.RawFull = reader.ReadSingle();
            dataFile.Header.EngZero = reader.ReadSingle();
            dataFile.Header.EngFull = reader.ReadSingle();
            dataFile.Header.Id = reader.ReadString(8);
            dataFile.Header.Type = reader.ReadInt16();
            dataFile.Header.Version = (TrnFileVersions)reader.ReadInt16();

            switch (dataFile.Header.Version)
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
                    ReadDataFileHeader5(dataFile, reader);
                    break;
                case TrnFileVersions.EightByteV600:
                    ReadDataFileHeader6(dataFile, reader);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///  Read DATA Trend File : Header (<see cref="TrnFileVersions.TwoByteV600"/>).
        /// </summary>
        /// <param name="dataFile"></param>
        /// <param name="reader"></param>
        private static void ReadDataFileHeader5(DataFile dataFile, BinaryReader reader)
        {
            dataFile.Header.StartEvent = reader.ReadInt32();
            dataFile.Header.TrnName = reader.ReadString(80);
            dataFile.Header.Mode = reader.ReadInt32();
            dataFile.Header.Area = reader.ReadInt16();
            dataFile.Header.Privilege = reader.ReadInt16();
            dataFile.Header.TrnType = (TrnTypes)reader.ReadInt16();
            dataFile.Header.SamplePeriod = reader.ReadInt32();
            dataFile.Header.Units = reader.ReadString(8);
            dataFile.Header.Format = reader.ReadInt32();
            dataFile.Header.StartTime = DateTimeOffset.FromUnixTimeSeconds(reader.ReadInt32());
            dataFile.Header.EndTime = DateTimeOffset.FromUnixTimeSeconds(reader.ReadInt32());
            dataFile.Header.Length = reader.ReadInt32();
            dataFile.Header.Ptr1 = reader.ReadInt32();
            dataFile.Header.Ptr2 = reader.ReadInt32();
            var alignment2 = reader.ReadString(2);
        }

        /// <summary>
        ///  Read DATA Trend File : Header (<see cref="TrnFileVersions.EightByteV600"/>).
        /// </summary>
        /// <param name="dataFile"></param>
        /// <param name="reader"></param>
        private static void ReadDataFileHeader6(DataFile dataFile, BinaryReader reader)
        {
            dataFile.Header.StartEvent = reader.ReadInt64();
            var alignment1 = reader.ReadString(12);
            dataFile.Header.TrnName = reader.ReadString(80);
            dataFile.Header.Mode = reader.ReadInt32();
            dataFile.Header.Area = reader.ReadInt16();
            dataFile.Header.Privilege = reader.ReadInt16();
            dataFile.Header.TrnType = (TrnTypes)reader.ReadInt16();
            dataFile.Header.SamplePeriod = reader.ReadInt32();
            dataFile.Header.Units = reader.ReadString(8);
            dataFile.Header.Format = reader.ReadInt32();
            dataFile.Header.StartTime = DateTimeOffset.FromFileTime(reader.ReadInt64());
            dataFile.Header.EndTime = DateTimeOffset.FromFileTime(reader.ReadInt64());
            dataFile.Header.Length = reader.ReadInt32();
            dataFile.Header.Ptr1 = reader.ReadInt32();
            dataFile.Header.Ptr2 = reader.ReadInt64();
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
