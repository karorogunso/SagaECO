using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001816 : Event
    {
        public S11001816()
        {
            this.EventID = 11001816;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "放鬆過度了、$R;" +
            "已經不想從這兒離開了呢～$R;" +
            "$P啊～…這下子…$R;" +
            "$R就這樣地過一生也可以的了～$R;", "過度放鬆的女子");

            //
            /*
            Say(pc, 0, "くつろぎ過ぎちゃって、$R;" +
            "もうここから離れたくないのよね～$R;" +
            "$Pあ～…動くの面倒臭い…$R;" +
            "$Rもう一生このままでいいわ～$R;", "くつろぎすぎた女");
            */
        }
    }
}
 