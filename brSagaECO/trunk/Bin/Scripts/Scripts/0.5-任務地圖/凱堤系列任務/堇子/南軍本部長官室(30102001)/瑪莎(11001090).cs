using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102001
{
    public class S11001090 : Event
    {
        public S11001090()
        {
            this.EventID = 11001090;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001090, 131, "謝謝$R;" +
                "$R全靠您了$R;");
        }
    }
}