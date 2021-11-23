using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002026 : Event
    {
        public S11002026()
        {
            this.EventID = 11002026;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "先遣突入部隊の失敗で$R;" +
            "上層部も再突入には慎重になって$R;" +
            "いるらしい。$R;" +
            "$P私も甘く見られたものだ……。$R;", "ヴェルメリオ");

        }
    }
}


        
   


