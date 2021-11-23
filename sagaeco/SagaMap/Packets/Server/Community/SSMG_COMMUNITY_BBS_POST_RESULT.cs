using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_BBS_POST_RESULT : Packet
    {
        public enum Results
        {
            SUCCEED = 0,//";投稿しました";
            FAILED = -1,//";投稿に失敗しました";
            NOT_ENOUGH_MONEY = -2,//";お金が足りません";
        }

        public SSMG_COMMUNITY_BBS_POST_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1AFF;
        }

        public Results Result
        {
            set
            {
                this.PutInt((int)value, 2);
            }
        }
    }
}

