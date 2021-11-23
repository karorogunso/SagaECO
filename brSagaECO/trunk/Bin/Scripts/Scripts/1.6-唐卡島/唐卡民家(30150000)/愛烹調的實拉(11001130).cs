using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30150000
{
    public class S11001130 : Event
    {
        public S11001130()
        {
            this.EventID = 11001130;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10008500) >= 20 &&
                CountItem(pc, 10026350) >= 10 &&
                CountItem(pc, 10008450) >= 30 &&
                CountItem(pc, 10045400) >= 30)
            {
                Say(pc, 131, "您來的正好，能不能把您的$R;" +
                    "$R『胡椒』20個$R;" +
                    "『黑色魚子醬』10個$R;" +
                    "『奶油』30個$R;" +
                    "『鹽』30個$R;" +
                    "$R給我呢？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "不給", "給他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10007400, 4))
                        {
                            TakeItem(pc, 10008500, 20);
                            TakeItem(pc, 10026350, 10);
                            TakeItem(pc, 10008450, 30);
                            TakeItem(pc, 10045400, 30);
                            GiveItem(pc, 10007400, 4);
                            GiveItem(pc, 10007450, 1);
                            Say(pc, 131, "拿您的東西，真不好意思喔$R;" +
                                "$R對了，要是不介意是剩下的，$R給您『乾魚』怎麼樣呢？$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『乾魚』$R;" +
                                "$P嗯？$R;" +
                                "$R這條魚很特別呀，$R;" +
                                "這個是不是？$R;");
                            return;
                        }
                        Say(pc, 131, "拿您的東西，真不好意思喔$R;" +
                            "$R對了，要是不介意是剩下的，$R給您『乾魚』怎麼樣呢？$R;" +
                            "$R把行李減少後，再來吧。$R;");
                        break;
                }
                return;
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "唉……$R;" +
                    "有一條奇怪的魚喜歡我，$R;" +
                    "$R天天給我送來乾魚，$R;" +
                    "現在已經厭煩了，也沒有地方放$R;" +
                    "說實話，真是不好意思呀。$R;");
            }
            else
            {
                Say(pc, 131, "出大事了$R;" +
                    "$R『胡椒』20個$R;" +
                    "『黑色魚子醬』10個$R;" +
                    "『奶油』30個$R;" +
                    "『鹽』30個$R;" +
                    "沒有了呀$R;" +
                    "$R哎呀怎麼辦阿？$R;");
            }
        }
    }
}