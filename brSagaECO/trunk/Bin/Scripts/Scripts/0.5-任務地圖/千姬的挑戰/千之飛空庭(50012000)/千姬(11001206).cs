using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50012000
{
    public class S11001206 : Event
    {
        public S11001206()
        {
            this.EventID = 11001206;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "阿！阿！看見了……$R;");
        }
    }
}