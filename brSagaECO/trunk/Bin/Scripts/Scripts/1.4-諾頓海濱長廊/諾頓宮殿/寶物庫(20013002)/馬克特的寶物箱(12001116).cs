using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20013002
{
    public class S12001116 : Event
    {
        public S12001116()
        {
            this.EventID = 12001116;
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
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 10000604, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「復活藥水」$R;");
                        return;
                    }
                    if (pc.Gender == PC_GENDER.FEMALE)
                    {
                        if (pc.JobBasic == PC_JOB.FENCER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064750, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「神聖靴子（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SCOUT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「夜忍靴子（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「白鳥靴子（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WIZARD)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063750, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「皇家魔道士靴子（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「妖艶的涼鞋（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WARLOCK)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064050, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「護士皮鞋（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064150, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「女匠師涼鞋（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「藥商高跟鞋（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.RANGER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50065250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「牛仔冒險靴子（女）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.MERCHANT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50065350, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「中華淑女高跟鞋（女）」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "鑰匙不對$R;");
                        return;

                    }
                    if (pc.JobBasic == PC_JOB.FENCER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50063450, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「神聖靴子（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.ARCHER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50063650, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「黑鳥靴子（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.WIZARD)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064851, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「皇家魔道士靴子（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.SHAMAN)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064950, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「翡翠華染涼鞋（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.TATARABE)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50065050, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「匠師的涼鞋（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FARMASIST)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50065150, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「天才博士的高筒鞋（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.RANGER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064350, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「深山探險靴子（男）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.MERCHANT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50060051, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「商店皮鞋（男）」$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "鑰匙不對$R;");
                    return;
            }
        }
    }
}