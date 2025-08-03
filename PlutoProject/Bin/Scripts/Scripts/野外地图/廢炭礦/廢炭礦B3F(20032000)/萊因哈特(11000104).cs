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
                    Say(pc, 131, "好像在想什么$R;");
                    break;
                case 2:
                    Say(pc, 131, "都来到这里了，您相当的强啊$R;" +
                        "虽然看起来是有点弱…$R;");
                    break;
            }
        }
    }
}
