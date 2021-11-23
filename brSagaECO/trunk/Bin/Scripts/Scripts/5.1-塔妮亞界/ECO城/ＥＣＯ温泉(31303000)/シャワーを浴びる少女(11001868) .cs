using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31303000
{
    public class S11001868 : Event
    {
        public S11001868()
        {
            this.EventID = 11001868;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "假如……$R;" +
            "有浴帽在就好了。$R;", "花灑沐浴少女");
            /*
            Say(pc, 0, "シャンプーハットが$R;" +
            "あればいいのに……。$R;", "シャワーを浴びる少女");
            */
        }


    }
}


