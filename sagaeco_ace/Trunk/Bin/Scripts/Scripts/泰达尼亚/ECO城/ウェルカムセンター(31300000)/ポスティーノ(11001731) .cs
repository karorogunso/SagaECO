using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001731 : Event
    {
        public S11001731()
        {
            this.EventID = 11001731;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "実はだんな様。$R;" +
            "$R奥様に、浮気がばれて$R;" +
            "ＥＣＯタウンに逃げてきたんですよ。$R;", "ポスティーノ");
        }


    }
}


