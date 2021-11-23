using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_GACHA_DRAW : Packet
    {
        public CSMG_IRIS_GACHA_DRAW()
        {
            this.offset = 2;
        }

        public uint PayFlag
        {
            get
            {
                return GetUInt(2);
            }
        }

        public uint SessionID
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public uint ItemID
        {
            get
            {
                return GetUInt(10);
            }
        }

        public override Packet New()
        {
            return new CSMG_IRIS_GACHA_DRAW();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisGacha(this);
        }

    }
}