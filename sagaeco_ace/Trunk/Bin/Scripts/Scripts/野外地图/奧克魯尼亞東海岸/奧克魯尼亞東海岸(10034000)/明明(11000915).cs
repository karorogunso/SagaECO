using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000915 : Event
    {
        public S11000915()
        {
            this.EventID = 11000915;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "哞~?$R;" +
                "$R（探一探脖子…）$R;");
        }
    }
}
