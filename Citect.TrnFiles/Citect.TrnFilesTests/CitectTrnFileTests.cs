using Citect.TrnFiles;

namespace Citect.TrnFilesTests
{
    [TestClass]
    public sealed class CitectTrnFileTests
    {
        [TestMethod]
        public void ReadHst()
        {
            var hstFile = CitectTrnFiles.ReadHst(@"data\Out.HST");

            Assert.Contains("CITECT", hstFile.Master.Title);
            Assert.Contains("VERSION 2", hstFile.Master.Title);
            Assert.Contains("TREND MASTER", hstFile.Master.Title);
            Assert.Contains("IO_AI_EX_0_Out", hstFile.Master.Title);
            Assert.Contains("Thu Apr  9 16:20:22 2026", hstFile.Master.Title);
            Assert.AreEqual("CITECT", hstFile.Master.Id);
            Assert.AreEqual(0, hstFile.Master.FileType);
            Assert.AreEqual(Versions.EightByteV600, hstFile.Master.Version);
            Assert.AreEqual(0, hstFile.Master.Mode);
            Assert.AreEqual(36, hstFile.Master.History);
            Assert.AreEqual(3, hstFile.Master.Files);
            Assert.AreEqual(0, hstFile.Master.NextFile);
            Assert.AreEqual(0, hstFile.Master.AddOn);

            Assert.HasCount(3, hstFile.Headers);
        }
    }
}
