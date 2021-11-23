using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001840 : Event
    {
        public S11001840()
        {
            this.EventID = 11001840;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "なによこれ～！$R;" +
            "$R水がでないじゃないっ！$R;", "シャワーを浴びる女");
}
}

        
    }


