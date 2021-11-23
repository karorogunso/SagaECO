using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001512 : Event
    {
        public S11001512()
        {
            this.EventID = 11001512;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "咕哦哦…咕哦哦……。$R;", "四古亀");
        }


    }
}


