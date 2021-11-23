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
            Say(pc, 131, "南邊有廣闊的榖倉之地$R;" +
                "總是有好吃的穀物！$R;" +
                "$R哇哈哈哈哈哈！$R;" +
                "$P…$R;" +
                "$P但是不要過橋的那一邊$R;" +
                "知道了嗎？這是警告！$R;");
        }
    }
}