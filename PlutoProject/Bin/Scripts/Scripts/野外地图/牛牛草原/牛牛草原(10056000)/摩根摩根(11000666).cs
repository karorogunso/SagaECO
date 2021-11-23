using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000666 : Event
    {
        public S11000666()
        {
            this.EventID = 11000666;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];
            BitMask<mogenmogen> mogenmogen_Amask = pc.AMask["mogenmogen"];
            if (mogenmogen_Amask.Test(mogenmogen.获得宠物))
            {
                Say(pc, 11000666, 361, "摩戈摩戈！！$R;");
                return;
            }
            if (mogenmogen_mask.Test(mogenmogen.与梦梦对话) &&
                mogenmogen_mask.Test(mogenmogen.与歷卡对话) &&
                mogenmogen_mask.Test(mogenmogen.与莫波对话) &&
                mogenmogen_mask.Test(mogenmogen.与哈娜对话))
            {
                mogenmogen_mask.SetValue(mogenmogen.请求完成, true);
                Say(pc, 11000666, 361, "摩戈摩戈！！$R;");
                Say(pc, 11000747, 131, "呀！摩戈摩戈，怎么了?$R;" +
                    "$R什么？$R;" +
                    "闻到了小子们过得很好的味道？$R;" +
                    "$P是吗？太好了！$R;" +
                    "您去看了孩子们了是吧？$R;" +
                    "$R谢谢，太感谢了$R;");
                return;
            }
            mogenmogen_mask.SetValue(mogenmogen.接受请求, true);
            Say(pc, 11000666, 131, "摩戈摩戈$R;");
            Say(pc, 11000747, 131, "这怎么办呢?$R;" +
                "\"摩戈摩戈没有力气，大事不好了\"$R;" +
                "$R是不是担心自己的孩子$R;" +
                "所以才这样呢？$R;");
            switch (Select(pc, "想怎么做呢?", "", "查问孩子的行踪", "什么也不做"))
            {
                case 1:
                    Say(pc, 11000747, 131, "摩戈摩戈有4个孩子$R;" +
                        "$R长子「莫波」$R;" +
                        "委托了附近旅馆照顾喔$R;" +
                        "$P「历卡」$R;" +
                        "送了给熟人商人的儿子作生日礼物啰$R;" +
                        "$P长女『哈娜』被$R;" +
                        "法伊斯特市的商人带走了$R;" +
                        "还有『梦梦』$R;" +
                        "在法伊斯特寄去阿克罗波利斯$R;" +
                        "给好朋友作礼物了$R;" +
                        "$R都过得好吧…$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}