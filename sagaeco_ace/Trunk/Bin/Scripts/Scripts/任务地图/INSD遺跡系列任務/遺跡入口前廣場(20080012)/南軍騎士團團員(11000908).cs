using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20080012
{
    public class S11000908 : Event
    {
        public S11000908()
        {
            this.EventID = 11000908;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "要回去，请这边走$R;" +
                "$P哎~有种我亲身去过遗迹后$R;" +
                "回来的感觉阿$R;");
        }
    }
}