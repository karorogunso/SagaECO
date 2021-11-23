using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 賭博 : Event
    {
        public 賭博()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            switch (Select(pc, "准备怎么玩？", "", "『』ecoin向", "『』Gold向", "『』不赌"))
            {
                case 1:
                    if (pc.ECoin > 1)
                    {
                        Say(pc, 131, "$CR " + pc.Name + " $CD您现有$R;" +
                      "$CR" + pc.ECoin + " $CD枚ecoin$R;", "");
                        switch (Select(pc, "玩什么呢？", "", "抽印章", "未使用", "不赌"))
                        {
                            case 1:
                                if (pc.ECoin > 5000)
                                {
                                    if (Select(pc, "抽么", "", "抽", "不抽") == 1)
                                    {
                                        pc.ECoin -= 5000;
                                        if (SagaLib.Global.Random.Next(0, 99) < 20)
                                        {
                                            GiveRandomTreasure(pc, "100ky");
                                            return;
                                        }

                                        Say(pc, 131, "欢迎再来，您现有$R;" +
                                         "$CR" + pc.ECoin + "$CD枚ecoin$R;", "庄家");
                                        return;
                                    }

                                }
                                else
                                {
                                    Say(pc, 131, "ecoin不够，您现有$R;" +
                                      "$CR" + pc.ECoin + "$CD枚ecoin$R;", "庄家");
                                }
                                return;
                        }
                    }
                    Say(pc, 0, 131, "玩迷你游戏需要$R;" +
                    "准备ecoin币$CR1$CD枚以上。$R;" +
                    "$R首先去找$CRecoin柜台$CD$R;" +
                    "购买足够的ecoin币吧。$R;", "庄家");

                    return;
                case 2:
                    switch (Select(pc, "玩什么呢？", "", "抽签", "骰子", "不赌"))
                    {
                        case 1:
                            if (pc.Gold > 99999)
                            {
                                pc.Gold -= 100000;
                                if (SagaLib.Global.Random.Next(0, 99) < 20)
                                {
                                    GiveRandomTreasure(pc, "kuji14");
                                    return;
                                }
                                Say(pc, 131, "RP不行呀♪$R;");
                            }
                            else
                            {
                                Say(pc, 131, "金币不够啊♪$R;");
                            }
                            break;
                        case 2:

                            switch (Select(pc, "选择", "", "1点10万", "2点50万", "3点100万", "4点500万", "5点1000万", "6点5000万", "不賭"))
                            {
                                case 1:
                                    if (pc.Gold > 99999)
                                    {
                                        pc.Gold -= 100000;
                                        selection = Global.Random.Next(1, 6);
                                        switch (selection)
                                        {
                                            case 1:
                                                ShowEffect(pc, 4523);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "恭喜你……！$R;", "");
                                                GiveItem(pc, 10009551, 1);
                                                break;
                                            case 2:
                                                ShowEffect(pc, 4524);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                            case 3:
                                                ShowEffect(pc, 4525);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                            case 4:
                                                ShowEffect(pc, 4526);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                            case 5:
                                                ShowEffect(pc, 4527);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                            case 6:
                                                ShowEffect(pc, 4528);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "金币不够啊♪$R;");
                                    }
                                    break;
                                case 2:
                                    if (pc.Gold > 499999)
                                    {
                                        pc.Gold -= 500000;
                                        selection = Global.Random.Next(1, 3);
                                        switch (selection)
                                        {
                                            case 1:
                                                if (SagaLib.Global.Random.Next(0, 99) < 20)
                                                {
                                                    ShowEffect(pc, 4524);//2点
                                                    Wait(pc, 660);
                                                    GiveRandomTreasure(pc, "RPG");
                                                    Say(pc, 0, 131, "恭喜你……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4523);//1点
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                            case 2:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4525);//3点
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "遗憾……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4526);//4点
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");

                                                break;
                                            case 3:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4527);//5点
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "遗憾……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4528);//6点
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遗憾……！$R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "金币不够啊♪$R;");
                                    }
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                            }


                            break;
                    }
                    return;
            }
        }
    }
}