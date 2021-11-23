using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:路標(12002022) X:154 Y:253
namespace SagaScript.M10031000
{
    public class S12002022 : Event
    {
        public S12002022()
        {
            this.EventID = 12002022;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
