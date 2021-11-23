using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001903 : Event
    {
        public S11001903()
        {
            this.EventID = 11001903;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "……。$R;", "バニーショップ");
            OpenShopBuy(pc, 411);
}
}

        
    }


