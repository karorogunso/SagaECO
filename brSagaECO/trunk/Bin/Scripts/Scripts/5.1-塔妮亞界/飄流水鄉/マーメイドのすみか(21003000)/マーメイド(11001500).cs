using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001500 : Event
    {
        public S11001500()
        {
            this.EventID = 11001500;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哦哦……第一次看到人類呢。$R;", "美人魚");
        }


    }
}


