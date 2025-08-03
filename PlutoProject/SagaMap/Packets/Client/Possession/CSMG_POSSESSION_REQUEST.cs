using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_POSSESSION_REQUEST : Packet
    {
        public CSMG_POSSESSION_REQUEST()
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

        public PossessionPosition PossessionPosition
        {
            get
            {
                return (PossessionPosition)this.GetByte(6);
            }
        }

        public string Comment
        {
            get
            {
                byte len = this.GetByte(7);
                byte[] buf = this.GetBytes(len, 8);
                string tmp = Global.Unicode.GetString(buf);
                tmp = tmp.Replace("\0", "");
                return tmp;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_POSSESSION_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPossessionRequest(this);
        }

    }
}