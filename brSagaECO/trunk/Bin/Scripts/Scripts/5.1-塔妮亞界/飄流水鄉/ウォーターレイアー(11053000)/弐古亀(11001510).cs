using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001510 : Event
    {
        public S11001510()
        {
            this.EventID = 11001510;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "咕~咕……。$R;", "弐古亀");
        }


    }
}


