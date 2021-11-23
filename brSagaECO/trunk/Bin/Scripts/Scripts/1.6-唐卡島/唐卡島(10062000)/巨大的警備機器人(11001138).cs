using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001138 : Event
    {
        public S11001138()
        {
            this.EventID = 11001138;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "鈴鈴…鈴……$R;");
        }
    }
}