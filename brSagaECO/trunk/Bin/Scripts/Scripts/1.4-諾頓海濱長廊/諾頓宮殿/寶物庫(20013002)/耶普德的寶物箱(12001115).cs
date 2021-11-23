using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20013002
{
    public class S12001115 : Event
    {
        public S12001115()
        {
            this.EventID = 12001115;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);

            if (CountItem(pc, 10022600) < 1)
            {
                Say(pc, 255, "寶物箱上鎖了$R;");
                return;
            }
            switch (Select(pc, "開提佩勒特的寶物箱嗎？", "", "什麼都不做", "用女王的鑰匙"))
            {
                case 1:
                    break;
                case 2:
                    if (!CheckInventory(pc, 10000604, 1))
                    {
                        Say(pc, 255, "行李太多了！$R;");
                        return;
                    }
                    if (!mask.Test(NDFlags.第一次职业装))
                    {
                        mask.SetValue(NDFlags.第一次职业装, true);
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 10000604, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「復活藥水」$R;");
                        return;
                    }
                    if (pc.Gender == PC_GENDER.FEMALE)
                    {
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50014752, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「寶藍色闊身長筒褲（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50014850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「女性匠師的長褲（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50014950, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「藥商的長褲（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50072252, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「粉紅色獸角箭筒」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "鑰匙不對$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.ARCHER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50072251, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「黑色獸角箭筒」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.TATARABE)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015350, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「匠師的長褲（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FARMASIST)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015450, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「天才博士長褲（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.RANGER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015050, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「深山探險長褲（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.MERCHANT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015150, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「商店長褲（男）」$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "鑰匙不對$R;");
                    break;
            }
        }
    }
}