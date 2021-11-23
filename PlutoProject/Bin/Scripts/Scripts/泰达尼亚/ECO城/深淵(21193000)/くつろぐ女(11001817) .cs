using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001817 : Event
    {
        public S11001817()
        {
            this.EventID = 11001817;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "忙しい毎日のちょっとした息抜きよ。$R;" +
            "$R悪いけど、一人にしてくれる？$R;" +
            "この瞬間だけは、現実に帰りたく無いの。$R;", "くつろぐ女");
}
}

        
    }


