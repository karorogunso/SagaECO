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
                    Say(pc, 131, "這裡就是『荒廢礦坑』$R;" +
                        "如果對自己實力沒有自信的話$R;" +
                        "還是早點回去的好!$R;");
                    break;
                case 2:
                    Say(pc, 131, "雖然這裡到處都是$R;" +
                        "會掉落貴重道具的魔物$R;");
                    break;
            }
        }
    }
}
