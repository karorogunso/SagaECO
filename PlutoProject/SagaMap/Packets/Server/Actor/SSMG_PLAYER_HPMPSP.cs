using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_HPMPSP : Packet
    {
        public SSMG_PLAYER_HPMPSP()
        {
            this.data = new byte[35];
            this.offset = 2;
            this.ID = 0x021C;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint HP
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9)
                {
                    //this.PutByte(4, 6);
                    this.PutByte(3, 6);
                }
                else
                {
                    this.PutByte(3, 6);
                }
                /*if (value.type == ActorType.MOB)
                    this.PutUInt(value.HP, 7);
                else*/
                this.PutUInt(0, 7);
                this.PutUInt(value, 11);
                //this.PutUInt(0, 15);
            }
        }

        public uint MP
        {
            set
            {
                this.PutUInt(0, 15);
                this.PutUInt(value, 19);
                //this.PutUInt(0, 23);
            }
        }

        public uint SP
        {
            set
            {
                this.PutUInt(0, 23);
                this.PutUInt(value, 27);
            }
        }

        public uint EP
        {
            set
            {
                this.PutUInt(value, 31);
            }
        }
    }
}
        
