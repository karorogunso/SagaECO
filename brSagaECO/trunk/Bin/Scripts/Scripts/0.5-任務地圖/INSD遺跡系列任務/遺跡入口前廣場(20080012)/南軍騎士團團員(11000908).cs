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
            Say(pc, 131, "要回去，請這邊走$R;" +
                "$P哎~有種我親身去過遺跡後$R;" +
                "回來的感覺阿$R;");
        }
    }
}