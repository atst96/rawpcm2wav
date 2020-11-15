using System;
using System.Collections.Generic;
using System.Text;

namespace RawPCM2WAVTool
{
    internal enum WAVE_FORMAT_TYPE : ushort
    {
        /// <summary>
        /// PCM
        /// </summary>
        PCM = 0x0001,

        /// <summary>
        /// float
        /// </summary>
        IEEE_FLOAT = 0x0003,

        /// <summary>
        /// A-law
        /// </summary>
        ALAW = 0x0006,

        /// <summary>
        /// μ-law
        /// </summary>
        MULAW = 0x0007,

        /// <summary>
        /// サブフォーマットで定義
        /// </summary>
        EXTENSIBLE = 0xFFFE,
    }
}
