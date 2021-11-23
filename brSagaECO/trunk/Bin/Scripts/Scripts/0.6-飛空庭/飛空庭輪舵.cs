using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public class 飛空庭輪舵 : Event
    {
        public 飛空庭輪舵()
        {
            this.EventID = 12001110;
        }

        public override void OnEvent(ActorPC pc)
        {
            string input;

            ActorPC owner = GetFGardenOwner(pc);

            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc == owner)
            {
                if (pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL] != 0)
                {
                    switch (Select(pc, "做什麼呢？", "", "飛空庭起飛", "補充摩根炭", "改造飛空庭", "從飛空庭下來", "設定出入限制", "打出招牌", "什麼也不做"))
                    {
                        case 1:
                            if (!Knights_mask.Test(Knights.已經加入騎士團))
                            {
                                switch (Select(pc, "想去哪呢", "", "阿高普路斯市", "東域村落", "軍艦島", "南域", "北域", "唐卡市", "什麼也不做"))
                                {
                                    case 1:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向阿高普路斯市出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10023000, 182, 170);
                                        break;
                                    case 2:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向東域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10018100, 217, 94);
                                        break;
                                    case 3:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向軍艦島出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10035000, 100, 206);
                                        break;
                                    case 4:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向南域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10046000, 210, 198);
                                        break;
                                    case 5:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向北域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10001000, 103, 37);
                                        break;
                                    case 6:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向唐卡出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10062000, 193, 43);
                                        break;
                                    case 7:
                                        break;
                                }
                            }
                            else
                            {
                                string temp = "";
                                if (Knights_mask.Test(Knights.加入東軍騎士團))
                                {
                                    temp = "牛牛草原";
                                }
                                else if (Knights_mask.Test(Knights.加入西軍騎士團))
                                {
                                    temp = "摩根市";
                                }
                                else if (Knights_mask.Test(Knights.加入南軍騎士團))
                                {
                                    temp = "阿伊恩市";
                                }
                                else
                                {
                                    temp = "諾頓市";
                                }

                                switch (Select(pc, "想去哪呢", "", "阿高普路斯市", "東域村落", "軍艦島", "南域", "北域", "唐卡市", temp, "什麼也不做"))
                                {
                                    case 1:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向阿高普路斯市出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10023000, 182, 170);
                                        break;
                                    case 2:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向東域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10018100, 217, 94);
                                        break;
                                    case 3:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向軍艦島出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10032000, 100, 206);
                                        break;
                                    case 4:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向南域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10046000, 210, 198);
                                        break;
                                    case 5:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向北域出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10001000, 103, 37);
                                        break;
                                    case 6:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向唐卡出發!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10062000, 193, 43);
                                        break;
                                    case 7:
                                        switch (temp)
                                        {
                                            case "牛牛草原":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向牛牛草原出發!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10056000, 40, 70);
                                                break;
                                            case "摩根市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向摩根市出發!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10060000, 228, 185);
                                                break;
                                            case "阿伊恩市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向阿伊恩市出發!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10063100, 119, 50);
                                                break;
                                            case "諾頓市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向諾頓市出發!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10050000, 74, 143);
                                                break;
                                        }
                                        break;
                                    case 8:
                                        break;
                                }
                            }
                            break;
                        case 3:
                            ShowUI(pc, UIType.FGEquipt);
                            break;
                        case 4:
                            NPCMotion(pc, 12001111, 612);
                            Wait(pc, 500);

                            PlaySound(pc, 2231, false, 100, 50);
                            Wait(pc, 2000);

                            ExitFGarden(pc);
                            break;

                        case 5:
                            break;
                        case 6:
                            input = InputBox(pc, "請輸入招牌內容", InputType.PetRename);
                            if (input != "")
                                GetRopeActor(owner).Title = input;
                            break;
                        case 7:
                            break;
                    }
                }
                else
                {
                    switch (Select(pc, "想要做什麼呢?", "", "改造飛空庭", "從飛空庭下來", "設定出入限制", "打出招牌", "什麼也不做"))
                    {
                        case 1:
                            ShowUI(pc, UIType.FGEquipt);
                            break;
                        case 2:
                            NPCMotion(pc, 12001111, 612);
                            Wait(pc, 500);

                            PlaySound(pc, 2231, false, 100, 50);
                            Wait(pc, 2000);

                            ExitFGarden(pc);
                            break;

                        case 3:
                            break;
                        case 4:
                            input = InputBox(pc, "請輸入招牌內容", InputType.PetRename);
                            if (input != "")
                                GetRopeActor(owner).Title = input;
                            break;
                        case 5:
                            break;
                    }
                }
            }
            else
            {
                switch (Select(pc, "想要做什麼呢?", "", "從飛空庭下來", "什麼也不做"))
                {
                    case 1:
                        NPCMotion(pc, 12001111, 612);
                        Wait(pc, 500);

                        PlaySound(pc, 2231, false, 100, 50);
                        Wait(pc, 2000);

                        ExitFGarden(pc);
                        break;

                    case 2:
                        break;
                }
            }
            
        }
    }
}