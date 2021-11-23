using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_WRP_LIST : Packet
    {
        public SSMG_WRP_LIST()
        {
            this.data = new byte[9];
            this.ID = 0x0173;
        }

        public List<ActorPC> RankingList
        {
            set
            {
                byte[][] names = new byte[value.Count][];
                byte[] lvs = new byte[value.Count];
                byte[] jlvs = new byte[value.Count];
                byte[] jobs = new byte[value.Count];
                int[] wrps = new int[value.Count];
                uint[] types = new uint[value.Count];

                int count = 0;
                foreach (ActorPC i in value)
                {
                    names[count] = Global.Unicode.GetBytes(i.Name);
                    lvs[count] = i.Level;
                    jlvs[count] = i.CurrentJobLevel;
                    jobs[count] = (byte)i.Job;
                    wrps[count] = i.WRP;
                    if (count == 0)
                        types[count] = 10;
                    else if (count < 10)
                        types[count] = 1;
                    else
                        types[count] = 0;
                    count++;                    
                }
                int len = 0;
                foreach (byte[] i in names)
                {
                    len += i.Length;
                }
                this.data = new byte[9 + 16 * value.Count + len];
                this.ID = 0x0173;
                this.offset = 2;
                
                PutByte((byte)value.Count);
                for (int i = 1; i <= value.Count; i++)
                {
                    PutInt(i);
                }

                PutByte((byte)value.Count);
                foreach (byte[] i in names)
                {
                    PutByte((byte)i.Length);
                    PutBytes(i);
                }
                PutByte((byte)value.Count);
                foreach (byte i in lvs)
                {
                    PutByte(i);
                }
                PutByte((byte)value.Count);
                foreach (byte i in jlvs)
                {
                    PutByte(i);
                }
                PutByte((byte)value.Count);
                foreach (byte i in jobs)
                {
                    PutByte(i);
                }
                PutByte((byte)value.Count);
                foreach (int i in wrps)
                {
                    PutInt(i);
                }
                PutByte((byte)value.Count);
                foreach (uint i in types)
                {
                    PutUInt(i);
                }
            }
        }
    }
}

