using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000751 : Event
    {
        public S11000751()
        {
            this.EventID = 11000751;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "施工中$R;");
        }
    }
}