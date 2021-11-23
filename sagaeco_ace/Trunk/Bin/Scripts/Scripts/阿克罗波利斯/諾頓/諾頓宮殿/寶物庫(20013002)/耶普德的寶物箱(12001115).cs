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
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 10000604, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「复活药水」$R;");
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
                            Say(pc, 255, "得到「蓝染巫女裙裤（宽松）」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.TATARABE)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50014850, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「女匠师长裤♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.FARMASIST)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50014950, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「医务室短裤♀」$R;");
                            return;
                        }
                        if (pc.JobBasic == PC_JOB.ARCHER)
                        {
                            PlaySound(pc, 2420, false, 100, 50);
                            NPCMotion(pc, 12001115, 112);
                            GiveItem(pc, 50072252, 1);
                            TakeItem(pc, 10022600, 1);
                            Say(pc, 255, "得到「龙角箭筒（粉色）」$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 255, "钥匙不对$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.ARCHER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50072251, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「龙角箭筒（黑色）」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.TATARABE)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015350, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「匠师长裤♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.FARMASIST)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015450, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「天才博士短裤♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.RANGER)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015050, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「登山短裤♂」$R;");
                        return;
                    }
                    if (pc.JobBasic == PC_JOB.MERCHANT)
                    {
                        PlaySound(pc, 2420, false, 100, 50);
                        NPCMotion(pc, 12001115, 112);
                        GiveItem(pc, 50015150, 1);
                        TakeItem(pc, 10022600, 1);
                        Say(pc, 255, "得到「集市短裤♂」$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "钥匙不对$R;");
                    break;
            }
        }
    }
}