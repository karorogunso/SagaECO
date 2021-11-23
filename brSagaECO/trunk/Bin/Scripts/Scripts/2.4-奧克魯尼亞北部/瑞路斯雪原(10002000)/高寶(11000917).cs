using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S11000917 : Event
    {
        public S11000917()
        {
            this.EventID = 11000917;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 342, "汪！汪！$R;" +
                "$R（看樣子還想再獵到獵物…）$R;");
        }
    }
}
