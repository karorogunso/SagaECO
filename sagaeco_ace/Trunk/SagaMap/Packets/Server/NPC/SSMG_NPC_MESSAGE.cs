using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_MESSAGE : Packet
    {
        public SSMG_NPC_MESSAGE()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
            {
                this.data = new byte[12];
            }
            else
                this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x03F7;
        }

        public void SetMessage(uint npcID, byte num, string message, ushort motion, string title)
        {
            this.PutUInt(npcID, 2);
            ushort oldoffset;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
            {
                oldoffset = 7;
                this.PutByte(0, 6);
            }
            else
                oldoffset = 6;
            this.PutByte(num, oldoffset);
            byte[] buf = Global.Unicode.GetBytes(message);
            byte[] buff = new byte[buf.Length + this.data.Length + 1];
            this.data.CopyTo(buff, 0);
            this.data = buff;
            byte size = (byte)(buf.Length + 1);
            oldoffset++;
            this.PutByte(size, oldoffset);
            oldoffset++;
            this.PutBytes(buf, oldoffset);

            ushort offset = (ushort)(8 + size);
            if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
            {
                this.PutByte(0, offset);
                offset++;
            }
            this.PutUShort(motion, offset);

            buf = Global.Unicode.GetBytes(title);
            buff = new byte[buf.Length + this.data.Length + 1];
            this.data.CopyTo(buff, 0);
            this.data = buff;
            size = (byte)(buf.Length + 1);
            this.PutByte((byte)size, (ushort)(offset + 2));
            this.PutBytes(buf, (ushort)(offset + 3));
        }
    }
}

