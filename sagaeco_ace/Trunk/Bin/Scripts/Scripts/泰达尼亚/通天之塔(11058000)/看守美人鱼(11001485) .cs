using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001485 : Event
    {
        public S11001485()
        {
            this.EventID = 11001485;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "300年前...泰达尼亚王将$R;" +
            "这扇门封印...$R;" +
            "我和我赖以生存的地方也被封印了$R;" +
            "$R这之后,再也没有人来过....$R;" +
            "。$R;", "");

            Say(pc, 131, "抱歉...。$R;" +
            "不应该提起以前的事情$R;" +
            "共同来打开尘封的泰达尼亚吧!$R;" +
            "$R我和你一同期待着...$R;", "");
        }
    }
}




