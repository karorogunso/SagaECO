using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20023000
{
    public class S11001099 : Event
    {
        public S11001099()
        {
            this.EventID = 11001099;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];
            Say(pc, 131, "歡迎來到馬克特碼頭唷$R;" +
                "去唐卡的飛空庭飛機場在這裡喔。$R;");
            //PARAM ME.WORK0 = ME.SKILL_LV.706

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "呵呵$R;" +
                    pc.Name + "不是嗎！$R;" +
                    "$R沒有必要辦理乘搭手續$R;" +
                    "請直接上飛空庭吧$R;");
                return;
            }

            if (TravelFGarden_mask.Test(TravelFGarden.已办理去唐卡手续))
            {
                Say(pc, 131, "請上飛空庭唷$R;");
                return;
            }
            switch (Select(pc, "歡迎來到馬克特碼頭", "", "什麼也不做", "辦理搭乘手續"))
            {
                case 1:
                    break;
                case 2:
                    if (CountItem(pc, 10041500) >= 1 && !TravelFGarden_mask.Test(TravelFGarden.持有南军团证))
                    {
                        TravelFGarden_mask.SetValue(TravelFGarden.持有南军团证, true);
                        //_5a82 = true;
                        Say(pc, 131, "那是『阿伊恩薩烏斯騎士團證』嗎？$R;" +
                            "$R雖然帶來了，不過不好意思，$R;" +
                            "入境唐卡的時候，沒有用的。$R;" +
                            "$P雖然唐卡是$R;" +
                            "我們阿伊恩薩烏斯的領地，$R;" +
                            "$R但唐卡是『經濟特區』$R;" +
                            "所以有自治權喔$R;" +
                            "$R以個人身份入境時，$R;" +
                            "手續跟普通人是一樣的呀$R;" +
                            "$P阿伊恩城的空軍長官正在銷售$R;" +
                            "$R『唐卡島入國許可證』，去買吧。$R;");
                        return;
                    }
                    Say(pc, 131, "那麼，請出示$R;" +
                        "『唐卡島入國許可證』喔$R;");
                    switch (Select(pc, "出示入境許可證嗎？", "", "不出示", "出示。"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (CountItem(pc, 10042300) > 0)
                            {
                                TravelFGarden_mask.SetValue(TravelFGarden.已办理去唐卡手续, true);
                                //_5a83 = true;
                                PlaySound(pc, 2040, false, 100, 50);
                                TakeItem(pc, 10042300, 1);
                                Say(pc, 131, "……$R;" +
                                    "沒問題了$R;" +
                                    "$R那麼在這裡回收許可證了。$R;" +
                                    "請搭乘飛空庭唷$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 131, "不出示入境許可證$R;" +
                                "就不能乘飛空庭呀。$R;" +
                                "請出示『唐卡島入國許可證』$R;");
                            break;
                    }
                    break;
            }
            //*/
        }
    }
}