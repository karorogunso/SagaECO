using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CREATED_ADD_RESULT_1 : Packet
    {
        public SSMG_PPROTECT_CREATED_ADD_RESULT_1()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x2366;
        }

        public List<ActorPC> List
        {
            set
            {
                int count = value.Count;
                if (count == 0)
                    return;

                byte[] buff = new byte[this.data.Length + count * 9];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)count, 4);
                //this.offset = 5;
                for (int i = 0; i < count; i++)
                {
                    setString(value[i].Name);
                }
                this.PutByte((byte)count);
                for (int i = 0; i < count; i++)
                {
                    if(value[i].Pet!=null)
                        this.PutUInt(value[i].Pet.PetID);
                    else
                        this.PutUInt(0);

                }
                this.PutByte((byte)count);
                for (int i = 0; i < count; i++)
                {
                    this.PutByte(0x0);//base lv
                }
                this.PutByte((byte)count);
                for (int i = 0; i < count; i++)
                {
                    this.PutByte(0x0);//转生状态
                }
                this.PutByte((byte)count);
                for (int i = 0; i < count; i++)
                {
                    this.PutByte(0x0);//rake？
                }
                this.PutByte((byte)count);
                for (int i = 0; i < count; i++)
                {
                    this.PutByte(0x0);//好感度？
                }
            }
        }

        void setString(string str)
        {
            byte[] buf = Global.Unicode.GetBytes(str);
            byte[] buff = new byte[this.data.Length + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size);
            this.PutBytes(buf);
        }
    }
}

