using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000126 : Event
    {
        public S11000126()
        {
            this.EventID = 11000126;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "这一带从前因为经常刮风而有名$R;" +
                        "$R特别是早春刮的强烈南风$R;" +
                        "叫做“摩戈之风”$R;");
                    break;
                case 2:
                    Say(pc, 131, "风之精灵菲尔应该就在这附近$R;" +
                        "是我的朋友，是非常好的孩子$R;");
                    break;
            }
        }
    }
}
