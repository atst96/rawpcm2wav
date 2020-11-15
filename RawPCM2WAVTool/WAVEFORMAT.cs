using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RawPCM2WAVTool
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct WAVEFORMAT
    {
        /// <summary>
        /// フォーマット
        /// </summary>
        [FieldOffset(0)]
        public WAVE_FORMAT_TYPE wFormatTag;
        
        /// <summary>
        /// チャンネル数
        /// </summary>
        [FieldOffset(2)]
        public ushort nChannels;

        /// <summary>
        /// サンプリングレート
        /// </summary>
        [FieldOffset(4)]
        public uint nSamplesPerSec;

        /// <summary>
        /// 平均データ転送速度
        /// </summary>
        [FieldOffset(8)]
        public uint nAvgBytesPerSec;

        /// <summary>
        /// ブロックアライメント
        /// </summary>
        [FieldOffset(12)]
        public ushort nBlockAlign;

        /// <summary>
        /// サンプルあたりのビット数
        /// </summary>
        [FieldOffset(14)]
        public ushort nBitsPerSample;
    }
}
