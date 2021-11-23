using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:光之塔周邊(10070000) NPC基本信息:挙動不審な男(11001992) X:151 Y:49
namespace SagaScript.M10070000
{
    public class S11001992 : Event
    {
        public S11001992()
        {
            this.EventID = 11001992;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}