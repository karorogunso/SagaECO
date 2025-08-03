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
                Say(pc, 255, "宝物箱上锁了$R;");
                return;
            }
            switch (Select(pc, "开提佩勒特的宝物箱吗？", "", "什么都不做", "用女王的钥匙"))
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
                        Say(pc, 255, "得到「复活药水」$R;");
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
                            Say(pc, 255, "得到「神圣长靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SCOUT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「寂静之靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「白天鹅长靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WIZARD)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063750, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「宫廷魔导师靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50063850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「娇艳的草屐♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WARLOCK)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064050, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「护士皮鞋♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064150, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「女匠师凉鞋♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50064250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「医务室靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.RANGER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50065250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「亚马逊靴♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.MERCHANT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001116, 112);
                            GiveItem(pc, 50065350, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「深闺闺秀靴♀」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "钥匙不对$R;");
                        return;

                    }
                    if (pc.JobBasic == PC_JOB.FENCER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50063450, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「神圣长靴♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.ARCHER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50063650, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「黑天鹅靴♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.WIZARD)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064851, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「宫廷魔导师靴♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.SHAMAN)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064950, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「草木染的草屐♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.TATARABE)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50065050, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「匠师凉鞋♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FARMASIST)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50065150, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「天才博士平底鞋♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.RANGER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50064350, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「登山靴♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.MERCHANT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001116, 112);
                        GiveItem(pc, 50060051, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「集市皮鞋」$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "钥匙不对$R;");
                    return;
            }
        }
    }
}