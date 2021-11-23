using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50036000
{
    public class S11001341 : Event
    {
        public S11001341()
        {
            this.EventID = 11001341;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "何も入ってない…。$R;", " ");
        }
    }
}