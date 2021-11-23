using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_CARD_ASSEMBLE_CONFIRM : Packet
    {
        public CSMG_IRIS_CARD_ASSEMBLE_CONFIRM()
        {
            this.offset = 2;
        }

        public uint CardID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public uint SupportItem
        {
            get
            {
                return this.GetUInt(6);
            }
        }


        public uint ProtectItem
        {
            get
            {
                return this.GetUInt(10);
            }
        }


        public byte BaseLevel
        {
            get
            {
                return this.GetByte(14);
            }
        }

        public byte JobLevel
        {
            get
            {
                return this.GetByte(15);
            }
        }

        public ushort ExpRate
        {
            get
            {
                return this.GetUShort(16);
            }
        }

        public ushort JExpRate
        {
            get
            {
                return this.GetUShort(18);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_IRIS_CARD_ASSEMBLE_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisCardAssemble(this);
        }

    }
}