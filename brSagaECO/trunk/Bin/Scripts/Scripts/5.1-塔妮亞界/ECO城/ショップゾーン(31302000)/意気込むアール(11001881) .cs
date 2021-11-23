using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001881 : Event
    {
        public S11001881()
        {
            this.EventID = 11001881;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "なぁ、早く行こうぜっ！$R;", "意気込むアール");

            Say(pc, 11001880, 0, "ま、待って！$R;" +
            "$R深呼吸させてっ！$R;", "意気込むエリュ");
}
}

        
    }


