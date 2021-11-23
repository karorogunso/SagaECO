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
                Say(pc, 11000666, 361, "摩根摩根！！$R;");
                return;
            }
            if (mogenmogen_mask.Test(mogenmogen.与梦梦对话) &&
                mogenmogen_mask.Test(mogenmogen.与歷卡对话) &&
                mogenmogen_mask.Test(mogenmogen.与莫波对话) &&
                mogenmogen_mask.Test(mogenmogen.与哈娜对话))
            {
                mogenmogen_mask.SetValue(mogenmogen.请求完成, true);
                Say(pc, 11000666, 361, "摩根摩根！！$R;");
                Say(pc, 11000747, 131, "呀！摩根摩根，怎麼了?$R;" +
                    "$R什麼？$R;" +
                    "聞到了小子們過得很好的味道？$R;" +
                    "$P是嗎？太好了！$R;" +
                    "您去看了孩子們了是吧？$R;" +
                    "$R謝謝，太感謝了$R;");
                return;
            }
            mogenmogen_mask.SetValue(mogenmogen.接受请求, true);
            Say(pc, 11000666, 131, "摩根摩根$R;");
            Say(pc, 11000747, 131, "這怎麼辦呢?$R;" +
                "\"摩根摩根没有力氣，大事不好了\"$R;" +
                "$R是不是擔心自己的孩子$R;" +
                "所以才這樣呢？$R;");
            switch (Select(pc, "想怎麼做呢?", "", "查問孩子的行踪", "什麼也不做"))
            {
                case 1:
                    Say(pc, 11000747, 131, "摩根摩根有4個孩子$R;" +
                        "$R長子「莫波」$R;" +
                        "委託了附近旅館照顧喔$R;" +
                        "$P「歷卡」$R;" +
                        "送了給相熟商人的兒子作生日禮物囉$R;" +
                        "$P長女『哈娜』$R;" +
                        "帕斯特市的商人帶走了$R;" +
                        "還有『夢夢』$R;" +
                        "在帕斯特寄去阿高普路斯$R;" +
                        "給好朋友作禮物了$R;" +
                        "$R都過得好吧…$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}