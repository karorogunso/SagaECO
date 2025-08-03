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
                Say(pc, 131, "您来的正好，能不能把您的$R;" +
                    "$R『胡椒』20个$R;" +
                    "『黑色鱼子酱』10个$R;" +
                    "『黄油』30个$R;" +
                    "『盐』30个$R;" +
                    "$R给我呢？$R;");
                switch (Select(pc, "怎么办呢？", "", "不给", "给他吧"))
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
                            Say(pc, 131, "拿您的东西，真不好意思喔$R;" +
                                "$R对了，要是不介意是剩下的，$R给您『干鱼』怎么样呢？$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『干鱼』$R;" +
                                "$P嗯？$R;" +
                                "$R这条鱼很特别呀，$R;" +
                                "这个是不是？$R;");
                            return;
                        }
                        Say(pc, 131, "拿您的东西，真不好意思喔$R;" +
                            "$R对了，要是不介意是剩下的，$R给您『干鱼』怎么样呢？$R;" +
                            "$R把行李减少后，再来吧。$R;");
                        break;
                }
                return;
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "唉……$R;" +
                    "有一条奇怪的鱼喜欢我，$R;" +
                    "$R天天给我送来干鱼，$R;" +
                    "现在已经厌烦了，也没有地方放$R;" +
                    "说实话，真是不好意思呀。$R;");
            }
            else
            {
                Say(pc, 131, "出大事了$R;" +
                    "$R『胡椒』20个$R;" +
                    "『黑色鱼子酱』10个$R;" +
                    "『黄油』30个$R;" +
                    "『盐』30个$R;" +
                    "没有了呀$R;" +
                    "$R哎呀怎么办阿？$R;");
            }
        }
    }
}