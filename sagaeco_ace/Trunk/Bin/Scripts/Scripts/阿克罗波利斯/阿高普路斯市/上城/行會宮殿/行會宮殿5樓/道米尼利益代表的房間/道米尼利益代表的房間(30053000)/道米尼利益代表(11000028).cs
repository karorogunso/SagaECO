using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:道米尼利益代表的房間(30053000) NPC基本信息:道米尼利益代表(11000028) X:3 Y:4
namespace SagaScript.M30053000
{
    public class S11000028 : Event
    {
        public S11000028()
        {
            this.EventID = 11000028;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
