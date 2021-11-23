using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000680 : Event
    {
        public S11000680()
        {
            this.EventID = 11000680;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Level < 45)
            {
                Say(pc, 131, "从这里开始就是东方地牢$R;" +
                    "被毒菌污染的树林$R;" +
                    "危险的魔物到处乱窜$R;" +
                    "$R您的等级还是比较危险的$R;" +
                    "最好绕路走$R;");
                return;
            }
            Say(pc, 131, "从这里开始就是东方地牢$R;" +
                "被毒菌污染的树林$R;" +
                "危险的魔物到处乱窜$R;" +
                "$R您的等级应该没有问题$R;" +
                "但是要小心！$R;");
        }
    }
}