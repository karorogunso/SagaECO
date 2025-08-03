using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000148 : Event
    {
        public S11000148()
        {
            this.EventID = 11000148;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
                Say(pc, 11000148, 131, "这裡是鱼人的聚集地$R;" +
                    "$R只是您还没有发现$R;" +
                    "像这样的场所，别的地方也有很多呢!$R;" +
                    "$P您找也没有用$R;" +
                    "如果没有某人的许可的话$R;" +
                    "您绝对不会找到的啦$R;");
            Say(pc, 11000148, 131, "鱼人如果缺水的话$R;" +
                "是不能够移动的啦$R;" +
                "$R但是不会死的$R;" +
                "只会静静的，等待别人的帮助喔$R;");
        }
    }
}