using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_RECRUIT_CREATE : Packet
    {
        public CSMG_COMMUNITY_RECRUIT_CREATE()
        {
            this.offset = 2;
        }

        public Manager.RecruitmentType Type
        {
            get
            {
                return (SagaMap.Manager.RecruitmentType)this.GetByte(2);
            }
        }

        public string Title
        {
            get
            {
                string title = Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte(3), 4));
                return title.Replace("\0", "");
            }
        }

        public string Content
        {
            get
            {
                byte size = this.GetByte(3);
                string title = Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)(4 + size)), (ushort)(5 + size)));
                return title.Replace("\0", "");
            }
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_RECRUIT_CREATE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRecruitCreate(this);
        }

    }
}