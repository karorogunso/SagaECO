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
                    "是什麽都可以染色的帥氣阿姨$R;" +
                    "$R你的『星紋貝雷帽』要用1500金幣染色嗎?$R;");
                switch (Select(pc, "用1500金幣染色嗎?", "", "是的!染色吧!", "我想選擇顔色", "不染色"))
                {
                    case 1:
                        if (CheckInventory(pc, 50024100, 1))
                        {
                            if (pc.Gold > 1499)
                            {
                                selection = Global.Random.Next(1, 5);
                                pc.Gold -= 1500;
                                Say(pc, 131, "期待出來什麽顔色呢$R;");
                                switch (selection)
                                {
                                    case 1:
                                        GiveItem(pc, 50024101, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 2:
                                        GiveItem(pc, 50024103, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 3:
                                        GiveItem(pc, 50024105, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 4:
                                        GiveItem(pc, 50024107, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 5:
                                        GiveItem(pc, 50024150, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "金幣不夠啊～♪$R;");
                            return;
                        }
                        Say(pc, 131, "您的行李太多了$R;");
                        break;
                    case 2:
                        if (CheckInventory(pc, 50024100, 1))
                        {
                            if (pc.Gold > 2999)
                            {
                                Say(pc, 131, "要選擇顔色的價格是2倍!$R;" +
                                    "去拿3000金幣再來吧~♪$R;");
                                switch (Select(pc, "付3000金幣後染色嗎?", "", "染成紅色!", "染成藍色!", "染成綠色!", "染成黃色!", "染成棕色!", "不染色"))
                                {
                                    case 1:
                                        GiveItem(pc, 50024101, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        pc.Gold -= 3000;
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 2:
                                        GiveItem(pc, 50024103, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 3:
                                        GiveItem(pc, 50024105, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 4:
                                        GiveItem(pc, 50024107, 1);
                                        TakeItem(pc, 50024100, 1);
                                        PlaySound(pc, 2215, false, 100, 50);
                                        pc.Gold -= 3000;
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                    case 5:
                                        GiveItem(pc, 50024150, 1);
                                        TakeItem(pc, 50024100, 1);
                                        pc.Gold -= 3000;
                                        PlaySound(pc, 2215, false, 100, 50);
                                        Wait(pc, 2000);
                                        Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "要選擇顔色的價格是2倍!$R;" +
                                "去拿3000金幣再來吧~♪$R;");
                            return;
                        }
                        Say(pc, 131, "您的行李太多了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦~♪$R;" +
                "我是染色店的阿姨$R;" +
                "是什麽都可以染色的帥氣阿姨$R;" +
                "$R有沒有『星紋貝雷帽』?$R;");
        }
    }
}
