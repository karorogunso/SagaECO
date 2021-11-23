using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001876 : Event
    {
        public S11001876()
        {
            this.EventID = 11001876;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "これは夢だろうか……。$R;" +
            "$Rタイタニア種族しかいないはずの$R;" +
            "この街にエミルやドミニオンが……。$R;", "座っている男");
}
}

        
    }


