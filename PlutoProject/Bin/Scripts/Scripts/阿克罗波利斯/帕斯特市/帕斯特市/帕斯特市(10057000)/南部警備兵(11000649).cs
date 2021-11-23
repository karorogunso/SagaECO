using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000649 : Event
    {
        public S11000649()
        {
            this.EventID = 11000649;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "南边是广阔的谷仓地带$R;" +
                "总是有好吃的谷物！$R;" +
                "$R哇哈哈哈哈哈！$R;" +
                "$P…$R;" +
                "$P但是不要过桥的那一边$R;" +
                "知道了吗？这是警告！$R;");
        }
    }
}