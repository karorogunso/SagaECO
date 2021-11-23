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

            Say(pc, 131, "歡迎到摩根機場$R;" +
                "從這裡乘坐飛空庭可以到軍艦島唷$R;");
            
            switch (Select(pc, "歡迎到摩根機場", "", "乘坐飛空庭到軍艦島", "乘坐飛空庭到阿伊恩薩烏斯", "什麼也不做"))
            {
                case 1:
                    if (TravelFGarden_mask.Test(TravelFGarden.已經辦理手續))
                    {
                        Say(pc, 131, "客人的手續已完畢$R;" +
                            "請馬上登機吧$R;");
                        return;
                    }

                    if (Knights_mask.Test(Knights.加入西軍騎士團))//_0a35)
                    {
                        TravelFGarden_mask.SetValue(TravelFGarden.已經辦理手續, true);
                        //_6a57 = true;
                        Say(pc, 131, "隸屬西軍是嗎？$R;" +
                            "$R西軍的人是免費的$R;" +
                            "可以直接乘坐唷$R;");
                        return;
                    }

                    Say(pc, 131, "到軍艦島需要300金幣$R;");
                    switch (Select(pc, "付300金幣嗎？", "", "付", "不付"))
                    {
                        case 1:
                            if (pc.Gold > 299)
                            {
                                TravelFGarden_mask.SetValue(TravelFGarden.已經辦理手續, true);
                                //_6a57 = true;
                                pc.Gold -= 300;
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "現在馬上出發了$R;" +
                                    "請馬上登機吧$R;");
                                return;
                            }
                            PlaySound(pc, 2041, false, 100, 50);
                            Say(pc, 131, "錢不夠$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    ShowEffect(pc, 11000824, 4508);
                    Wait(pc, 2000);
                    Say(pc, 131, "到阿伊恩薩烏斯的直達航班$R現在不運行，$R;" +
                        "直達航班…$R;" +
                        "什麼時候才開始恢復，還不清楚阿。$R;" +
                        "$P想去阿伊恩薩烏斯，$R首先到奧克魯尼亞大陸，$R;" +
                        "然後再利用陸路入境吧。$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}