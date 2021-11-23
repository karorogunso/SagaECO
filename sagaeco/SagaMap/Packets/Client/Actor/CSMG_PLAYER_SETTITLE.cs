using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_SETTITLE : Packet
    {
        public CSMG_PLAYER_SETTITLE()
        {
            this.offset = 2;
        }        

        public uint GetTSubID
        {
            get
            {
                return GetUInt(3);
            }
        }
        public uint GetTConjID
        {
            get
            {
                return GetUInt(7);
            }
        }
        public uint GetTPredID
        {
            get
            {
                return GetUInt(11);
            }
        }
        public uint GetTBattleID
        {
            get
            {
                return GetUInt(15);
            }
        }

        public override Packet New()
        {
            return new CSMG_PLAYER_SETTITLE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerSetTitle(this);
        }

    }
}