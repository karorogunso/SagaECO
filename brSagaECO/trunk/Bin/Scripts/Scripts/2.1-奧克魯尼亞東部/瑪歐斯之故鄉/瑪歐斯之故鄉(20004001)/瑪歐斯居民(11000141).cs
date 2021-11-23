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
                    "$P說一聲瑪烏鼠$R;" +
                    "是瑪歐斯打招呼的方式唷$R;");
            Say(pc, 11000141, 131, "哦，$R;" +
                "人類！$R;");
            if (pc.Race == PC_RACE.TITANIA)
            {
                Say(pc, 11000141, 131, "什麼？不是人類？$R;" +
                    "塔妮亞種族？$R;" +
                    "哦！$R;" +
                    "是塔妮亞啊！！$R;");
                return;
            }
            if (pc.Race == PC_RACE.DOMINION)
            {
                Say(pc, 11000141, 131, "什麼？不是人類？$R;" +
                    "道米尼種族？$R;" +
                    "$P哦！$R;" +
                    "是道米尼阿!!$R;");
                return;
            }
        }
    }
}