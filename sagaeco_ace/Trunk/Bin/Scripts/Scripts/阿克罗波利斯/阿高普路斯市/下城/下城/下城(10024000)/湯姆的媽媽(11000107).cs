using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:湯姆的媽媽(11000107) X:61 Y:168
namespace SagaScript.M10024000
{
    public class S11000107 : Event
    {
        public S11000107()
        {
            this.EventID = 11000107;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
