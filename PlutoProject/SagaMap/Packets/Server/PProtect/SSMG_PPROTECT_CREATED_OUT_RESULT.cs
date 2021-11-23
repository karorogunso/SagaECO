using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CREATED_OUT_RESULT : Packet
    {
        public SSMG_PPROTECT_CREATED_OUT_RESULT()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x236C;
        }


        public void SetName(string str)
        {
            byte[] buf = Global.Unicode.GetBytes(str + "\0");
            byte[] buff = new byte[4 + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, 3);
            this.PutBytes(buf, 4);
        }
    }
}

