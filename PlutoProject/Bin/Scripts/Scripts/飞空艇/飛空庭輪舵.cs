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

            bool freeride = false;

            ActorPC owner = GetFGardenOwner(pc);

            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc == owner)
            {
                if (pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL] != 0)
                {
                    switch (Select(pc, "做什么呢？", "", "飞空庭起飞", "补充摩戈炭", "改造飞空庭", "从飞空庭下来", "设定出入限制", "打出招牌", "什么也不做"))
                    {
                        case 1:

                            //Say(pc, 131, "引擎是：" + pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL]);

                            //return;

                            if (pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL] == 30060000)
                            {
                                freeride = true;
                            }
                            

                            if(pc.FGarden.Fuel < 10 && !freeride)
                            {
                                PlaySound(pc, 2041, false, 100, 50);
                                Say(pc, 0, 131, "摩根炭不足…$R;", " ");
                                return;
                            }

                            if (!Knights_mask.Test(Knights.已經加入騎士團))
                            {

                                
                                switch (Select(pc, "想去哪呢", "", "阿克罗波利斯", "东方海角", "军舰岛", "南方海角", "北方海角", "唐卡市", "什么也不做"))
                                {
                                    case 1:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向阿克罗波利斯出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10023000, 182, 170);
                                        if (!freeride) pc.FGarden.Fuel -= 10;

                                        break;
                                    case 2:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向东方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10018100, 217, 94);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 3:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向军舰岛出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10035000, 100, 206);
                                        if(!freeride)pc.FGarden.Fuel -= 10;
                                        break;
                                    case 4:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向南方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10046000, 210, 198);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 5:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向北方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10001000, 103, 37);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 6:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向唐卡出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10062000, 193, 43);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                        /*
                                    case 7:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "...找个光之塔的偏僻处降落吧$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10070000, 76, 225);
                                        break;
                                    case 8:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "...找个通天塔的角落降落吧$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10058000, 154, 167);
                                        break;
                                        */
                                    case 7:
                                        break;
                                }
                            }
                            else
                            {
                                string temp = "";
                                if (Knights_mask.Test(Knights.加入東軍騎士團))
                                {
                                    temp = "哞哞草原";
                                }
                                else if (Knights_mask.Test(Knights.加入西軍騎士團))
                                {
                                    temp = "摩戈市";
                                }
                                else if (Knights_mask.Test(Knights.加入南軍騎士團))
                                {
                                    temp = "艾恩萨乌斯市";
                                }
                                else
                                {
                                    temp = "诺森市";
                                }

                                switch (Select(pc, "想去哪呢", "", "阿克罗波利斯", "东方海角", "军舰岛", "南方海角", "北方海角", "唐卡市", temp, "什么也不做"))
                                {
                                    case 1:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向阿克罗波利斯出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10023000, 182, 170);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 2:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向东方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10018100, 217, 94);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 3:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向军舰岛出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10035000, 100, 206);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 4:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向南方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10046000, 210, 198);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 5:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向北方海角出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10001000, 103, 37);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 6:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "向唐卡出发!!$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10062000, 193, 43);
                                        if (!freeride) pc.FGarden.Fuel -= 10;
                                        break;
                                    case 7:
                                        switch (temp)
                                        {
                                            case "哞哞草原":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向哞哞草原出发!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10056000, 40, 70);
                                                if (!freeride) pc.FGarden.Fuel -= 10;
                                                break;
                                            case "摩戈市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向摩戈市出发!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10060000, 228, 185);
                                                if (!freeride) pc.FGarden.Fuel -= 10;
                                                break;
                                            case "艾恩萨乌斯市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向艾恩萨乌斯市出发!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10063100, 119, 50);
                                                if (!freeride) pc.FGarden.Fuel -= 10;
                                                break;
                                            case "诺森市":
                                                PlaySound(pc, 2426, false, 100, 50);
                                                Say(pc, 0, 131, "向诺森市出发!!$R;", " ");
                                                PlaySound(pc, 2438, false, 100, 50);
                                                FGTakeOff(pc, 10050000, 74, 143);
                                                if (!freeride) pc.FGarden.Fuel -= 10;
                                                break;
                                        }
                                        break;
                                        /*
                                    case 8:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "...找个光之塔的偏僻处降落吧$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10070000, 76, 225);
                                        break;
                                    case 9:
                                        PlaySound(pc, 2426, false, 100, 50);
                                        Say(pc, 0, 131, "...找个通天塔的角落降落吧$R;", " ");
                                        PlaySound(pc, 2438, false, 100, 50);
                                        FGTakeOff(pc, 10058000, 154, 167);
                                        break;
                                        */
                                    case 8:
                                        break;
                                }
                            }
                            break;
                        case 2:
                            switch (Select(pc, "現有摩根炭: " + pc.FGarden.Fuel + " 個", "", "補充摩根炭", "什么也不做"))
                            {
                                case 1:
                                    ushort count = ushort.Parse(InputBox(pc, "要補充多少？", InputType.Bank));
                                    if(CountItem(pc, 10016700) < count)
                                    {
                                        PlaySound(pc, 2041, false, 100, 50);
                                        Say(pc, 0, 131, "身上摩根炭不足…$R;", " ");
                                        return;
                                    }else
                                    {
                                        pc.FGarden.Fuel += (uint)count;
                                        TakeItem(pc, 10016700, count);
                                        Say(pc, 0, 131,"已補充 " + count + " 個摩根炭");
                                        
                                    }

                                    break;
                                case 2:
                                    break;
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
                            input = InputBox(pc, "请输入招牌内容", InputType.PetRename);
                            if (input != "")
                                GetRopeActor(owner).Title = input;
                            break;
                        case 7:
                            break;
                    }
                }
                else
                {
                    switch (Select(pc, "想要做什么呢?", "", "改造飞空庭", "从飞空庭下来", "设定出入限制", "打出招牌", "什么也不做"))
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
                            input = InputBox(pc, "请输入招牌内容", InputType.PetRename);
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
                switch (Select(pc, "想要做什么呢?", "", "从飞空庭下来", "什么也不做"))
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