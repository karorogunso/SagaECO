using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S11000105 : Event
    {
        public S11000105()
        {
            this.EventID = 11000105;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1 :
                    Say(pc, 131, "这附近好像有很多杀人蜂巢穴$R;" +
                        "大概是因为这样外面到处都是杀人蜂$R;" +
                        "真难走啊$R;");
                    break;
                case 2:
                    Say(pc, 131, "往东边走是阿克罗波利斯$R;" +
                        "往西边走是不死之岛$R;");
                    break;
            }
        }
    }
}
