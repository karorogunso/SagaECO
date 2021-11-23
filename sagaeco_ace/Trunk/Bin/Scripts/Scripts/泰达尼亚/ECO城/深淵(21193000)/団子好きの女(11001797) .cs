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
            Say(pc, 0, "団子って丸いじゃない？$R;" +
            "$Rあの曲線が芸術だと思うのよ。$R;" +
            "だから、食べる前に目で楽しむべきだわ。$R;" +
            "$P二度の満足を得れる団子…$R;" +
            "$Rああ、$R;" +
            "なんて素晴らしい食べ物なのかしら！$R;", "団子好きの女");
        }
    }
}