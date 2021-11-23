using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S11001383 : Event
    {
        public S11001383()
        {
            this.EventID = 11001383;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ガァッ！$R;", "ダークドラゴ");

        }
    }
}