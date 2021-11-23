using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000674 : Event
    {
        public S11000674()
        {
            this.EventID = 11000674;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];

            if (pc.Fame < 10)
            {
                Say(pc, 11000674, 361, "哞哞！！！$R;");
                Say(pc, 0, 131, "警戒森嚴，無法接近$R;");
                return;
            }
            if (mogenmogen_mask.Test(mogenmogen.接受请求) && !mogenmogen_mask.Test(mogenmogen.与哈娜对话))
            {
                mogenmogen_mask.SetValue(mogenmogen.与哈娜对话, true);
                Say(pc, 11000674, 361, "哞嗚嗚！$R;" +
                    "哞哞...$R;");
                Say(pc, 0, 131, pc.Name + "在嗅氣味$R;" +
                    "$R好像聞到摩根摩根的味道$R;");
                Say(pc, 11000674, 361, "哞嗚~哞嗚~$R;");
                Say(pc, 0, 131, "哈娜好像在跟您道謝呢$R;");
                return;
            }
            Say(pc, 11000674, 131, "哞嗚！$R;");
        }
    }
}