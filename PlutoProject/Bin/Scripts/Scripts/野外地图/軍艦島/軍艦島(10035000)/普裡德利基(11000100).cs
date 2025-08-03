using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000100 : Event
    {
        public S11000100()
        {
            this.EventID = 11000100;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "这里就是『废炭矿』$R;" +
                        "虽然这里到处都是$R;" +
                        "会掉落贵重道具的魔物!$R;");
                    break;
                case 2:
                    Say(pc, 131, "但如果对自己实力没有自信的话$R;" +
                        "还是早点回去的好$R;");
                    break;
            }
        }
    }
}
