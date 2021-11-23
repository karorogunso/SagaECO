using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001478 : Event
    {
        public S11001478()
        {
            this.EventID = 11001478;
        }

        public override void OnEvent(ActorPC pc)
        {

          Say(pc, 131, "別的地方買不到呦。$R;", "美人魚食品店");
        }


    }
}


