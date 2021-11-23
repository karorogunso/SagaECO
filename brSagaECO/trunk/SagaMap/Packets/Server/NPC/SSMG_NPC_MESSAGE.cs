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
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.data = new byte[18];
            }
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
            {
                this.data = new byte[12];
            }
            else
                this.data = new byte[11];
            this.offset = 2;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.ID = 0x03F9;
            }
            else
                this.ID = 0x03F7;
        }

        public void SetMessage(uint npcID, byte num, string message, ushort motion, string title)
        {
            this.PutUInt(npcID, 2);
            ushort oldoffset;
            byte[] buf;
            byte[] buff;

            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.PutUInt(npcID, 2);

                List<string> messages = new List<string>();
                int index = message.IndexOf("$R");
                while (message.IndexOf("$R") > 0)
                {
                    messages.Add(message.Substring(0, index + 2));
                    message = message.Remove(0, index + 2);
                    index = message.IndexOf("$R");
                }
                messages.Add(message);

                oldoffset = 8;
                this.PutByte((byte)messages.Count, oldoffset++);
                for (int j = 0; j < messages.Count; j++)
                {
                    buf = Global.Unicode.GetBytes(messages[j]);
                    buff = new byte[buf.Length + this.data.Length + 1];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)(buf.Length), oldoffset++);
                    this.PutBytes(buf, oldoffset);
                    oldoffset += (byte)(buf.Length);
                }
                this.PutUShort(motion, oldoffset++);

                buf = Global.Unicode.GetBytes(title);
                buff = new byte[buf.Length + this.data.Length + 1];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                oldoffset++;
                this.PutByte((byte)(buf.Length + 1), oldoffset++);
                this.PutBytes(buf, oldoffset);
                oldoffset += (byte)(buf.Length + 1);
            }
            else
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                {
                    oldoffset = 7;
                    this.PutByte(0, 6);
                }
                else
                    oldoffset = 6;
                this.PutByte(num, oldoffset);
                buf = Global.Unicode.GetBytes(message);
                buff = new byte[buf.Length + this.data.Length + 1];
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
}

