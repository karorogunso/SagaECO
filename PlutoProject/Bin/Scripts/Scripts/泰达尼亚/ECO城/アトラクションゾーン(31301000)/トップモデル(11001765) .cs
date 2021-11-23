using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001765 : Event
    {
        public S11001765()
        {
            this.EventID = 11001765;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "もう、浮き輪がなくっちゃ$R;" +
            "泳げなぁ～いっ！$R;", "トップモデル");
}
}

        
    }


