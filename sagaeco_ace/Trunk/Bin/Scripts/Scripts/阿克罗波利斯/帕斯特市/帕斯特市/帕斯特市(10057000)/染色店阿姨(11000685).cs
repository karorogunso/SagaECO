using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000685 : Event
    {
        public S11000685()
        {
            this.EventID = 11000685;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 50012300) >= 1)
            {
                /*
                //50012300, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    Say(pc, 131, "啦啦啦$R;" +
                        "我是染色店的阿姨！$R;" +
                        "什麽都可以染色的帥阿姨！$R;" +
                        "$R有『迷你裙』嗎?$R;" +
                        "$P…??$R;" +
                        "$P有點奇怪的『迷你裙』啊!$R;" +
                        "$R這個没辦法染色~$R;");
                    return;
                }
                */
                Say(pc, 131, "啦啦啦$R;" +
                    "我是染色店的阿姨！$R;" +
                    "什么都可以染色的帅阿姨！$R;" +
                    "$R『迷你裙』花500金币，染色如何？$R;");
                switch (Select(pc, "花500金币染色？", "", "染色！", "想挑颜色！", "不想染色！"))
                {
                    case 1:
                        /*
                        if (a//ME.ITEMSLOT_EMPTY < 1
                        )
                        {
                            Call(EVT11000404011);
                            return;
                        }
                        */
                        if (pc.Gold < 500)
                        {
                            Say(pc, 131, "金币不足~$R;");
                            return;
                        }
                        pc.Gold -= 500;
                        Say(pc, 131, "期待会变成什么颜色！$R;");
                        int a = Global.Random.Next(1, 11);
                        switch (a)
                        {
                            case 1:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012350, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 2:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012351, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 3:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012352, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 4:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012353, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 5:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012354, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 6:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012355, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 7:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012356, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 8:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012357, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 9:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012358, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 10:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012359, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                            case 11:
                                TakeItem(pc, 50012300, 1);
                                GiveItem(pc, 50012361, 1);
                                PlaySound(pc, 2215, false, 100, 50);
                                //FADE OUT BLACK
                                Wait(pc, 2000);
                                //FADE IN
                                Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                break;
                        }
                        break;
                    case 2:
                        /*
                        if (a//ME.ITEMSLOT_EMPTY < 1
                        )
                        {
                            Call(EVT11000404011);
                            return;
                        }
                        */
                        if (pc.Gold > 999)
                        {
                            Say(pc, 131, "挑颜色的话$R;" +
                                "拿1000金币过来吧？$R;");
                            switch (Select(pc, "花1000金币染色吗？", "", "加金黄色条纹！", "染浅棕色！", "染灰色方格纹！", "染绿色方格纹！", "染蓝色方格纹！", "染浅绿色！", "染深绿色！", "染白色！", "染薄荷绿色！", "加白色条纹！", "染深红色！", "不染色"))
                            {
                                case 1:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012350, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 2:
                                    //EVT1100068516
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012351, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 3:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012352, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 4:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012353, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 5:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012354, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 6:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012355, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 7:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012356, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 8:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012357, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 9:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012358, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 10:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012359, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                                case 11:
                                    TakeItem(pc, 50012300, 1);
                                    GiveItem(pc, 50012361, 1);
                                    pc.Gold -= 1000;
                                    PlaySound(pc, 2215, false, 100, 50);
                                    //FADE OUT BLACK
                                    Wait(pc, 2000);
                                    //FADE IN
                                    Say(pc, 131, "简单又方便的染色！$R真的是很好玩$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "挑颜色的话$R;" +
                            "拿1000金币过来吧$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "啦啦啦$R;" +
                "我是染色店的阿姨！$R;" +
                "什么都可以染色的帅阿姨！$R;" +
                "$R有『迷你裙』吗?$R;");
            //EVENTEND
            //EVT11000685bad
            //EVENTEND
            //EVT1100068500
            //EVENTEND
            //EVT1100068501
            //EVENTEND
            //EVT1100068502
            //EVENTEND
            //EVT1100068503
            //EVENTEND
            //EVT1100068504
            //EVENTEND
            //EVT1100068505
            //EVENTEND
            //EVT1100068506
            //EVENTEND
            //EVT1100068507
            //EVENTEND
            //EVT1100068508
            //EVENTEND
            //EVT1100068509
            //EVENTEND
            //EVT1100068510
            //EVENTEND
            //EVT1100068511
            //EVENTEND
            //EVT1100068512
            //EVENTEND
            //EVT1100068513
            //EVENTEND
            //EVT1100068515
            //EVENTEND
            //EVENTEND
            //EVT1100068517
            //EVENTEND
            //EVT1100068518
            //EVENTEND
            //EVT1100068519
            //EVENTEND
            //EVT1100068520
            //EVENTEND
            //EVT1100068521
            //EVENTEND
            //EVT1100068522
            //EVENTEND
            //EVT1100068523
            //EVENTEND
            //EVT1100068524
            //EVENTEND
            //EVT1100068525
            //EVENTEND
            //EVT1100068526
            //EVENTEND

        }
    }
}