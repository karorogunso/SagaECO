using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059000
{
    public class S11001276 : Event
    {
        public S11001276()
        {
            this.EventID = 11001276;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001276, 131, "この遺跡の中は、非常に危険だ！$R;" +
                "準備は十分にするんだな。$R;");
        }
    }
}