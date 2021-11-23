using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001844 : Event
    {
        public S11001844()
        {
            this.EventID = 11001844;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "……。$R;" +
            "$R……我、已經……$R;" +
            "無法抵抗命運……。$R;", "威廉");

            //
            /*
            Say(pc, 65535, "……。$R;" +
            "$R……もう、俺は……$R;" +
            "運命にあらがうことはしない……。$R;", "ウィルヘルム");
            */
        }


    }
}


