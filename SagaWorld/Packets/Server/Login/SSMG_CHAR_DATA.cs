using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SagaLib;

using SagaDB.Actor;

namespace SagaWorld.Packets.Server
{
    public class SSMG_CHAR_DATA : Packet
    {
        public SSMG_CHAR_DATA()
        {
            this.data = new byte[86];
            this.offset = 2;
            this.ID = 0x28;
        }

        private void SetName(string name, int index)
        {
            int current = 0;
            int offset = 3;
            this.PutByte(3, 2);
            while (current < index)
            {
                byte size;
                size = this.GetByte((ushort)offset);
                offset = offset + size + 1;
                current++;
            }
            byte[] buf;
            byte[] buff;
            buf = System.Text.Encoding.UTF8.GetBytes(name);
            this.PutByte((byte)buf.Length, (ushort)offset);
            offset++;
            buff = new byte[this.data.Length + buf.Length];
            Array.Copy(this.data, 0, buff, 0, offset);
            Array.Copy(this.data, offset, buff, offset + buf.Length, this.data.Length - offset);
            this.data = buff;
            this.PutBytes(buf, (ushort)offset);
            this.SetUnkown();
        }

        private ushort GetDataOffset()
        {
            ushort offset = 3;
            for (int i = 0; i < 3; i++)
            {
                byte size;
                size = this.GetByte((ushort)offset);
                offset = (ushort)(offset + size + 1);
            }
            return offset;
        }

        private void SetRace(byte race, ushort index)
        {
            ushort offset = this.GetDataOffset();
            this.PutByte(3, offset);
            this.PutByte(race, (ushort)(offset + index + 1));
        }

        private void SetGender(byte gender, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 4);
            this.PutByte(3, offset);
            this.PutByte(gender, (ushort)(offset + index + 1));
        }

        private void SetHairStyle(byte hair, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 8);
            this.PutByte(3, offset);
            this.PutByte(hair, (ushort)(offset + index + 1));
        }

        private void SetHairColor(byte color, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 12);
            this.PutByte(3, offset);
            this.PutByte(color, (ushort)(offset + index + 1));
        }

        //Default by 0xff
        private void SetWig(byte wig, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 16);
            this.PutByte(3, offset);
            this.PutByte(wig, (ushort)(offset + index + 1));
        }

        private void SetIfExist(bool exist, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 20);
            this.PutByte(3, offset);
            if (exist)
                this.PutByte(0xff, (ushort)(offset + index + 1));
            else
                this.PutByte(0x00, (ushort)(offset + index + 1));
        }

        private void SetFace(byte face, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 24);
            this.PutByte(3, offset);
            this.PutByte(face, (ushort)(offset + index + 1));
        }

        private void SetUnkown()
        {
            ushort offset = (ushort)(this.GetDataOffset() + 28);
            this.PutUInt(0x03000000, offset);
            this.PutUInt(0x03000000, (ushort)(offset + 4));
            this.PutUInt(0x03000000, (ushort)(offset + 8));
        }

        private void SetJob(byte job, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 40);
            this.PutByte(3, offset);
            this.PutByte(job, (ushort)(offset + index + 1));
        }

        private void SetMap(uint map, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 44);
            this.PutByte(3, offset);
            this.PutUInt(map, (ushort)(offset + (index * 4) + 1));
        }

        private void SetLv(byte lv, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 57);
            this.PutByte(3, offset);
            this.PutByte(lv, (ushort)(offset + index + 1));
        }

        private void SetJob1(byte job1, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 61);
            this.PutByte(3, offset);
            this.PutByte(job1, (ushort)(offset + index + 1));
        }

        private void SetQuestRemaining(ushort quest, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 65);
            this.PutByte(3, offset);
            this.PutUShort(quest, (ushort)(offset + (index * 2) + 1));
        }

        private void SetJob2X(byte job2x, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 72);
            this.PutByte(3, offset);
            this.PutByte(job2x, (ushort)(offset + index + 1));
        }

        private void SetJob2T(byte job2t, ushort index)
        {
            ushort offset = (ushort)(this.GetDataOffset() + 76);
            this.PutByte(3, offset);
            this.PutByte(job2t, (ushort)(offset + index + 1));
        }

        public List<ActorPC> Chars
        {
            set
            {
                if (value.Count == 0)
                {
                    this.SetName("", 0);
                    this.SetRace(0, 0);
                    this.SetGender(0, 0);
                    this.SetHairStyle(0, 0);
                    this.SetHairColor(0, 0);
                    this.SetWig(0, 0);
                    this.SetIfExist(false, 0);
                    this.SetFace(0, 0);
                    this.SetJob(0, 0);
                    this.SetMap(0, 0);
                    this.SetLv(0, 0);
                    this.SetJob1(0, 0);
                    this.SetQuestRemaining(0, 0);
                    this.SetJob2X(0, 0);
                    this.SetJob2T(0, 0);
                }
                for (int i = 0; i < 3; i++)
                {
                    var pcs = 
                        from p in value 
                        where p.Slot == i 
                        select p;
                    if (pcs.Count() == 0)
                        continue;
                    ActorPC pc = pcs.First();
                    this.SetName(pc.Name, (ushort)i);
                    this.SetRace((byte)pc.Race, (ushort)i);
                    this.SetGender((byte)pc.Gender, (ushort)i);
                    this.SetHairStyle(pc.HairStyle, (ushort)i);
                    this.SetHairColor(pc.HairColor, (ushort)i);
                    this.SetWig(pc.Wig, (ushort)i);
                    this.SetIfExist(true, (ushort)i);
                    this.SetFace(pc.Face, (ushort)i);
                    this.SetJob((byte)pc.Job, (ushort)i);
                    this.SetMap(pc.MapID, (ushort)i);
                    this.SetLv(pc.Level, (ushort)i);
                    this.SetJob1(pc.JobLevel1, (ushort)i);
                    this.SetQuestRemaining(pc.QuestRemaining, (ushort)i);
                    this.SetJob2X(pc.JobLevel2X, (ushort)i);
                    this.SetJob2T(pc.JobLevel2T, (ushort)i);
                }
            }
        }
    }
}

