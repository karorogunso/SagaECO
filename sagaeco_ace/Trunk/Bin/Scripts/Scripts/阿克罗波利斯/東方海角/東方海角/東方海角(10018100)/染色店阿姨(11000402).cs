using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000402 : Event
    {
        public S11000402()
        {
            this.EventID = 11000402;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (CountItem(pc, 50021100) >= 1)
            {
                Say(pc, 131, "啦啦啦~♪$R;" +
                    "我是染色店的阿姨$R;" +
                    "什么都可以染色的帅气阿姨$R;" +
                    "$R把你的『小丑帽』$R;" +
                    "用10000金币染色吗?$R;");
                switch (Select(pc, "用10000金币染色吗?", "", "染色", "不染色"))
                {
                    case 1:
                        if (pc.Gold > 9999)
                        {
                            TakeItem(pc, 50021100, 1);
                            pc.Gold -= 10000;
                            Say(pc, 131, "期待会出来什么颜色呢$R;");
                            PlaySound(pc, 2215, false, 100, 50);
                            Wait(pc, 2000);

                            selection = Global.Random.Next(1, 7);
                            switch (selection)
                            {
                                case 1:
                                    GiveItem(pc, 50021101, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 2:
                                    GiveItem(pc, 50021103, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 3:
                                    GiveItem(pc, 50021104, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 4:
                                    GiveItem(pc, 50021105, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 5:
                                    GiveItem(pc, 50021107, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 6:
                                    GiveItem(pc, 50021109, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                                case 7:
                                    GiveItem(pc, 50021110, 1);
                                    Say(pc, 131, "做起来简单，所以非常有意思$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "金币不够啊♪$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦~♪$R;" +
                "我是染色店的阿姨$R;" +
                "什么都可以染色的帅气阿姨$R;" +
                "$R有没有『小丑帽』?$R;");
        }
    }
}