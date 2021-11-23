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
                Say(pc, 11000148, 131, "這裡是瑪歐斯的聚集地$R;" +
                    "$R只是您還沒有發現$R;" +
                    "像這樣的場所，別的地方也有很多呢!$R;" +
                    "$P您找也沒有用$R;" +
                    "如果沒有某人的許可的話$R;" +
                    "您絕對不會找到的啦$R;");
            Say(pc, 11000148, 131, "瑪歐斯如果缺水的話$R;" +
                "是不能夠移動的啦$R;" +
                "$R但是不會死的$R;" +
                "只會靜靜的，等待別人的幫助喔$R;");
        }
    }
}