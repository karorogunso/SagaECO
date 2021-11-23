using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:南門守衛(11000131)X:121 Y:61
namespace SagaScript.M10031000
{
    public class S11000131 : Event
    {
        public S11000131()
        {
            this.EventID = 11000131;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
