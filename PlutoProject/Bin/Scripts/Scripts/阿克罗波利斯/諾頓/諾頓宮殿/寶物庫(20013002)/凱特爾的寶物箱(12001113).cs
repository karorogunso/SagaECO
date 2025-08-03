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
                switch (Select(pc, "开凯特尔的宝物箱吗？", "", "什么都不做", "用女王的钥匙"))
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
                            Say(pc, 255, "得到「复活药水」$R;");
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
                                Say(pc, 255, "得到「亚马逊护目镜♀」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.FARMASIST)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026450, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「大嘴怪帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.WARLOCK)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026350, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「小恶魔护士帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.VATES)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50037051, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「可爱无边帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SHAMAN)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                //NPCMOTION_ONE 112 0 12001115
                                GiveItem(pc, 50014751, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「蓝染巫女裙裤（紧身）」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.WIZARD)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50036550, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「外出发箍」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.ARCHER)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50027350, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「白天鹅帽」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SCOUT)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026150, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「肃清纱巾」$R;");
                                return;
                            }
                            if (pc.JobBasic == PC_JOB.SWORDMAN)
                            {
                                PlaySound(pc, 2420, false, 100, 50);
                                NPCMotion(pc, 12001113, 112);
                                GiveItem(pc, 50026050, 1);
                                TakeItem(pc, 10022600, 1);
                                Say(pc, 255, "得到「夜叉头盔」$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 255, "钥匙不对$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.MERCHANT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036750, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「集市发带」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.RANGER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50026550, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「护目镜登山帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50027450, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「天才博士帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50027650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「抗UV防晒帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.VATES)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036650, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「秩序缎带」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SHAMAN)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50037151, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「草木染乌帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.WIZARD)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036950, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「魔法耳套」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50026250, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「黑天鹅帽」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.SCOUT)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001113, 112);
                            GiveItem(pc, 50036850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「不动决心布发箍」$R;");
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
                        Say(pc, 255, "钥匙不对$R;");
                        break;
                }
                return;
            }
            Say(pc, 255, "宝物箱上锁了$R;");
        }
    }
}