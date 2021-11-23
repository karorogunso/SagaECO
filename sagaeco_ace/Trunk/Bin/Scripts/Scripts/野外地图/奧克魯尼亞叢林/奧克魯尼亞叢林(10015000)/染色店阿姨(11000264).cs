using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10015000
{
    public class S11000264 : Event
    {
        public S11000264()
        {
            this.EventID = 11000264;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (CountItem(pc, 50024100) >= 1)
            {
                Say(pc, 131, "啦啦啦~♪$R;" +
                    "我是染色店的阿姨$R;" +
                    "是什么都可以染色的帅气阿姨$R;" +
                    "$R你的『星纹贝雷帽』要用1500金币染色吗?$R;");
                switch (Select(pc, "用1500金币染色吗?", "", "是的!染色吧!", "我想选择颜色", "不染色"))
                {
                    case 1:
                        if (CheckInventory(pc, 50024100, 1))
                        {
                            if (pc.Gold > 1499)
                            {
                                selection = Global.Random.Next(1, 5);
                                pc.Gold -= 1500;
                                Say(pc, 131, "期待出来什么颜色呢$R;");
                                switch (selection)
                                {
                                    case 1:
                                        GiveItem(pc, 50024101, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 2:
                                        GiveItem(pc, 50024103, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 3:
                                        GiveItem(pc, 50024105, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 4:
                                        GiveItem(pc, 50024107, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 5:
                                        GiveItem(pc, 50024150, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "金币不够啊～♪$R;");
                            return;
                        }
                        Say(pc, 131, "您的行李太多了$R;");
                        break;
                    case 2:
                        if (CheckInventory(pc, 50024100, 1))
                        {
                            if (pc.Gold > 2999)
                            {
                                Say(pc, 131, "要选择颜色的价格是2倍!$R;" +
                                    "去拿3000金币再来吧~♪$R;");
                                switch (Select(pc, "付3000金币后染色吗?", "", "染成红色!", "染成蓝色!", "染成绿色!", "染成黄色!", "染成棕色!", "不染色"))
                                {
                                    case 1:
                                        GiveItem(pc, 50024101, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        pc.Gold -= 3000;
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 2:
                                        GiveItem(pc, 50024103, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 3:
                                        GiveItem(pc, 50024105, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 4:
                                        GiveItem(pc, 50024107, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        pc.Gold -= 3000;
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                    case 5:
                                        GiveItem(pc, 50024150, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "要选择颜色的价格是2倍!$R;" +
                                "去拿3000金币再来吧~♪$R;");
                            return;
                        }
                        Say(pc, 131, "您的行李太多了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦~♪$R;" +
                "我是染色店的阿姨$R;" +
                "是什么都可以染色的帅气阿姨$R;" +
                "$R有没有『星纹贝雷帽』?$R;");
        }
    }
}
