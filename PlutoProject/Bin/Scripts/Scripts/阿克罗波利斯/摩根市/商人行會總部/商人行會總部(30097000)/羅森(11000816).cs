using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000816 : Event
    {
        public S11000816()
        {
            this.EventID = 11000816;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我父亲在摩戈算最富有的哦$R;");
        }
    }
}