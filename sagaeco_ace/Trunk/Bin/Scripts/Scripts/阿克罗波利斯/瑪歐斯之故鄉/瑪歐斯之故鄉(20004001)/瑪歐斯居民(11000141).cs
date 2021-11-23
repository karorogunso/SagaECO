using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000141 : Event
    {
        public S11000141()
        {
            this.EventID = 11000141;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
                Say(pc, 11000141, 131, "依嘶$R;" +
                    "$P……$R;" +
                    "$P说一声玛乌鼠$R;" +
                    "是鱼人打招呼的方式哦$R;");
            Say(pc, 11000141, 131, "哦，$R;" +
                "人类！$R;");
            if (pc.Race == PC_RACE.TITANIA)
            {
                Say(pc, 11000141, 131, "什么？不是人类？$R;" +
                    "泰达尼亚种族？$R;" +
                    "哦！$R;" +
                    "是泰达尼亚啊！！$R;");
                return;
            }
            if (pc.Race == PC_RACE.DOMINION)
            {
                Say(pc, 11000141, 131, "什么？不是人类？$R;" +
                    "多米尼翁种族？$R;" +
                    "$P哦！$R;" +
                    "是多米尼翁啊!!$R;");
                return;
            }
        }
    }
}