using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:南門守衛(11000132) X:134 Y:61
namespace SagaScript.M10031000
{
    public class S11000132 : Event
    {
        public S11000132()
        {
            this.EventID = 11000132;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
