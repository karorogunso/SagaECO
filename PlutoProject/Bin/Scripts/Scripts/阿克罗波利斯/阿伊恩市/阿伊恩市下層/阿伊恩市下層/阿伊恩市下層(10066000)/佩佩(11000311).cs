using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000311 : Event
    {
        public S11000311()
        {
            this.EventID = 11000311;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是南方地牢$R;");
        }
    }
}