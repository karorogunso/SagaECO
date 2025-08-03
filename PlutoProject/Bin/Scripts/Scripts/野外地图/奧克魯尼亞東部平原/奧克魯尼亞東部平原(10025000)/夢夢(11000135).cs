using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:夢夢(11000135) X:41 Y:136
namespace SagaScript.M10025000
{
    public class S11000135 : Event
    {
        public S11000135()
        {
            this.EventID = 11000135;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];

            if (mogenmogen_mask.Test(mogenmogen.接受请求) && !mogenmogen_mask.Test(mogenmogen.与梦梦对话))
            {
                mogenmogen_mask.SetValue(mogenmogen.与梦梦对话, true);
                Say(pc, 11000135, 361, "咕噜咕噜咕噜……?$R;" +
                     "咕噜咕噜???$R;");
                Say(pc, 0, 131, pc.Name + "在嗅气味$R;" +
                    "$R怎么觉得好像有摩戈摩戈的味道$R;");
                Say(pc, 11000135, 361, "咕噜咕噜隆~♪$R;");
                Say(pc, 0, 131, "梦梦说谢谢$R;" +
                    "$P…好像有这样的想法$R;");
                return;
            }
            Say(pc, 11000050, 131, "啊啊!!$R不要打我们梦梦!$R;");
        }
    }
}
