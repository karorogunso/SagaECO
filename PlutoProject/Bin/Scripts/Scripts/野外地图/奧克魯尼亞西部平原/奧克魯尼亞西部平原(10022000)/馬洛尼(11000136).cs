using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:馬洛尼(11000136) X:40 Y:136
namespace SagaScript.M10022000
{
    public class S11000136 : Event
    {
        public S11000136()
        {
            this.EventID = 11000136;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000051, 131, "「马洛尼」不是平凡的魔物，$R;" +
                                   "是我最珍惜的朋友。$R;", "行会商人");
        }
    }
}
