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
            Say(pc, 131, "欢迎来到马尔科特码头$R;" +
                "去唐卡的飞空庭机场在这里。$R;");
            //PARAM ME.WORK0 = ME.SKILL_LV.706

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "呵呵$R;" +
                    pc.Name + "不是吗！$R;" +
                    "$R没有必要办理乘搭手续$R;" +
                    "请直接上飞空庭吧$R;");
                return;
            }

            if (TravelFGarden_mask.Test(TravelFGarden.已办理去唐卡手续))
            {
                Say(pc, 131, "请上飞空庭$R;");
                return;
            }
            switch (Select(pc, "欢迎来到马尔科特码头", "", "什么也不做", "办理搭乘手续"))
            {
                case 1:
                    break;
                case 2:
                    if (CountItem(pc, 10041500) >= 1 && !TravelFGarden_mask.Test(TravelFGarden.持有南军团证))
                    {
                        TravelFGarden_mask.SetValue(TravelFGarden.持有南军团证, true);
                        //_5a82 = true;
                        Say(pc, 131, "那是『阿伊恩萨乌斯骑士团证』吗？$R;" +
                            "$R虽然带来了，不过不好意思，$R;" +
                            "入境唐卡的时候，没有用的。$R;" +
                            "$P虽然唐卡是$R;" +
                            "我们艾恩萨乌斯的领地，$R;" +
                            "$R但唐卡是『经济特区』$R;" +
                            "所以有自治权喔$R;" +
                            "$R以个人身份入境时，$R;" +
                            "手续跟普通人是一样的呀$R;" +
                            "$P艾恩萨乌斯的空军长官正在销售$R;" +
                            "$R『唐卡岛入国许可证』，去买吧。$R;");
                        return;
                    }
                    Say(pc, 131, "那么，请出示$R;" +
                        "『唐卡岛入国许可证』$R;");
                    switch (Select(pc, "出示入境许可证吗？", "", "不出示", "出示。"))
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
                                    "没问题了$R;" +
                                    "$R那么在这里回收许可证了。$R;" +
                                    "请搭乘飞空庭$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 131, "不出示入境许可证$R;" +
                                "就不能乘飞空庭呀。$R;" +
                                "请出示『唐卡岛入国许可证』$R;");
                            break;
                    }
                    break;
            }
            //*/
        }
    }
}