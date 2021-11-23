using SagaLib;
using SagaLogin.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaLogin.Packets.Client
{
    public class CSMG_TOOL_GIFTS : Packet
    {
        public CSMG_TOOL_GIFTS()
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
            return new CSMG_TOOL_GIFTS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnGetGiftsRequest(this);
        }

    }
}
