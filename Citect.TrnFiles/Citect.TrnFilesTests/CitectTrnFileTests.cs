using Citect.TrnFiles;

namespace Citect.TrnFilesTests
{
    [TestClass]
    public sealed class CitectTrnFileTests
    {
        [TestMethod]
        public void ReadHstFile5()
        {
            var hstFile = CitectTrnFiles.ReadHstFile(@"data\E_MOT1_0\IsStarted\IsStarted.HST");

            // Master Header
            Assert.AreEqual(@"CITECT
VERSION 2
TREND MASTER
E_MOT1_0_IsStarted
Thu Apr  9 16:20:13 2026", hstFile.Header.Title);            
            Assert.AreEqual("CITECT", hstFile.Header.Id);
            Assert.AreEqual(0, hstFile.Header.Type);
            Assert.AreEqual(TrnFileVersions.TwoByteV600, hstFile.Header.Version);
            Assert.AreEqual(0, hstFile.Header.Mode);
            Assert.AreEqual(36, hstFile.Header.MaxFiles);
            Assert.AreEqual(3, hstFile.Header.Files);
            Assert.AreEqual(0, hstFile.Header.NextFile);
            Assert.AreEqual(0, hstFile.Header.UserFiles);
            Assert.HasCount(3, hstFile.Data);

            // Data Header 0
            var header0 = hstFile.Data.ElementAt(0);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\E_MOT1_0\IsStarted\IsStarted.002", header0.Name);
            Assert.AreEqual("CITECT", header0.Id);
            Assert.AreEqual(0, header0.Type);
            Assert.AreEqual(TrnFileVersions.TwoByteV600, header0.Version);
            Assert.AreEqual(1, header0.StartEvent);
            Assert.AreEqual("E_MOT1_0_IsStarted", header0.TrnName);
            Assert.AreEqual(0, header0.Mode);
            Assert.AreEqual(0, header0.Area);
            Assert.AreEqual(0, header0.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header0.TrnType);
            Assert.AreEqual(5000, header0.SamplePeriod);
            Assert.AreEqual("", header0.Units);
            Assert.AreEqual(262145, header0.Format);
            Assert.AreEqual(new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc), header0.StartTime);
            Assert.AreEqual(new DateTime(2026, 8, 31, 23, 59, 55, DateTimeKind.Utc), header0.EndTime);
            Assert.AreEqual(535680, header0.Length);
            Assert.AreEqual(355147, header0.Ptr1);
            Assert.AreEqual(-2, header0.Ptr2);

            // Data Header 1
            var header1 = hstFile.Data.ElementAt(1);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\E_MOT1_0\IsStarted\IsStarted.001", header1.Name);
            Assert.AreEqual("CITECT", header1.Id);
            Assert.AreEqual(0, header1.Type);
            Assert.AreEqual(TrnFileVersions.TwoByteV600, header1.Version);
            Assert.AreEqual(1, header1.StartEvent);
            Assert.AreEqual("E_MOT1_0_IsStarted", header1.TrnName);
            Assert.AreEqual(0, header1.Mode);
            Assert.AreEqual(0, header1.Area);
            Assert.AreEqual(0, header1.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header1.TrnType);
            Assert.AreEqual(5000, header1.SamplePeriod);
            Assert.AreEqual("", header1.Units);
            Assert.AreEqual(262145, header1.Format);
            Assert.AreEqual(new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc), header1.StartTime);
            Assert.AreEqual(new DateTime(2026, 7, 1, 23, 59, 55, DateTimeKind.Utc), header1.EndTime);
            Assert.AreEqual(535680, header1.Length);
            Assert.AreEqual(423778, header1.Ptr1);
            Assert.AreEqual(-2, header1.Ptr2);

            // Data Header 2
            var header2 = hstFile.Data.ElementAt(2);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\E_MOT1_0\IsStarted\IsStarted.000", header2.Name);
            Assert.AreEqual("CITECT", header2.Id);
            Assert.AreEqual(0, header2.Type);
            Assert.AreEqual(TrnFileVersions.TwoByteV600, header2.Version);
            Assert.AreEqual(1, header2.StartEvent);
            Assert.AreEqual("E_MOT1_0_IsStarted", header2.TrnName);
            Assert.AreEqual(0, header2.Mode);
            Assert.AreEqual(0, header2.Area);
            Assert.AreEqual(0, header2.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header2.TrnType);
            Assert.AreEqual(5000, header2.SamplePeriod);
            Assert.AreEqual("", header2.Units);
            Assert.AreEqual(262145, header2.Format);
            Assert.AreEqual(new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), header2.StartTime);
            Assert.AreEqual(new DateTime(2026, 5, 1, 23, 59, 55, DateTimeKind.Utc), header2.EndTime);
            Assert.AreEqual(535680, header2.Length);
            Assert.AreEqual(148714, header2.Ptr1);
            Assert.AreEqual(-2, header2.Ptr2);
        }

        [TestMethod]
        public void ReadHstFile6()
        {
            var hstFile = CitectTrnFiles.ReadHstFile(@"data\IO_AI_EX_0\Out\Out.HST");

            // Master Header
            Assert.AreEqual(@"CITECT
VERSION 2
TREND MASTER
IO_AI_EX_0_Out
Thu Apr  9 16:20:22 2026", hstFile.Header.Title);
            Assert.AreEqual("CITECT", hstFile.Header.Id);
            Assert.AreEqual(0, hstFile.Header.Type);
            Assert.AreEqual(TrnFileVersions.EightByteV600, hstFile.Header.Version);
            Assert.AreEqual(0, hstFile.Header.Mode);
            Assert.AreEqual(36, hstFile.Header.MaxFiles);
            Assert.AreEqual(3, hstFile.Header.Files);
            Assert.AreEqual(0, hstFile.Header.NextFile);
            Assert.AreEqual(0, hstFile.Header.UserFiles);
            Assert.HasCount(3, hstFile.Data);

            // Data Header 0
            var header0 = hstFile.Data.ElementAt(0);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\IO_AI_EX_0\Out\Out.002", header0.Name);
            Assert.AreEqual("CITECT", header0.Id);
            Assert.AreEqual(0, header0.Type);
            Assert.AreEqual(TrnFileVersions.EightByteV600, header0.Version);
            Assert.AreEqual(1, header0.StartEvent);
            Assert.AreEqual("IO_AI_EX_0_Out", header0.TrnName);
            Assert.AreEqual(0, header0.Mode);
            Assert.AreEqual(0, header0.Area);
            Assert.AreEqual(0, header0.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header0.TrnType);
            Assert.AreEqual(5000, header0.SamplePeriod);
            Assert.AreEqual("mbar", header0.Units);
            Assert.AreEqual(262145, header0.Format);
            Assert.AreEqual(new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc), header0.StartTime);
            Assert.AreEqual(new DateTime(2026, 8, 31, 23, 59, 55, DateTimeKind.Utc), header0.EndTime);
            Assert.AreEqual(535680, header0.Length);
            Assert.AreEqual(355147, header0.Ptr1);
            Assert.AreEqual(-2, header0.Ptr2);

            // Data Header 1
            var header1 = hstFile.Data.ElementAt(1);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\IO_AI_EX_0\Out\Out.001", header1.Name);
            Assert.AreEqual("CITECT", header1.Id);
            Assert.AreEqual(0, header1.Type);
            Assert.AreEqual(TrnFileVersions.EightByteV600, header1.Version);
            Assert.AreEqual(1, header1.StartEvent);
            Assert.AreEqual("IO_AI_EX_0_Out", header1.TrnName);
            Assert.AreEqual(0, header1.Mode);
            Assert.AreEqual(0, header1.Area);
            Assert.AreEqual(0, header1.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header1.TrnType);
            Assert.AreEqual(5000, header1.SamplePeriod);
            Assert.AreEqual("mbar", header1.Units);
            Assert.AreEqual(262145, header1.Format);
            Assert.AreEqual(new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc), header1.StartTime);
            Assert.AreEqual(new DateTime(2026, 7, 1, 23, 59, 55, DateTimeKind.Utc), header1.EndTime);
            Assert.AreEqual(535680, header1.Length);
            Assert.AreEqual(423778, header1.Ptr1);
            Assert.AreEqual(-2, header1.Ptr2);

            // Data Header 2
            var header2 = hstFile.Data.ElementAt(2);
            Assert.AreEqual(@"C:\ProgramData\AVEVA Plant SCADA 2023 R2\Data\DFB\Trends\IO_AI_EX_0\Out\Out.000", header2.Name);
            Assert.AreEqual("CITECT", header2.Id);
            Assert.AreEqual(0, header2.Type);
            Assert.AreEqual(TrnFileVersions.EightByteV600, header2.Version);
            Assert.AreEqual(1, header2.StartEvent);
            Assert.AreEqual("IO_AI_EX_0_Out", header2.TrnName);
            Assert.AreEqual(0, header2.Mode);
            Assert.AreEqual(0, header2.Area);
            Assert.AreEqual(0, header2.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, header2.TrnType);
            Assert.AreEqual(5000, header2.SamplePeriod);
            Assert.AreEqual("mbar", header2.Units);
            Assert.AreEqual(262145, header2.Format);
            Assert.AreEqual(new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), header2.StartTime);
            Assert.AreEqual(new DateTime(2026, 5, 1, 23, 59, 55, DateTimeKind.Utc), header2.EndTime);
            Assert.AreEqual(535680, header2.Length);
            Assert.AreEqual(148714, header2.Ptr1);
            Assert.AreEqual(-2, header2.Ptr2);
        }
       
        [TestMethod]
        public void ReadDataFile5()
        {
            var dataFile = CitectTrnFiles.ReadDataFile(@"data\E_MOT1_0\IsStarted\IsStarted.000");

            // Header
            Assert.AreEqual(@"CITECT
VERSION 2
TREND HISTORY
E_MOT1_0_IsStarted
Wed Apr  1 02:00:00 2026", dataFile.Header.Title);
            Assert.AreEqual(0F, dataFile.Header.RawZero);
            Assert.AreEqual(32000F, dataFile.Header.RawFull);
            Assert.AreEqual(0F, dataFile.Header.EngZero);
            Assert.AreEqual(32000F, dataFile.Header.EngFull);
            Assert.AreEqual("CITECT", dataFile.Header.Id);
            Assert.AreEqual(0, dataFile.Header.Type);
            Assert.AreEqual(TrnFileVersions.TwoByteV600, dataFile.Header.Version);
            Assert.AreEqual(1, dataFile.Header.StartEvent); 
            Assert.AreEqual("E_MOT1_0_IsStarted", dataFile.Header.TrnName);
            Assert.AreEqual(0, dataFile.Header.Mode);
            Assert.AreEqual(0, dataFile.Header.Area);
            Assert.AreEqual(0, dataFile.Header.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, dataFile.Header.TrnType);
            Assert.AreEqual(5000, dataFile.Header.SamplePeriod);
            Assert.AreEqual("", dataFile.Header.Units);
            Assert.AreEqual(262145, dataFile.Header.Format);
            Assert.AreEqual(new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), dataFile.Header.StartTime);
            Assert.AreEqual(new DateTime(2026, 5, 1, 23, 59, 55, DateTimeKind.Utc), dataFile.Header.EndTime);
            Assert.AreEqual(535680, dataFile.Header.Length);
            Assert.AreEqual(148714, dataFile.Header.Ptr1);
            Assert.AreEqual(-2, dataFile.Header.Ptr2);


        }

        [TestMethod]
        public void ReadDataFile6()
        {
            var dataFile = CitectTrnFiles.ReadDataFile(@"data\IO_AI_EX_0\Out\Out.000");

            // Header
            Assert.AreEqual(@"CITECT
VERSION 2
TREND HISTORY
IO_AI_EX_0_Out
Wed Apr  1 02:00:00 2026", dataFile.Header.Title);
            Assert.AreEqual(float.MinValue, dataFile.Header.RawZero);
            Assert.AreEqual(float.MaxValue, dataFile.Header.RawFull);
            Assert.AreEqual(float.MinValue, dataFile.Header.EngZero);
            Assert.AreEqual(float.MaxValue, dataFile.Header.EngFull);
            Assert.AreEqual("CITECT", dataFile.Header.Id);
            Assert.AreEqual(0, dataFile.Header.Type);
            Assert.AreEqual(TrnFileVersions.EightByteV600, dataFile.Header.Version);
            Assert.AreEqual(1, dataFile.Header.StartEvent);
            Assert.AreEqual("IO_AI_EX_0_Out", dataFile.Header.TrnName);
            Assert.AreEqual(0, dataFile.Header.Mode);
            Assert.AreEqual(0, dataFile.Header.Area);
            Assert.AreEqual(0, dataFile.Header.Privilege);
            Assert.AreEqual(TrnTypes.PeriodicTrend, dataFile.Header.TrnType);
            Assert.AreEqual(5000, dataFile.Header.SamplePeriod);
            Assert.AreEqual("mbar", dataFile.Header.Units);
            Assert.AreEqual(262145, dataFile.Header.Format);
            Assert.AreEqual(new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), dataFile.Header.StartTime);
            Assert.AreEqual(new DateTime(2026, 5, 1, 23, 59, 55, DateTimeKind.Utc), dataFile.Header.EndTime);
            Assert.AreEqual(535680, dataFile.Header.Length);
            Assert.AreEqual(148714, dataFile.Header.Ptr1);
            Assert.AreEqual(-2, dataFile.Header.Ptr2);


        }
    }
}
