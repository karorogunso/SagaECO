using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001799 : Event
    {
        public S11001799()
        {
            this.EventID = 11001799;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "外は怖いから、ウキワから出たくないの。$R;" +
            "$Pでもたまに出たくなるの。$R;" +
            "$Rでも出れないの。$R;" +
            "ふしぎ！$R;", "不思議な少女");
        }
    }
}