using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SagaLib;

using SagaDB.Actor;

namespace SagaLogin.Packets.Server
{
    public class SSMG_CHAR_DATA : Packet
    {
        public SSMG_CHAR_DATA()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[156];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[131];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[113];
            else
                this.data = new byte[86];
            this.offset = 2;
            this.ID = 0x28;
        }

        private void SetName(string name, int index)
        {
            int current = 0;
            int offset = 3;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.PutByte(4, 2);
            else
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
            //this.SetUnkown();
        }

        private ushort GetDataOffset()
        {
            ushort offset = 3;
            int count;
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                count = 4;
            else
                count = 3;
            for (int i = 0; i < count; i++)
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
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.PutByte(4, offset);
            else
                this.PutByte(3, offset);
            this.PutByte(race, (ushort)(offset + index + 1));
        }

        private void SetForm(byte form, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 5);
                this.PutByte(4, offset);
                this.PutByte(form, (ushort)(offset + index + 1));
            }
        }

        private void SetGender(byte gender, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 10);
                this.PutByte(4, offset);
                this.PutByte(gender, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 4);
                this.PutByte(3, offset);
                this.PutByte(gender, (ushort)(offset + index + 1));
            }
        }

        private void SetHairStyle(ushort hair, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 15);
                this.PutByte(4, offset);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(hair, (ushort)(offset + index * 2 + 1));
                else
                    this.PutUShort(hair, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 8);
                this.PutByte(3, offset);
                this.PutUShort(hair, (ushort)(offset + index + 1));
            }
        }

        private void SetHairColor(byte color, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 24) : (ushort)(this.GetDataOffset() + 20);
                this.PutByte(4, offset);
                this.PutByte(color, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 12);
                this.PutByte(3, offset);
                this.PutByte(color, (ushort)(offset + index + 1));
            }
        }

        //Default by 0xff
        private void SetWig(ushort wig, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 29) : (ushort)(this.GetDataOffset() + 25);
                this.PutByte(4, offset);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(wig, (ushort)(offset + index * 2 + 1));
                else
                    this.PutByte((byte)wig, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 16);
                this.PutByte(3, offset);
                this.PutByte((byte)wig, (ushort)(offset + index + 1));
            }
        }

        private void SetIfExist(bool exist, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 38) : (ushort)(this.GetDataOffset() + 30);
                this.PutByte(4, offset);
                if (exist)
                    this.PutByte(0xff, (ushort)(offset + index + 1));
                else
                    this.PutByte(0x00, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 20);
                this.PutByte(3, offset);
                if (exist)
                    this.PutByte(0xff, (ushort)(offset + index + 1));
                else
                    this.PutByte(0x00, (ushort)(offset + index + 1));
            }
        }

        private void SetFace(ushort face, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 43) : (ushort)(this.GetDataOffset() + 35);
                this.PutByte(4, offset);
                this.PutUShort(face, (ushort)(offset + (index * 2 + 1)));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 24);
                this.PutByte(3, offset);
                this.PutUShort(face, (ushort)(offset + index + 1));
            }
        }

        private void SetRebirthFlag(byte unknown, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 53);
                this.PutByte(4, offset);
                this.PutByte(unknown, (ushort)(offset + index + 1));
            }
        }

        private void SetTail(byte tail, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 53);
                this.PutByte(4, offset);
                this.PutByte(tail, (ushort)(offset + index + 1));
            }
        }

        private void SetWing(byte wing, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 58);
                this.PutByte(4, offset);
                this.PutByte(wing, (ushort)(offset + index + 1));
            }
        }

        private void SetWingColor(byte wingcolor, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 63);
                this.PutByte(4, offset);
                this.PutByte(wingcolor, (ushort)(offset + index + 1));
            }
        }

        private void SetUnkown()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 53) : (ushort)(this.GetDataOffset() + 40);
                this.PutByte(4, offset);
                this.PutUInt(0, (ushort)(offset + 1));
                this.PutByte(4, (ushort)(offset + 5));
                this.PutUInt(0, (ushort)(offset + 6));
                this.PutByte(4, (ushort)(offset + 10));
                this.PutUInt(0, (ushort)(offset + 11));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 28);
                this.PutUInt(0x03000000, offset);
                this.PutUInt(0x03000000, (ushort)(offset + 4));
                this.PutUInt(0x03000000, (ushort)(offset + 8));
            }
        }

        private void SetJob(byte job, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 68) : (ushort)(this.GetDataOffset() + 55);
                this.PutByte(4, offset);
                this.PutByte(job, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 40);
                this.PutByte(3, offset);
                this.PutByte(job, (ushort)(offset + index + 1));
            }
        }

        private void SetMap(uint map, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 73) : (ushort)(this.GetDataOffset() + 60);
                this.PutByte(4, offset);
                this.PutUInt(map, (ushort)(offset + (index * 4) + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 44);
                this.PutByte(3, offset);
                this.PutUInt(map, (ushort)(offset + (index * 4) + 1));
            }
        }

        private void SetLv(byte lv, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 90) : (ushort)(this.GetDataOffset() + 77);
                this.PutByte(4, offset);
                this.PutByte(lv, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 57);
                this.PutByte(3, offset);
                this.PutByte(lv, (ushort)(offset + index + 1));
            }
        }

        private void SetJob1(byte job1, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 95) : (ushort)(this.GetDataOffset() + 82);
                this.PutByte(4, offset);
                this.PutByte(job1, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 61);
                this.PutByte(3, offset);
                this.PutByte(job1, (ushort)(offset + index + 1));
            }
        }

        private void SetQuestRemaining(ushort quest, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 100) : (ushort)(this.GetDataOffset() + 87);
                this.PutByte(4, offset);
                this.PutUShort(quest, (ushort)(offset + (index * 2) + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 65);
                this.PutByte(3, offset);
                this.PutUShort(quest, (ushort)(offset + (index * 2) + 1));
            }
        }

        private void SetJob2X(byte job2x, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 109) : (ushort)(this.GetDataOffset() + 96);
                this.PutByte(4, offset);
                this.PutByte(job2x, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 72);
                this.PutByte(3, offset);
                this.PutByte(job2x, (ushort)(offset + index + 1));
            }
        }

        private void SetJob2T(byte job2t, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort offset = Configuration.Instance.Version >= SagaLib.Version.Saga11 ? (ushort)(this.GetDataOffset() + 114) : (ushort)(this.GetDataOffset() + 101);
                this.PutByte(4, offset);
                this.PutByte(job2t, (ushort)(offset + index + 1));
            }
            else
            {
                ushort offset = (ushort)(this.GetDataOffset() + 76);
                this.PutByte(3, offset);
                this.PutByte(job2t, (ushort)(offset + index + 1));
            }
        }

        private void SetJob3(byte job3, ushort index)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
            {
                ushort offset = (ushort)(this.GetDataOffset() + 119);
                this.PutByte(4, offset);
                this.PutByte(job3, (ushort)(offset + index + 1));
            }
        }

        public List<ActorPC> Chars
        {
            set
            {
                if (value.Count == 0)
                {
                    this.SetName("", 0);
                    this.SetRace(0, 0);
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        this.SetForm(0, 0);
                    this.SetGender(0, 0);
                    this.SetHairStyle(0, 0);
                    this.SetHairColor(0, 0);
                    this.SetWig(0, 0);
                    this.SetIfExist(false, 0);
                    this.SetFace(0, 0);
                    this.SetRebirthFlag(0, 0);
                    this.SetTail(0, 0);
                    this.SetWing(0, 0);
                    this.SetWingColor(0, 0);
                    this.SetJob(0, 0);
                    this.SetMap(0, 0);
                    this.SetLv(0, 0);
                    this.SetJob1(0, 0);
                    this.SetQuestRemaining(0, 0);
                    this.SetJob2X(0, 0);
                    this.SetJob2T(0, 0);
                    this.SetJob3(0, 0);
                }
                int count;
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    count = 4;
                else
                    count = 3;
                for (int i = 0; i < count; i++)
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
                    if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                        this.SetForm((byte)pc.Form, (ushort)i);
                    this.SetGender((byte)pc.Gender, (ushort)i);
                    this.SetHairStyle(pc.HairStyle, (ushort)i);
                    this.SetHairColor(pc.HairColor, (ushort)i);
                    this.SetWig(pc.Wig, (ushort)i);
                    this.SetIfExist(true, (ushort)i);
                    this.SetFace(pc.Face, (ushort)i);
                    byte rblv = 0x00;
                    if (pc.Rebirth)
                        rblv = 0x64;
                    else
                        rblv = 0x00;
                    this.SetRebirthFlag(rblv, (ushort)i);
                    this.SetTail(pc.TailStyle, (ushort)i);
                    this.SetWing(pc.WingStyle, (ushort)i);
                    this.SetWingColor(pc.WingColor, (ushort)i);
                    this.SetJob((byte)pc.Job, (ushort)i);
                    this.SetMap(pc.MapID, (ushort)i);
                    this.SetLv(pc.Level, (ushort)i);
                    this.SetJob1(pc.JobLevel1, (ushort)i);
                    this.SetQuestRemaining(pc.QuestRemaining, (ushort)i);
                    this.SetJob2X(pc.JobLevel2X, (ushort)i);
                    this.SetJob2T(pc.JobLevel2T, (ushort)i);
                    this.SetJob3(pc.JobLevel3, (ushort)i);//Job3 level
                }
            }
        }
    }
}

