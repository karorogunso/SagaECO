using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001515 : Event
    {
        public S11001515()
        {
            this.EventID = 11001515;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "哫哫哫哫……。$R;", "七古亀");
        }


    }
}


