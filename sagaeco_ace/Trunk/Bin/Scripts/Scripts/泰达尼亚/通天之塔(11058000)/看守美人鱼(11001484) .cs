using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001484 : Event
    {
        public S11001484()
        {
            this.EventID = 11001484;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这扇门被开启....$R;" +
            "迎接我们的是光明还是黑暗呢...$R;" +
            "$R...这只是我听说过的.$R;" +
            "没办法...我们只能无法指望$R;" +
            "光明的出现了...$R;", "");
        }
    }
}




