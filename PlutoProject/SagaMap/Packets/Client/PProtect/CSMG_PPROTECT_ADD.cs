using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_ADD : Packet
    {
        public CSMG_PPROTECT_ADD()
        {
            this.offset = 2;
        }

        public uint PPID
        {
            get
            {
                return this.GetUInt(2);
            }
        }


        public string Password
        {
            get
            {
                byte inedx = this.GetByte(6);
                byte[] buf = this.GetBytes((ushort)(inedx - 1), 7);
                return Global.Unicode.GetString(buf);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_ADD();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPProtectADD(this);
        }

    }
}