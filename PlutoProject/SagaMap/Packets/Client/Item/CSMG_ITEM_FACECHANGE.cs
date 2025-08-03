using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_FACECHANGE : Packet
    {
        public CSMG_ITEM_FACECHANGE()
        {
            this.offset = 2;
        }
        public uint SlotID
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public ushort FaceID
        {
            get
            {
                return this.GetUShort(6);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_FACECHANGE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerFaceChange(this);
        }

    }
}