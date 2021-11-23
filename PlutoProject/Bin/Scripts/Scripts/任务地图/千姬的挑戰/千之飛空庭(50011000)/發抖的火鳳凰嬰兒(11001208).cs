using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50011000
{
    public class S11001208 : Event
    {
        public S11001208()
        {
            this.EventID = 11001208;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "…………$R;");
            Say(pc, 0, 131, "(好像很害怕呢……)$R;", " ");
        }
    }
}