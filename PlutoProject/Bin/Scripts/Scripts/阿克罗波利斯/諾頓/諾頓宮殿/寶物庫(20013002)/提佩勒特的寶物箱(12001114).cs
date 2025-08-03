using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20013002
{
    public class S12001114 : Event
    {
        public S12001114()
        {
            this.EventID = 12001114;
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
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 10000604, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「复活药水」$R;");
                        return;
                    }
                    if (pc.Gender == PC_GENDER.FEMALE)
                    {
                        if (pc.JobBasic == PC_JOB.SWORDMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 60002550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「夜叉盔甲♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FENCER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 60002850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「神圣盔甲♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SCOUT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50007850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「寂静套装♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50009250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「白天鹅礼服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WIZARD)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50008050, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「宫廷魔导师服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50008150, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「蓝染巫女服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.VATES)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50009650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「可爱长袍♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WARLOCK)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50008350, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「小恶魔护士服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50008450, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「女匠师服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50008550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「白衣♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.RANGER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 50009950, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「亚马逊服♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.MERCHANT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001114, 112);
                            GiveItem(pc, 60100050, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「深闺闺秀旗袍♀」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "钥匙不对$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.SWORDMAN)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 60002750, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「夜叉盔甲♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FENCER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 60002650, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「神圣盔甲♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.SCOUT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009150, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「寂静套装♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.ARCHER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50007950, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「黑天鹅服♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.WIZARD)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009350, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「宫廷魔导师服♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.SHAMAN)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009550, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「草木染狩衣♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.VATES)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50008250, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「秩序长袍♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.WARLOCK)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009450, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「神秘套装♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.TATARABE)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009750, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「匠师服♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FARMASIST)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50009850, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「天才博士服♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.RANGER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50008650, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「登山夹克♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.MERCHANT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001114, 112);
                        GiveItem(pc, 50008750, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「集市夹克♂」$R;");
                        return;
                    }
                    break;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 255, "钥匙不对$R;");
        }
    }
}