using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_DEMIC_CONFIRM : Packet
    {
        public CSMG_DEM_DEMIC_CONFIRM()
        {
            this.offset = 2;
        }

        public byte Page
        {
            get
            {
                return GetByte(2);
            }
        }

        public short[,] Chips
        {
            get
            {
                short[,] res = new short[9, 9];
                offset = 4;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        res[j, i] = GetShort();
                    }
                }
                return res;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_DEMIC_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMDemicConfirm(this);
        }

    }
}