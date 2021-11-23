using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000323 : Event
    {
        public S11000323()
        {
            this.EventID = 11000323;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哥哥，您太帥了。$R;");
        }
    }
}