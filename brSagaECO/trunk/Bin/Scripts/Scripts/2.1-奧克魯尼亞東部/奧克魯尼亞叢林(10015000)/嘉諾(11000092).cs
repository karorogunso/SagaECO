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
                    Say(pc, 131, "從這裡一直往東邊走的話$R;" +
                        "就是帕斯特，那邊的料理很好吃的$R;");
                    break;
                case 2:
                    Say(pc, 131, "不久之前這條路$R;" +
                        "仍被稱爲世界上最安全的路$R;" +
                        "$P可是最近因爲魔物增多$R;" +
                        "已經不再安全，也沒有人這樣叫了$R;");
                    break;
            }
        }
    }
}
