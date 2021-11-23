using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_RING_EMBLEM_UPLOAD : Packet
    {
        public CSMG_RING_EMBLEM_UPLOAD()
        {
            this.offset = 2;
        }

        public byte[] Data
        {
            get
            {
                if (GetInt(3) == 0xFD)
                {
                    int len = this.GetInt(7);
                    byte[] buf = this.GetBytes((ushort)len, 11);
                    return buf;
                }
                else
                {
                    int len = this.GetInt(3);
                    byte[] buf = this.GetBytes((ushort)len, 7);
                    return buf;
                }
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_RING_EMBLEM_UPLOAD();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRingEmblemUpload(this);
        }

    }
}