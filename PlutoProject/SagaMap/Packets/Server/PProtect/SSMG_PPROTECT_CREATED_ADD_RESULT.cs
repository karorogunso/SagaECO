using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CREATED_ADD_RESULT : Packet
    {
        public SSMG_PPROTECT_CREATED_ADD_RESULT()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x2368;
        }


        public void SetData(string name,string password, byte errid
            , byte unknown1, byte unknown2)
        {
            this.PutByte(errid);
            if(string.IsNullOrEmpty(name))
                this.PutByte(0);
            else
                setString(name, this.offset);
            if (string.IsNullOrEmpty(password))
                this.PutByte(0);
            else
                setString(password, this.offset);
            this.PutByte(unknown1);
            this.PutByte(unknown2);
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

