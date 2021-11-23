using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001909 : Event
    {
        public S11001909()
        {
            this.EventID = 11001909;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "いらっしゃいませーっ！$R;" +
            "$R温泉のおともに「のせタオル」は$R;" +
            "いかがですかー？$R;", "雑貨屋レンテ");
           OpenShopBuy(pc, 410);

            Say(pc, 131, "ありがとうございましたーっ！$R;", "雑貨屋レンテ");
}
}

        
    }


