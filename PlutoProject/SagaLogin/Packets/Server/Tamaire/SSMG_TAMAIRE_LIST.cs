using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Tamaire;
using SagaLib;
using SagaDB.ECOShop;

namespace SagaLogin.Packets.Server
{
    public class SSMG_TAMAIRE_LIST : Packet
    {
        public SSMG_TAMAIRE_LIST()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x0227;
        }
         
        public void PutData (List<TamaireLending> data, byte baselv)
        {
            byte[] buf = new byte[this.data.Length + data.Count*4];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            offset = 3;
            this.PutByte((byte)data.Count, offset);
            for (int i = 0; i < data.Count; i++)
                this.PutUInt(data[i].Lender, offset);

            buf = new byte[this.data.Length + data.Count];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.PutByte((byte)data.Count, offset);
            for (int i = 0; i < data.Count; i++)
                this.PutByte(data[i].Baselv, offset);

            buf = new byte[this.data.Length + data.Count];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.PutByte((byte)data.Count, offset);
            for (int i = 0; i < data.Count; i++)
                this.PutByte((byte)(data[i].JobType), offset);
            
            this.PutByte((byte)data.Count, offset);
            byte[] name;
            int size;
            for (int i = 0; i < data.Count; i++)
            {
                name = Global.Unicode.GetBytes(LoginServer.charDB.GetChar(data[i].Lender).Name);
                size = name.Length;
                buf = new byte[this.data.Length + size + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)size, offset);
                this.PutBytes(name, offset);
            }

            this.PutByte((byte)data.Count, offset);
            byte[] comment;
            for (int i = 0; i < data.Count; i++)
            {
                comment = Global.Unicode.GetBytes(data[i].Comment);
                size = comment.Length;
                buf = new byte[this.data.Length + size + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)size, offset);
                this.PutBytes(comment, offset);
            }
            

            this.PutByte((byte)data.Count, offset);
            buf = new byte[this.data.Length + data.Count * 2];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            for (int i = 0; i < data.Count; i++)
            {
                int leveldiff = data[i].Baselv - baselv;
                if (leveldiff < 0)
                    leveldiff = -leveldiff;
                if (leveldiff ==0)
                    this.PutShort(0, offset);
                if (leveldiff >=1 && leveldiff <=5)
                    this.PutShort(10, offset);
                if (leveldiff >= 6 && leveldiff <= 10)
                    this.PutShort(30, offset);
                if (leveldiff >= 11 && leveldiff <= 15)
                    this.PutShort(50, offset);
                if (leveldiff >= 16 && leveldiff <= 20)
                    this.PutShort(100, offset);
                if (leveldiff >= 20 && leveldiff <= 105)
                    this.PutShort((short)((leveldiff-20)/5*50+100), offset);
                if (leveldiff >= 106 && leveldiff <= 109)
                    this.PutShort(980, offset);
            }
        }
    }
}
