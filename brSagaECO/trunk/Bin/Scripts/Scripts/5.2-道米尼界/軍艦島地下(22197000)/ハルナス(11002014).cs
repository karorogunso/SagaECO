using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002014 : Event
    {
        public S11002014()
        {
            this.EventID = 11002014;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "道米尼被完全侵略的話$R;" +
                "之後我們的世界也危在旦夕了。$R;" +
                "這場戰鬥關乎保護祖國的事的話$R;" +
                "去戰鬥的理由是同此的充分對吧。$R;", "ハルナス");
            //
            /*
            Say(pc, 131, "ドミニオン界が完全侵略されちまったら$R;" +
            "次は俺たちの世界が危ないんだ。$R;" +
            "この戦いが祖国を守る事に繋がるなら$R;" +
            "戦う理由はそれで十分だろ。$R;", "ハルナス");
            */
        }
    }
}


        
   


