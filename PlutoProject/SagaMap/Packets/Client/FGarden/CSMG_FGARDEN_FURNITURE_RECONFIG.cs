using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FGARDEN_FURNITURE_RECONFIG : Packet
    {
        public CSMG_FGARDEN_FURNITURE_RECONFIG()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public short X
        {
            get
            {
                return this.GetShort(6);
            }
        }

        public short Y
        {
            get
            {
                return this.GetShort(8);
            }
        }

        public short Z
        {
            get
            {
                return this.GetShort(10);
            }
        }

        public ushort Dir
        {
            get
            {
                return this.GetUShort(12);
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FGARDEN_FURNITURE_RECONFIG();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFGardenFurnitureReconfig(this);
        }

    }
}