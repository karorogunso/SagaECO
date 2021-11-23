using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20013002
{
    public class S12001113 : Event
    {
        public S12001113()
        {
            this.EventID = 12001113;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (CountItem(pc, 10022600) >= 1)
            {
                switch (Select(pc, "開凱特爾的寶物箱嗎？", "", "什麼都不做", "用女王的鑰匙"))
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
                            //_6a05 = true;
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 10000604, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「復活藥水」$R;");
                            return;
                        }
                        if (pc.Gender == PC_GENDER.FEMALE)
                        {
                            if (pc.JobBasic == PC_JOB.RANGER)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50027550, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「牛仔冒險護目鏡（女）」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.FARMASIST)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026450, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「大口魔怪帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.WARLOCK)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026350, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「惡女護士帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.VATES)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50037051, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「可愛的冠帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SHAMAN)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                //NPCMOTION_ONE 112 0 12001115
                                GiveItem(pc, 50014751, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「深藍色修身長筒褲（女）」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.WIZARD)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50036550, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「蝴蝶結頭飾帶」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.ARCHER)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50027350, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「白鳥圓邊帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SCOUT)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026150, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「夜忍頭罩」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SWORDMAN)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026050, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「夜叉頭盔」$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 255, "鑰匙不對$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.MERCHANT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036750, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「商店頭帶」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.RANGER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50026550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「深山探險護目鏡」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50027450, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「天才博士圓邊帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50027650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「防紫外線太陽帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.VATES)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「神諭緞帶」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50037151, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「翡翠高身圓邊帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WIZARD)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036950, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「魔法耳朵斗篷」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50026250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「黑色圓邊帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SCOUT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「堅韌的頭飾帶」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SWORDMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50027250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「夜叉面具」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "鑰匙不對$R;");
                        break;
                }
                return;
            }
            Say(pc, 255, "寶物箱上鎖了$R;");
        }
    }
}