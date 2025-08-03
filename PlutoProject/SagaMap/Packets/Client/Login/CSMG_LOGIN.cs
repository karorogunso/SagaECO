using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_LOGIN : Packet
    {
        public string UserName;
        public string Password;
        public string MacAddress;
        public CSMG_LOGIN()
        {
            this.size = 55;
            this.offset = 8;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_LOGIN();
        }

        public void GetContent()
        {
            byte size;
            ushort offset = 2;
            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            size = this.GetByte(offset);
            offset++;
            this.UserName = enc.GetString(this.GetBytes((ushort)(size - 1), offset));
            offset += size;
            size = this.GetByte(offset);
            offset++;
            this.Password = enc.GetString(this.GetBytes((ushort)(size - 1), offset));
            offset++;
            offset += size;
            ushort a = GetUShort(offset);
            offset += 2;
            uint b = GetUInt(offset);
            MacAddress = Convert.ToString(a, 16) + Convert.ToString(b, 16);
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnLogin(this);
        }

    }
}