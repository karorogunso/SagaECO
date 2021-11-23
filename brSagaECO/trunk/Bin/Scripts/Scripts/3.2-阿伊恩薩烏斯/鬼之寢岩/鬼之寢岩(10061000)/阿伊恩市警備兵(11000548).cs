using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S11000548 : Event
    {
        public S11000548()
        {
            this.EventID = 11000548;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這條路的南邊$R;" +
                "是阿伊恩市$R;" +
                "$R要小心啊$R;");
        }
    }
}