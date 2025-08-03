using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CHAT_INFO : Packet
    {
        public SSMG_PPROTECT_CHAT_INFO()
        {
            this.data = new byte[17];
            this.offset = 2;
            this.ID = 0x236D;
        }


        public void SetData(ActorPC pc,byte id
            , byte unknown1, byte unknown2, byte unknown3, byte unknown4, byte unknown5)
        {
            this.PutUInt(pc.CharID);
            this.PutByte(id);
            setString(pc.Name, this.offset);
            if(pc.Pet!= null)
            {
                this.PutUInt(pc.Pet.PetID);
            }
            else
            {
                this.offset += 4;
            }
            this.PutByte(unknown1);
            this.PutByte(unknown2);
            this.PutByte(unknown3);
            this.PutByte(unknown4);
            this.PutByte(unknown5);
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

