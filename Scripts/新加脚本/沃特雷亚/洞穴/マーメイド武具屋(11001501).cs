using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001501 : Event
    {
        public S11001501()
        {
            this.EventID = 11001501;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "啊～真是稀奇的客人呢。$R;", "美人魚武器店");
        }


    }
}


