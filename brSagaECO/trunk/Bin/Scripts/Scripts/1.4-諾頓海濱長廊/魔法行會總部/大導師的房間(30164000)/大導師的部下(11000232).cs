using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30164000
{
    public class S11000232 : Event
    {
        public S11000232()
        {
            this.EventID = 11000232;
        }


        public override void OnEvent(ActorPC pc)
        {
            BitMask<Reset> mask = pc.CMask["Reset"];
            BitMask<NDFlags> NDFlags_mask = pc.CMask["NDFlags"];

            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "找大導師有什麼事情嗎？", "", "技能的幻覺", "技能重設", "狀態重設", "沒有特別的事情"))
                {
                    case 1:
                        洗一轉技能點(pc);
                        break;
                    case 2:
                        洗技能點(pc);
                        break;
                    case 3:
                        系屬性點(pc);
                        break;
                }
                return;
            }

            if (NDFlags_mask.Test(NDFlags.大导师第一次对话))
            {
                Say(pc, 131, "這裡是魔法行會總部$R;" +
                    "綜合管理魔法師的地方$R;" +
                    "$R來的好…$R;");
                return;
            }


            if (!mask.Test(Reset.洗過技能點) && !mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大導師有什麼事情嗎？", "", "技能重設", "狀態重設", "沒有特別的事情"))
                {
                    case 1:
                        洗技能點(pc);
                        break;
                    case 2:
                        系屬性點(pc);
                        break;
                }
            }

            if (!mask.Test(Reset.洗過技能點) && mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大導師有什麼事情嗎？", "", "技能重設", "沒有特別的事情"))
                {
                    case 1:
                        洗技能點(pc);
                        break;
                }
            }

            if (mask.Test(Reset.洗過技能點) && !mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大導師有什麼事情嗎？", "","狀態重設", "沒有特別的事情"))
                {
                    case 1:
                        系屬性點(pc);
                        break;
                }
            }
        }

        void 洗技能點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }
            BitMask<Reset> mask = pc.CMask["Reset"];

            if (pc.Skills.Count == 0 &&
                pc.Skills2.Count == 0)
            {
                Say(pc, 131, "您不是沒有學過技能嗎？$R;" +
                    "$R沒有學過，怎麼能說忘記呢$R;" +
                    "快滾吧$R;");
                return;
            }

            Say(pc, 131, "什麼？$R;" +
                "$R想把所有的技能刪除，重新學習？$R;");
            switch (Select(pc, "重設技能嗎？", "", "重設", "不重設"))
            {
                case 1:
                    Say(pc, 131, "重設技能會帶給您很多麻煩$R;" +
                        "不能多次進行。$R;" +
                        "$R確定要做嗎？$R;");
                    switch (Select(pc, "重設技能嗎？", "", "重設", "不重設"))
                    {
                        case 1:
                            mask.SetValue(Reset.洗過技能點, true);
                            //_2A43 = true;
                            ResetSkill(pc, 1);
                            ResetSkill(pc, 2);
                            //SKILLRESET_ALL
                            Say(pc, 131, "那也不錯$R;" +
                                "大導師，請借給我力量吧$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "把所有的技能都忘了$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void 系屬性點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }
            BitMask<Reset> mask = pc.CMask["Reset"];

            Say(pc, 131, "什麼？$R;" +
                "$R想回到出生狀態$R;" +
                "重新分配獎勵點數嗎？$R;");
            switch (Select(pc, "重設狀態嗎？", "", "重設", "不重設"))
            {
                case 1:
                    Say(pc, 131, "重設狀態會帶您給很多麻煩$R;" +
                        "不能多次進行。$R;" +
                        "$R確定要做嗎？$R;");
                    switch (Select(pc, "重設狀態嗎？", "", "重設", "不重設"))
                    {
                        case 1:

                            mask.SetValue(Reset.洗過屬性點, true);
                            //_2A47 = true;
                            ResetStatusPoint(pc);
                            //STATUSRESET
                            Say(pc, 131, "那也不錯$R;" +
                                "大導師，請借給我力量吧$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "狀態恢復初始值了$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void 洗一轉技能點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }

            int a = (int)pc.Level * 1000;

            if (pc.Skills.Count == 0 &&
                pc.Skills2.Count == 0)
            {
                Say(pc, 131, "您不是沒有學過技能嗎？$R;" +
                    "$R沒有學過，怎麼能說忘記呢$R;" +
                    "快滾吧$R;");
                return;
            }

            Say(pc, 131, "那麼當做使用費需要$R;" +
                a + "金幣$R;" +
                "沒問題吧？$R;");

            switch (Select(pc, "怎麼辦呢？", "", "支付", "不支付"))
            {
                case 1:
                    if (pc.Gold < a)
                    {
                        Say(pc, 131, "錢不夠呀$R;");
                        return;
                    }

                    Say(pc, 131, "那也不錯$R;" +
                        "大導師，請借給我力量吧$R;");
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    ResetSkill(pc, 1);
                    //SKILLRESET_ONE
                    if (pc.Level == 0)
                    {
                        return;
                    }
                    pc.Gold -= a;
                    Say(pc, 131, "支付了" + a + "金幣$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}