using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001843 : Event
    {
        public S11001843()
        {
            this.EventID = 11001843;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "この先は「ショップゾーン」だ。$R;" +
            "$Rショップゾーンでは他では$R;" +
            "手に入らない珍しいアイテムが$R;" +
            "売られている。$R;" +
            "$Rまぁ、ゆっくりしていくが良い。$R;", "警備員");
        }
    }
}


