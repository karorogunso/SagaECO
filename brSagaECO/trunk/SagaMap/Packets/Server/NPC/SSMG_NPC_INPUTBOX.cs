using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum InputType
    {
        Bank = 2,
        ItemCode,
        PetRename
    }
}

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_INPUTBOX : Packet
    {
        public SSMG_NPC_INPUTBOX()
        {
            this.data = new byte[7];
            this.offset = 2;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.ID = 0x05F4;
            else 
                this.ID = 0x05F0;
        }

        public string Title
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[7 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, 2);
                this.PutBytes(buf, 3);
            }
        }

        public InputType Type
        {
            set
            {
                byte offset = this.GetByte(2);
                this.PutInt((int)value, (ushort)(3 + offset));
            }
        }
    }
}

