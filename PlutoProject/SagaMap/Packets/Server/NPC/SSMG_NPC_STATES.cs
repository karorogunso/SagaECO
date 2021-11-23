using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_STATES : Packet
    {
        public SSMG_NPC_STATES()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x05E2;
        }


        public void PutNPCStates(Dictionary<uint, bool> NPCStates)
        {
            List<uint> npcs = new List<uint>(NPCStates.Keys);
            List<bool> states = new List<bool>(NPCStates.Values);

            byte[] buf = new byte[(byte)(this.data.Length + NPCStates.Count * 5 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;

            this.PutByte((byte)NPCStates.Count, offset);
            offset++;
            foreach (uint npc in npcs)
            {
                this.PutUInt(npc, offset);
                offset += 4;
            }
            this.PutByte((byte)NPCStates.Count, offset);
            offset++;
            foreach (bool visible in states)
            {
                if (visible)
                    this.PutByte(0x01, offset);
                else
                    this.PutByte(0x00, offset);
                offset++;
            }
        }
    }
}

