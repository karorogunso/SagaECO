using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10015000
{
    public class S11000092 : Event
    {
        public S11000092()
        {
            this.EventID = 11000092;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "从这里一直往东边走的话$R;" +
                        "就是法伊斯特，那边的料理很好吃的$R;");
                    break;
                case 2:
                    Say(pc, 131, "不久之前这条路$R;" +
                        "仍被称为世界上最安全的路$R;" +
                        "$P可是最近因为魔物增多$R;" +
                        "已经不再安全，也没有人这样叫了$R;");
                    break;
            }
        }
    }
}
