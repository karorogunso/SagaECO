using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000404 : Event
    {
        public S11000404()
        {
            this.EventID = 11000404;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (CountItem(pc, 50021500) >= 1)
            {

                Say(pc, 131, "啦啦啦~♪$R;" +
                    "我是染色店的阿姨$R;" +
                    "什麽都可以染色的帥氣阿姨$R;" +
                    "$R要把『魔法師帽』$R;" +
                    "用1000金幣染色嗎?$R;");
                switch (Select(pc, "用1000金幣染色嗎?", "", "染色", "不染色"))
                {
                    case 1:
                        if (pc.Gold > 999)
                        {
                            TakeItem(pc, 50021500, 1);
                            pc.Gold -= 1000;
                            Say(pc, 131, "期待出來什麽顔色阿$R;");
                            PlaySound(pc, 2215, false, 100, 50);
                            Wait(pc, 2000);
                            selection = Global.Random.Next(1, 7);
                            switch (selection)
                            {
                                case 1:
                                    GiveItem(pc, 50021501, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 2:
                                    GiveItem(pc, 50021503, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 3:
                                    GiveItem(pc, 50021504, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 4:
                                    GiveItem(pc, 50021505, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 5:
                                    GiveItem(pc, 50021507, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 6:
                                    GiveItem(pc, 50021509, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                                case 7:
                                    GiveItem(pc, 50021510, 1);
                                    Say(pc, 131, "做起來簡單，所以非常有意思$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "金幣不夠啊♪$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦~♪$R;" +
                "我是染色店的阿姨$R;" +
                "什麽都可以染色的帥氣阿姨$R;" +
                "$R有『魔法師帽』嗎?$R;");
        }
    }
}
