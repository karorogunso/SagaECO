using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:少數民族利益代表的房間(30054000) NPC基本信息:彼特烏(11000030) X:1 Y:4
namespace SagaScript.M30054000
{
    public class S11000030 : Event
    {
        public S11000030()
        {
            this.EventID = 11000030;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
