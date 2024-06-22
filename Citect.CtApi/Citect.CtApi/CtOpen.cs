using System;

namespace Citect
{
    /// <summary>
    /// The mode of the Cicode call.
    /// </summary>
    [Flags]
    public enum CtOpen
    {
        /// <summary>
        /// Reopen connection on error or communication interruption. If the connection to Plant SCADA is lost CTAPI will continue to retry to connect to Plant SCADA.
        /// </summary>
        Reconnect = 0x00000002,

        /// <summary>
        /// Open the CTAPI in read only mode. This allows read only access to data - you cannot write to any variable in Plant SCADA or call any Cicode function.
        /// </summary>
        ReadOnly = 0x00000004,

        /// <summary>
        /// Disables the display of message boxes when an error occurs.
        /// </summary>
        Batch = 0x00000008,

        /// <summary>
        /// Allows for encrypted communication. This parameter is implicitly set when encryption is enabled (with the Accept encrypted and non-encrypted connections options not selected) via the Configurator.
        /// </summary>
        Extended = 0x00000010,

        /// <summary>
        /// Indicates that User and Password are Windows user credentials. If User and Password are blank, then the credentials of the Windows user that is launching the application will be used.
        /// </summary>
        WindowUser = 0x00000020
    }
}
