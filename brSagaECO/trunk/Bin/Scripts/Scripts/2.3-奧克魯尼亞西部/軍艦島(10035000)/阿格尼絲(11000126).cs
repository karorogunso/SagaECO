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
                    Say(pc, 131, "這一區從前因爲經常颳風而有名$R;" +
                        "$R特別是早春刮的強烈南風$R;" +
                        "叫做『摩根之風』$R;");
                    break;
                case 2:
                    Say(pc, 131, "神風精靈菲爾應該就在這附近$R;" +
                        "是我的朋友，是非常好的孩子$R;");
                    break;
            }
        }
    }
}
