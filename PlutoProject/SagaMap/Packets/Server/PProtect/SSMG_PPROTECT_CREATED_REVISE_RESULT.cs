using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CREATED_REVISE_RESULT : Packet
    {
        public SSMG_PPROTECT_CREATED_REVISE_RESULT()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x2364;
        }


        public void SetData(string name,string message,uint id, byte maxMember
            , byte unknown1, byte unknown2)
        {
            this.PutByte(unknown1);
            setString(name, this.offset);
            setString(message, this.offset);
            this.PutByte(unknown2);
            this.PutUInt(id);
            this.PutByte(maxMember);
        }


        void setString(string str,int i)
        {
            byte[] buf = Global.Unicode.GetBytes(str + "\0");
            byte[] buff = new byte[this.data.Length + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, i);
            this.PutBytes(buf, i + 1);
        }
    }
}

