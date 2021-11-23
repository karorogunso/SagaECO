using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_DETAIL : Packet
    {

        public SSMG_PARTY_MEMBER_DETAIL()
        {
            this.data = new byte[23];
            this.offset = 2;
            this.ID = 0x19F5;
        }

        public uint PartyIndex
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public byte Form
        {
            set
            {
                if(Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutByte(value, 10);
            }
        }

        public PC_JOB Job
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutUInt((uint)value, 11);
                else
                    this.PutUInt((uint)value, 10);
            }
        }

        public byte Level
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutUInt((uint)value, 15);
                else
                    this.PutUInt((uint)value, 14);
            }
        }

        public byte JobLevel
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutUInt((uint)value, 19);
                else
                    this.PutUInt((uint)value, 18);
            }
        }
    }
}

