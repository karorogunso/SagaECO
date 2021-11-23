using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000747 : Event
    {
        public S11000747()
        {
            this.EventID = 11000747;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哦！您好！$R;" +
                "這裡是給寵物準備的家$R;" +
                "$R叫「寵物小屋」唷$R;");
            if (pc.Fame < 10)
            {
                switch (Select(pc, "想怎麼做呢？", "", "想了解一下關於您", "什麼是姓名表？", "想拿到姓名表", "什麼也不做"))
                {
                    case 1:
                        Say(pc, 131, "我是寵物飼養員$R;" +
                            "$R跟爺爺一起$R;" +
                            "在帕斯特的大自然裡$R;" +
                            "忙著繁殖和飼養寵物唷$R;" +
                            "$P家裡的爺爺$R;" +
                            "看起來雖然有點奇怪$R;" +
                            "但卻是個相當有實力的寵物飼養員呀$R;");
                        break;
                    case 2:
                        Say(pc, 131, "就是寫寵物名字的板$R;" +
                            "只要寫上名字$R;" +
                            "便可以更換寵物的名字唷$R;" +
                            "$R姓名板從我這買就可以了$R;");
                        break;
                    case 3:
                        Say(pc, 131, "每張1000金幣$R;");
                        switch (Select(pc, "支付1000金幣嗎？", "", "支付", "不支付"))
                        {
                            case 1:
                                if (pc.Gold < 1000)
                                {

                                    Say(pc, 131, "錢不夠呀？$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10037400, 1))
                                {
                                    GiveItem(pc, 10037400, 1);
                                    pc.Gold -= 1000;
                                    Say(pc, 131, "給我取個可愛的名字吧~$R;");
                                    return;
                                }
                                Say(pc, 131, "行李太多了唷$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            switch (Select(pc, "想怎麼做呢？", "", "想了解一下關於您", "治療寵物", "什麼是姓名表？", "想要姓名表", "找寵物主人", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "我是寵物飼養員$R;" +
                        "$R跟爺爺一起$R;" +
                        "在帕斯特的大自然裡$R;" +
                        "忙著繁殖和飼養寵物唷$R;" +
                        "$P家裡的爺爺$R;" +
                        "看起來雖然有點奇怪$R;" +
                        "但卻是個相當有實力的寵物飼養員呀$R;");
                    break;
                case 2:
                    PetRecover(pc, 1);
                    break;
                case 3:
                    Say(pc, 131, "就是寫寵物名字的板$R;" +
                        "只要寫上名字$R;" +
                        "便可以更換寵物的名字唷$R;" +
                        "$R姓名板從我這買就可以了$R;");
                    break;
                case 4:
                    Say(pc, 131, "每張1000金幣$R;");
                    switch (Select(pc, "支付1000金幣嗎？", "", "支付", "不支付"))
                    {
                        case 1:
                            if (pc.Gold < 1000)
                            {

                                Say(pc, 131, "錢不夠呀？$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10037400, 1))
                            {
                                GiveItem(pc, 10037400, 1);
                                pc.Gold -= 1000;
                                Say(pc, 131, "給我取個可愛的名字吧~$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了唷$R;");
                            break;
                    }
                    break;
                case 5:
                    Say(pc, 131, "……$R;");
                    break;
            }

        }
    }
}