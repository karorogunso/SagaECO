using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000824 : Event
    {
        public S11000824()
        {
            this.EventID = 11000824;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];
            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];
            NavigateCancel(pc);

            Say(pc, 131, "欢迎到摩戈机场$R;" +
                "从这里乘坐飞空庭可以到军舰岛$R;");

            switch (Select(pc, "欢迎到摩戈机场", "", "乘坐飞空庭到军舰岛", "乘坐飞空庭到艾恩萨乌斯", "什么也不做"))
            {
                case 1:
                    if (TravelFGarden_mask.Test(TravelFGarden.已經辦理手續))
                    {
                        Say(pc, 131, "客人的手续已完毕$R;" +
                            "请马上登机吧$R;");
                        return;
                    }

                    if (Knights_mask.Test(Knights.加入西軍騎士團))//_0a35)
                    {
                        TravelFGarden_mask.SetValue(TravelFGarden.已經辦理手續, true);
                        //_6a57 = true;
                        Say(pc, 131, "隶属西军是吗？$R;" +
                            "$R西军的人是免费的$R;" +
                            "可以直接乘坐$R;");
                        return;
                    }

                    Say(pc, 131, "到军舰岛需要300金币$R;");
                    switch (Select(pc, "付300金币吗？", "", "付", "不付"))
                    {
                        case 1:
                            if (pc.Gold > 299)
                            {
                                TravelFGarden_mask.SetValue(TravelFGarden.已經辦理手續, true);
                                //_6a57 = true;
                                pc.Gold -= 300;
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "现在马上出发了$R;" +
                                    "请马上登机吧$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 131, "钱不够$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    ShowEffect(pc, 11000824, 4508);
                    Wait(pc, 2000);
                    Say(pc, 131, "到艾恩萨乌斯的直达航班$R现在不运行，$R;" +
                        "直达航班…$R;" +
                        "什么时候才开始恢复，还不清楚阿。$R;" +
                        "$P想去艾恩萨乌斯，$R首先到阿克罗尼亚大陆，$R;" +
                        "然后再利用陆路入境吧。$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}