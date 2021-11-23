using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:白之聖堂(30120000) NPC基本信息:技能商店(12001076) X:4 Y:6
namespace SagaScript.M30120000
{
    public class S12001076 : Event
    {
        public S12001076()
        {
            this.EventID = 12001076;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
