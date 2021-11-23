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
            Say(pc, 131, "ドミニオン界が完全侵略されちまったら$R;" +
            "次は俺たちの世界が危ないんだ。$R;" +
            "この戦いが祖国を守る事に繋がるなら$R;" +
            "戦う理由はそれで十分だろ。$R;", "ハルナス");
        }
    }
}


        
   


