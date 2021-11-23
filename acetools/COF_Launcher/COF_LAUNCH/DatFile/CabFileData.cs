using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace COF_LAUNCH
{
    struct CabFileData
    {
        public IntPtr CabFileName;
        public Int32 CabFileSize;
        public Int32 CabError;
        public Int16 CabDate;
        public Int16 CabTime;
        public Int16 CabAttr;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string FullTargetName;
    }
    public class ExtractCab
    {
        delegate int SetupIterateCabinetCallback(int ConData, int Notift, IntPtr Param1, IntPtr Param2);

        [DllImport("setupapi.dll")]
        static extern uint SetupIterateCabinetA(string CabinetFile, int Reserved, SetupIterateCabinetCallback callback, int context);

        static int DoCallback(int ConData, int Notify, IntPtr Param1, IntPtr Param2)
        {
            int ret = 0;
            CabFileData data = (CabFileData)Marshal.PtrToStructure(Param1,typeof(CabFileData));
            if (Notify == 17)
            {
                string GetCabFile = Marshal.PtrToStringAnsi(data.CabFileName);
                data.FullTargetName = "temp\\" + GetCabFile + '\0';
                Marshal.StructureToPtr(data, Param1, true);
                ret = 1;
            }
            CabFileData data2 = (CabFileData)Marshal.PtrToStructure(Param2, typeof(CabFileData));
            if (Notify == 17)
            {
                string GetCabFile = Marshal.PtrToStringAnsi(data2.CabFileName);
                data2.FullTargetName = "temp\\" + GetCabFile + '\0';
                Marshal.StructureToPtr(data2, Param2, true);
                ret = 1;
            }
            return ret;
        }

        public uint SetupIterateCabinet(string CabinetFile)
        {
            return SetupIterateCabinetA(CabinetFile, 0, DoCallback, 0);
        }
    }
}
