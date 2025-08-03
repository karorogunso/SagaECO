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
                "这里是给宠物准备的家$R;" +
                "$R叫「宠物小屋」哦$R;");
            if (pc.Fame < 10)
            {
                switch (Select(pc, "想怎么做呢？", "", "想了解一下关于您", "什么是姓名表？", "想拿到姓名表", "什么也不做"))
                {
                    case 1:
                        Say(pc, 131, "我是宠物饲养员$R;" +
                            "$R跟爷爷一起$R;" +
                            "在法伊斯特的大自然里$R;" +
                            "忙着繁殖和饲养宠物哦$R;" +
                            "$P家里的爷爷$R;" +
                            "看起来虽然有点奇怪$R;" +
                            "但却是个相当有实力的宠物饲养员呀$R;");
                        break;
                    case 2:
                        Say(pc, 131, "就是写宠物名字的板$R;" +
                            "只要写上名字$R;" +
                            "便可以更换宠物的名字哦$R;" +
                            "$R姓名板从我这买就可以了$R;");
                        break;
                    case 3:
                        Say(pc, 131, "每张1000金币$R;");
                        switch (Select(pc, "支付1000金币吗？", "", "支付", "不支付"))
                        {
                            case 1:
                                if (pc.Gold < 1000)
                                {

                                    Say(pc, 131, "钱不够呀？$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10037400, 1))
                                {
                                    GiveItem(pc, 10037400, 1);
                                    pc.Gold -= 1000;
                                    Say(pc, 131, "给我取个可爱的名字吧~$R;");
                                    return;
                                }
                                Say(pc, 131, "行李太多了哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            switch (Select(pc, "想怎么做呢？", "", "想了解一下关于您", "治疗宠物", "什么是姓名表？", "想要姓名表", "找宠物主人", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "我是宠物饲养员$R;" +
                        "$R跟爷爷一起$R;" +
                        "在法伊斯特的大自然里$R;" +
                        "忙着繁殖和饲养宠物唷$R;" +
                        "$P家里的爷爷$R;" +
                        "看起来虽然有点奇怪$R;" +
                        "但却是个相当有实力的宠物饲养员呀$R;");
                    break;
                case 2:
                    PetRecover(pc, 1);
                    break;
                case 3:
                    Say(pc, 131, "就是写宠物名字的板$R;" +
                        "只要写上名字$R;" +
                        "便可以更换宠物的名字哦$R;" +
                        "$R姓名板从我这买就可以了$R;");
                    break;
                case 4:
                    Say(pc, 131, "每张1000金币$R;");
                    switch (Select(pc, "支付1000金币吗？", "", "支付", "不支付"))
                    {
                        case 1:
                            if (pc.Gold < 1000)
                            {

                                Say(pc, 131, "钱不够呀？$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10037400, 1))
                            {
                                GiveItem(pc, 10037400, 1);
                                pc.Gold -= 1000;
                                Say(pc, 131, "给我取个可爱的名字吧~$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了哦$R;");
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