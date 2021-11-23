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
                    Say(pc, 131, "這附近好像有很多殺人蜂巢穴$R;" +
                        "大概是因爲這樣外面到處都是殺人蜂$R;" +
                        "真難走啊$R;");
                    break;
                case 2:
                    Say(pc, 131, "往東邊走是阿高普路斯$R;" +
                        "往西邊走是不死之島$R;");
                    break;
            }
        }
    }
}
