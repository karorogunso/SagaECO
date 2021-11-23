using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001514 : Event
    {
        public S11001514()
        {
            this.EventID = 11001514;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "咕哦……、咕哦哦哦……。$R;", "六古亀");
        }


    }
}


