using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50012000
{
    public class S11001210 : Event
    {
        public S11001210()
        {
            this.EventID = 11001210;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…………$R;");
        }
    }
}