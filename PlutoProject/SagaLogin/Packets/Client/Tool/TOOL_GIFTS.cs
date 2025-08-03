using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class TOOL_GIFTS : Packet
    {
        public TOOL_GIFTS()
        {
            this.offset = 2;
        }

        public byte type
        {
            get
            {
                return GetByte(2);
            }
        }

        public string Title
        {
            get
            {
                byte size = GetByte(4);
                string buf = Global.Unicode.GetString(this.GetBytes(size, 5));
                return buf.Replace("\0", "");
            }
        }
        public string Sender
        {
            get
            {
                byte size = GetByte();
                string buf = Global.Unicode.GetString(GetBytes(size));
                return buf.Replace("\0", "");
            }
        }
        public string Content
        {
            get
            {
                byte size = GetByte();
                string buf = Global.Unicode.GetString(GetBytes(size));
                return buf.Replace("\0", "");
            }
        }
        public string CharIDs
        {
            get
            {
                byte size = GetByte();
                string buf = Global.Unicode.GetString(GetBytes(size));
                return buf.Replace("\0", "");
            }
        }
        public string GiftIDs
        {
            get
            {
                byte size = GetByte();
                string buf = Global.Unicode.GetString(GetBytes(size));
                return buf.Replace("\0", "");
            }
        }
        public string Days
        {
            get
            {
                byte size = GetByte();
                string buf = Global.Unicode.GetString(GetBytes(size));
                return buf.Replace("\0", "");
            }
        }

        public override SagaLib.Packet New()
        {
            return new TOOL_GIFTS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnGetGiftsRequest(this);
        }

    }
}