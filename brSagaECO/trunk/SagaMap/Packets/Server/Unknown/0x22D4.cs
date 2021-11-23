using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_0x22D4 : Packet
    {
        public SSMG_0x22D4()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x22D4;   
        }

        public void PutStates(Dictionary<ushort,bool> States)
        {
            List<ushort> id = new List<ushort>(States.Keys);
            List<bool> states = new List<bool>(States.Values);

            byte[] buf = new byte[(byte)(this.data.Length + States.Count * 3 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;
            this.PutByte((byte)States.Count);
            this.offset++;
            foreach (ushort ID in id)
            {
                this.PutUShort(ID, offset);
                offset += 2;
            }
            this.PutByte((byte)States.Count);
            this.offset++;
            foreach (bool enabled in states)
            {
                if (enabled)
                    this.PutByte(0x01, offset);
                else
                    this.PutByte(0x00, offset);
                offset++;
            }
        }
    }
}

