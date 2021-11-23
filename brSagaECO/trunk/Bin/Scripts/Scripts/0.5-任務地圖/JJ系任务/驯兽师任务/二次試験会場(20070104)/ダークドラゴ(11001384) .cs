using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S11001384 : Event
    {
        public S11001384()
        {
            this.EventID = 11001384;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……。$R;", "ダークドラゴ");

            Say(pc, 0, 131, "ドラゴの顔が険しい$R;", " ");

        }
    }
}