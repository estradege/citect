using System.Runtime.InteropServices;

namespace Citect
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CtTagValueItems
    {
        public uint dwLength;
        public ulong nTimestamp;
        public ulong nValueTimestamp;
        public ulong nQualityTimestamp;
        public byte bQualityGeneral;
        public byte bQualitySubstatus;
        public byte bQualityLimit;
        public byte bQualityExtendedSubstatus;
        public uint nQualityDatasourceErrorCode;
        public bool bOverride;
        public bool bControlMode;

    }
}
