using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_BBS_POST : Packet
    {
        public CSMG_COMMUNITY_BBS_POST()
        {
            this.offset = 2;
        }

        public string Title
        {
            get
            {
                byte len = GetByte(2);
                return Global.Unicode.GetString(GetBytes(len, 3)).Replace("\0", "");
            }
        }

        public string Content
        {
            get
            {
                byte offset = GetByte(2);
                int len = GetByte((ushort)(3 + offset));
                if (len == 0xfd)
                {
                    len = GetInt((ushort)(4 + offset));
                    return Global.Unicode.GetString(GetBytes((ushort)len, (ushort)(8 + offset))).Replace("\0", "");
                }
                else
                    return Global.Unicode.GetString(GetBytes((ushort)len, (ushort)(4 + offset))).Replace("\0", "");
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_BBS_POST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBBSPost(this);
        }

    }
}