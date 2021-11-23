using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001490 : Event
    {
        public S11001490()
        {
            this.EventID = 11001490;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "一點點的話、能夠明白。$R;", "美人魚");
        }


    }
}


