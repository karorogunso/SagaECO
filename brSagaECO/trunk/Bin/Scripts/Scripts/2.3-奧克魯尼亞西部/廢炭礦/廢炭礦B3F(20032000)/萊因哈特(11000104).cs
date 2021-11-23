using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20032000
{
    public class S11000104 : Event
    {
        public S11000104()
        {
            this.EventID = 11000104;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "……$R;");
                    Say(pc, 131, "好像在想什麽$R;");
                    break;
                case 2:
                    Say(pc, 131, "都來到這裡了，您相當的強啊$R;" +
                        "雖然看起來是有點弱…$R;");
                    break;
            }
        }
    }
}
