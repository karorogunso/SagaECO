using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FGARDEN_FURNITURE_SETUP : Packet
    {
        public CSMG_FGARDEN_FURNITURE_SETUP()
        {
            this.offset = 2;
        }

        public uint InventorySlot
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
                return GetShort(6);
            }
        }

        public short Y
        {
            get
            {
                return GetShort(8);
            }
        }

        public short Z
        {
            get
            {
                return GetShort(10);
            }
        }

        public short AxleX
        {
            get
            {
                return GetShort(12);
            }
        }
        public short AxleY
        {
            get
            {
                return GetShort(14);
            }
        }

        public short AxleZ
        {
            get
            {
                return GetShort(16);
            }
        }        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FGARDEN_FURNITURE_SETUP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFGardenFurnitureSetup(this);
        }

    }
}