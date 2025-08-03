using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:西門守衛(11000129) X:134 Y:61
namespace SagaScript.M10022000
{
    public class S11000129 : Event
    {
        public S11000129()
        {
            this.EventID = 11000129;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000129, 131, "照着这条路往西边走的话，$R;" +
                                   "会走道非常险峻的山区，$R;" +
                                   "那里魔物也非常凶猛，$R;" +
                                   "千万不可逞强啊!$R;", "西门守卫");
        }
    }
}
