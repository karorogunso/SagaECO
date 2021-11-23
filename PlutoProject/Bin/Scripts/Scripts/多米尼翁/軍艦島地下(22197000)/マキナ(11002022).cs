using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002022 : Event
    {
        public S11002022()
        {
            this.EventID = 11002022;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "こりゃアカン……。$R;" +
            "ラジエータの破損だけならまだしも$R;" +
            "バランサーまでイカレとる$R;" +
            "$Pいったいどないな敵と戦ったら$R;" +
            "こないなるっちゅ～ねん！$R;", "マキナ");

        }
    }
}


        
   


