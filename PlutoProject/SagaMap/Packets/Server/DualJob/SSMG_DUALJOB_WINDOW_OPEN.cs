using SagaLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Packets.Server
{
    public class SSMG_DUALJOB_WINDOW_OPEN : Packet
    {
        int packetoffset = 0;
        public SSMG_DUALJOB_WINDOW_OPEN()
        {
            this.data = new byte[87];
            this.offset = 2;
            this.ID = 0x22CE;
        }

        public byte CanChange
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public void SetDualJobList(byte jobCount, byte[] jobSerialList)
        {
            this.PutByte(jobCount, 3);
            this.PutBytes(jobSerialList, 4);
            packetoffset = 4 + jobCount * 2;
        }

        public byte[] DualJobLevel
        {
            set
            {
                this.PutByte(0x0C, packetoffset);
                packetoffset++;
                this.PutBytes(value, packetoffset);
                packetoffset += 12;
            }
        }

        public byte CurrentDualJobSerial
        {
            set
            {
                this.PutUShort(value, packetoffset);
                packetoffset += 2;
            }
        }

        public List<SagaDB.Skill.Skill> CurrentSkillList
        {
            set
            {
                this.PutByte(byte.Parse(value.Count.ToString()), packetoffset++);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort((ushort)value[i].ID, packetoffset);
                    packetoffset += 2;
                }
            }
        }
    }
}
