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
            switch (Select(pc, "準備怎麼玩？", "", "『』ecoin向", "『』Gold向", "『』不賭"))
            {
                case 1:
                    if (pc.ECoin > 1)
                    {
                        Say(pc, 131, "$CR " + pc.Name + " $CD您現有$R;" +
                      "$CR" + pc.ECoin + " $CD枚ecoin$R;", "");
                        switch (Select(pc, "玩什麽呢？", "", "抽印章", "未使用", "不賭"))
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

                                        Say(pc, 131, "歡迎再來，你現有$R;" +
                                         "$CR" + pc.ECoin + "$CD枚ecoin$R;", "莊家");
                                        return;
                                    }

                                }
                                else
                                {
                                    Say(pc, 131, "ecoin不夠，你現有$R;" +
                                      "$CR" + pc.ECoin + "$CD枚ecoin$R;", "莊家");
                                }
                                return;
                        }
                    }
                    Say(pc, 0, 131, "玩迷你遊戲需要$R;" +
                    "準備ecoin幣$CR1$CD枚以上。$R;" +
                    "$R首先去找$CRecoin櫃檯$CD$R;" +
                    "購買足夠的ecoin幣吧。$R;", "莊家");

                    return;
                case 2:
                    switch (Select(pc, "玩什麽呢？", "", "抽籤", "骰子", "不賭"))
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
                                Say(pc, 131, "金幣不夠啊♪$R;");
                            }
                            break;
                        case 2:

                            switch (Select(pc, "選擇", "", "1點10萬", "2點50萬", "3點100萬", "4點500萬", "5點1000萬", "6點5000萬", "不賭"))
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
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                            case 3:
                                                ShowEffect(pc, 4525);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                            case 4:
                                                ShowEffect(pc, 4526);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                            case 5:
                                                ShowEffect(pc, 4527);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                            case 6:
                                                ShowEffect(pc, 4528);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "金幣不夠啊♪$R;");
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
                                                    ShowEffect(pc, 4524);//2點
                                                    Wait(pc, 660);
                                                    GiveRandomTreasure(pc, "RPG");
                                                    Say(pc, 0, 131, "恭喜你……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4523);//1點
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                            case 2:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4525);//3點
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "遺憾……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4526);//4點
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");

                                                break;
                                            case 3:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4527);//5點
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "遺憾……！$R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4528);//6點
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "遺憾……！$R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "金幣不夠啊♪$R;");
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