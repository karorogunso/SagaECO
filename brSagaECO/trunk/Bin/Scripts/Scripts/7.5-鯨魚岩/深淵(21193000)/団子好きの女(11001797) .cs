using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001797 : Event
    {
        public S11001797()
        {
            this.EventID = 11001797;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "糰子不是圓圓的麼？$R;" +
            "$R我認為那曲線是藝術喔$R;" +
            "所以、食用之前要用眼睛去享受。$R;" +
            "$糰子給你雙重滿足…$R;" +
            "$R啊啊、$R;" +
            "那麼美好的食物啊！$R;", "喜歡糰子的女孩");
            
            //
            /*
            Say(pc, 0, "団子って丸いじゃない？$R;" +
            "$Rあの曲線が芸術だと思うのよ。$R;" +
            "だから、食べる前に目で楽しむべきだわ。$R;" +
            "$P二度の満足を得れる団子…$R;" +
            "$Rああ、$R;" +
            "なんて素晴らしい食べ物なのかしら！$R;", "団子好きの女");
            */
        }
    }
}
 