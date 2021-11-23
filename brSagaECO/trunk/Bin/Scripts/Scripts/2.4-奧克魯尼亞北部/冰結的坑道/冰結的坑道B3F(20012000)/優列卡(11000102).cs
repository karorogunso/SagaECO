using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20012000
{
    public class S11000102 : Event
    {
        public S11000102()
        {
            this.EventID = 11000102;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊啊啊…$R;" +
                "因爲妖怪在熙熙攘攘$R;" +
                "太可怕了$R;" +
                "$P但是不能只發抖阿!$R;" +
                "$R越是這個時候越要加把勁$R;" +
                "就算是生意也要做做看$R;");
            switch (Select(pc, "歡迎光臨……", "", "買東西", "賣東西", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 65);
                    break;
                case 2:
                    Say(pc, 131, "呀!計算器忘帶了…$R;" +
                        "沒有計算器的話我不能計算阿$R;" +
                        "它是致命的商人？$R;");
                    break;
                case 3:
                    Say(pc, 131, "啊啊…$R;" +
                        "我在這裡呆到什麽時候啊?$R;");
                    break;
            }
        }
    }
}
