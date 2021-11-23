using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:塔妮亞利益代表的房間(30052000) NPC基本信息:塔妮亞利益代表(11000027) X:3 Y:4
namespace SagaScript.M30052000
{
    public class S11000027 : Event
    {
        public S11000027()
        {
            this.EventID = 11000027;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
