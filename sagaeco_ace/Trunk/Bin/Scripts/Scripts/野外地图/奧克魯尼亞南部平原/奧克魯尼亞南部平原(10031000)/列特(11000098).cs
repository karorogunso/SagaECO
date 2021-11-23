using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:列特(11000098) X:177 Y:216
namespace SagaScript.M10031000
{
    public class S11000098 : Event
    {
        public S11000098()
        {
            this.EventID = 11000098;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
