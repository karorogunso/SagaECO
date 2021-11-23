using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:卡特琳娜(11000138) X:41 Y:136
namespace SagaScript.M10014000
{
    public class S11000138 : Event
    {
        public S11000138()
        {
            this.EventID = 11000138;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000138, 131, "主人!!$R;" +
                                   "我要大便!$R;", "卡特琳娜");
        }
    }
}
