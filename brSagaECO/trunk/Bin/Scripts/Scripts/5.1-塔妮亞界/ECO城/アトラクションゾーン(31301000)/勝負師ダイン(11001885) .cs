using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001885 : Event
    {
        public S11001885()
        {
            this.EventID = 11001885;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "弱いことが分かっていても$R;" +
            "ついやっちまう……。$R;" +
            "$Rアトラクションゾーンには$R;" +
            "そういった魔が潜んでいるな……。$R;", "勝負師ダイン");
        }
    }
}


