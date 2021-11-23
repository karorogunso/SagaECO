
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S10002290 : Event
    {
        public S10002290()
        {
            this.EventID = 10002290;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<测试剧情> 记录 = pc.CMask["测试剧情"];
            Say(pc, 0, "哈...哈....");
            Wait(pc, 2000);
            Say(pc, 0, "天好热...$R好饿...好渴...");
            Wait(pc, 2000);
            pc.TInt["NextMapEvent"] = 50005001;
            Warp(pc, 10100018, 86, 117);
        }
    }
}

