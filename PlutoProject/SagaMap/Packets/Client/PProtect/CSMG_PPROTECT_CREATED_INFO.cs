using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_CREATED_INFO : Packet
    {
        public CSMG_PPROTECT_CREATED_INFO()
        {
            this.offset = 2;

        }

        void load()
        {

            this.offset = 2;
            byte inedx = this.GetByte();
            byte[] buf = this.GetBytes((ushort)(inedx-1));
            this.offset += 1;
            name = Global.Unicode.GetString(buf);
            inedx = this.GetByte();
            buf = this.GetBytes((ushort)(inedx - 1));
            this.offset += 1;
            message = Global.Unicode.GetString(buf);
            inedx = this.GetByte();
            buf = this.GetBytes((ushort)(inedx - 1));
            this.offset += 1;
            password = Global.Unicode.GetString(buf);
            taskID = this.GetUInt();
            maxMember = this.GetByte();
        }
        public string name;
        public byte maxMember;
        public string message;
        public string password;
        public uint taskID;
        
        


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_CREATED_INFO();
        }

        public override void Parse(SagaLib.Client client)
        {
            load();
            ((MapClient)(client)).OnPProtectCreated(this);
        }

    }
}