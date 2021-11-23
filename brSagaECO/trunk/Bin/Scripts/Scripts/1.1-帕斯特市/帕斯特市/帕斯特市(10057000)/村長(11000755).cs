using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000755 : Event
    {
        public S11000755()
        {
            this.EventID = 11000755;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "又没有把垃圾分類回收！$R;" +
                "$R真是!這樓的傢伙們太不像話了！$R;");
        }
    }
}