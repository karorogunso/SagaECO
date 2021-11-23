using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M21003000
{
    public class S11001506 : Event
    {
        public S11001506()
        {
            this.EventID = 11001506;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (CountItem(pc, 50062100) >= 1)
            {
                Say(pc, 131, "啦啦啦～♪$R;" +
                            "我是染色美人鱼。$R;" +
                            "什么都可以染、出色的美人鱼。$R;" +
                            "$R『泰达尼亚脚环』$R;", "染色美人鱼");
                switch (Select(pc, "用10000金币染色吗?", "", "染色", "不染色"))
                {
                    case 1:
                        if (pc.Gold > 9999)
                        {
                            TakeItem(pc, 50062100, 1);
                            pc.Gold -= 10000;
                            Say(pc, 131, "期待出来什么颜色$R;");
                            PlaySound(pc, 2215, false, 100, 50);
                            Wait(pc, 2000);

                            selection = Global.Random.Next(1, 7);
                            switch (selection)
                            {
                                case 1:
                                    GiveItem(pc, 50062102, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 2:
                                    GiveItem(pc, 50062103, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 3:
                                    GiveItem(pc, 50062105, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 4:
                                    GiveItem(pc, 50062107, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 5:
                                    GiveItem(pc, 50062109, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 6:
                                    GiveItem(pc, 50062110, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 7:
                                    GiveItem(pc, 50062113, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                            }
                            return;

                        }
                        Say(pc, 131, "金币不够啊♪$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦～♪$R;" +
            "我是染色美人鱼。$R;" +
            "什么都可以染、出色的美人鱼。$R;" +
            "$R『泰达尼亚脚环』没有拿吗？$R;", "染色美人鱼");
        }
    }
}
