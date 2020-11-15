using System;
using System.IO;
using System.Text;

namespace RawPCM2WAVTool
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var files = args;
            for (int idx = 0; idx < files.Length; ++idx)
            {
                var file = files[idx];
                Console.WriteLine($"File[{idx}]: { Path.GetFileName(file) }");

                if (!File.Exists(file))
                {
                    Console.WriteLine("ファイルが見つかりません。");
                    Console.WriteLine();
                    continue;
                }

                Convert(file);
            }
        }

        private static unsafe void Convert(string path)
        {
            var dir = Path.GetDirectoryName(path);

            var wavFileName = Path.GetFileNameWithoutExtension(path) + ".WAV";
            var wavPath = Path.Combine(dir, wavFileName);

            using (var outputStream = File.OpenWrite(wavPath))
            using (var waveWriter = new BinaryWriter(outputStream))
            using (var binReader = File.OpenRead(path))
            {
                uint fmtDataSize = (uint)sizeof(WAVEFORMAT);
                uint dataSize = (uint)binReader.Length;

                /**
                 * FMTチャンク
                 */
                var fmtData = new byte[fmtDataSize];
                fixed (byte* p_fmtData = fmtData)
                {
                    WAVEFORMAT* p_format = (WAVEFORMAT*)p_fmtData;
                    p_format->wFormatTag = WAVE_FORMAT_TYPE.PCM;
                    p_format->nChannels = 2;
                    p_format->nSamplesPerSec = 44100;
                    p_format->nAvgBytesPerSec = 176400;
                    p_format->nBlockAlign = 4;
                    p_format->nBitsPerSample = 16;
                }

                /**
                 * RIFチャンクの書き込み
                 */

                // フォーマット
                var format = Encoding.ASCII.GetBytes("RIFF");
                waveWriter.Write(format);

                // サイズ
                // フォームタイプ(4byte) + fmtチャンク((4 + 4) + fmtDataSize) + dataチャンク((4 + 4) + dataSize)
                uint riffSize = fmtDataSize + dataSize + 20;
                waveWriter.Write(riffSize);

                // フォームタイプ
                var formType = Encoding.ASCII.GetBytes("WAVE");
                waveWriter.Write(formType);

                /*
                 * FMTチャンク書き込み
                 */
                var fmtType = Encoding.ASCII.GetBytes("fmt ");
                waveWriter.Write(fmtType);
                waveWriter.Write(fmtDataSize);
                waveWriter.Write(fmtData);

                /**
                 * dataチャンクの書き込み
                 */

                // DATAチャンクの識別子
                var dataType = Encoding.ASCII.GetBytes("data");
                waveWriter.Write(dataType);
                waveWriter.Write(dataSize);

                waveWriter.Flush();

                binReader.CopyTo(outputStream);
            }
        }
    }
}
