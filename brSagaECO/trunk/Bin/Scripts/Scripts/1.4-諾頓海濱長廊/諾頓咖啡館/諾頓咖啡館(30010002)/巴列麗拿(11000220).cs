using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010002
{
    public class S11000220 : Event
    {
        public S11000220()
        {
            this.EventID = 11000220;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (CountItem(pc, 10005800) >= 1)
            {
                Say(pc, 131, "呀！$R;" +
                    "$R那個是不是『豌豆莢』？$R;" +
                    "只要有它就能做濃湯了$R;");
                switch (Select(pc, "歡迎光臨！", "", "買東西", "什麼也不做"))
                {
                    case 1:
                        TakeItem(pc, 10005800, 1);
                        OpenShopBuy(pc, 106);
                        break;
                    case 2:
                        break;
                }
                Say(pc, 131, "歡迎再來$R;");
                return;
            }
            if (mask.Test(NDFlags.中央的巴列麗拿))
            {
                Say(pc, 131, "哦？$R;" +
                    "我們在阿高普路斯見過吧？$R;" +
                    "$R還有麻煩過您呢？$R;" +
                    "微火中長時間煮沸$R;" +
                    "就能完成特製的巴列麗拿雜菜濃湯$R;" +
                    "$R來！請嘗一嘗吧！$R;" +
                    "$P真想這麼說$R;" +
                    "不過沒有『豌豆莢』，做不了啊$R;" +
                    "$R抱歉唷$R;");
                return;
            }
            Say(pc, 131, "給我『豌豆莢』$R;" +
                "就能做到美味的濃湯唷$R;");
        }
    }
}
