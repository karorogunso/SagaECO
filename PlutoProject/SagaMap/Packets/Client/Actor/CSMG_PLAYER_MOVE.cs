using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_MOVE : Packet
    {
        public CSMG_PLAYER_MOVE()
        {
            this.data = new byte[10];
            this.ID = 0x11F8;
            this.offset = 2;
        }

        public short X
        {
            get
            {
                return this.GetShort(2);
            }
            set
            {
                this.PutShort(value, 2);
            }
        }

        public short Y
        {
            get
            {
                return this.GetShort(4);
            }
            set
            {
                this.PutShort(value, 4);
            }
        }

        public ushort Dir
        {
            get
            {
                return this.GetUShort(6);
            }
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public MoveType MoveType
        {
            get
            {
                return (MoveType)this.GetUShort(8);
            }
            set
            {
                this.PutUShort((ushort)value, 8);
            }
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_MOVE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMove(this);
        }

    }
}