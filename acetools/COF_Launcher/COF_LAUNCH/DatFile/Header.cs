using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace COF_LAUNCH
{
    public unsafe class Header
    {
        public string filename;
        public int offset;
        public int packsize;
        public int unpacksize;
        public int index;
        public string datafile;
        public byte[] filecontent;

        [DllImport("Unpack.dll")]
        static extern int Pack(byte[] src, int srcSize, byte** dest, out int destSize, int dw, int dw2);
        [DllImport("Unpack.dll")]
        static extern int Unpack(byte[] src, int srcSize, byte** dest, out int destSize, int dw);
        [DllImport("Unpack.dll")]
        static extern int PackFree(byte** dest);

        public static bool Unpack(byte[] src, byte[] dst)
        {
            fixed (byte* ptr = dst)
            {
                byte* tmp = ptr;
                int dstSize = dst.Length;
                int res = Unpack(src, src.Length, &tmp, out dstSize, 1);
                return (res == 1);
            }
        }

        public static bool Pack(byte[] src, out byte[] dst)
        {
            byte* pDst;
            int dstSize;
            int res = Pack(src, src.Length, &pDst, out dstSize, 1, 0);
            if (res == 1)
            {
                dst = new byte[dstSize];
                fixed (byte* ptr = dst)
                {
                    int* data = (int*)pDst;
                    int* dataDst = (int*)ptr;
                    int time = dstSize / 4;
                    for (int i = 0; i < time; i++)
                    {
                        dataDst[i] = data[i];
                    }
                    for (int i = 0; i < dstSize % 4; i++)
                    {
                        ptr[time * 4 + i] = pDst[time * 4 + i];
                    }
                }
            }
            else
                dst = new byte[0];
            //PackFree(&pDst);
            return (res == 1);
        }

    }

}
