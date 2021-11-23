using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001762 : Event
    {
        public S11001762()
        {
            this.EventID = 11001762;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ゲーマーズ$R;" +
            "ECOタウン支店ができたにょ！$R;" +
            "$Pなんだかよくわからない場所だけど$R;" +
            "でじこはここでも頑張るにょ。$R;", "デ・ジ・キャラット");
}
}

        
    }


