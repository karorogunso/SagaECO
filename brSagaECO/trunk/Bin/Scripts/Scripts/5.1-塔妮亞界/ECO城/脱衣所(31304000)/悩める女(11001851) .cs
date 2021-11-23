using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001851 : Event
    {
        public S11001851()
        {
            this.EventID = 11001851;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "お風呂上りの牛乳やコーヒー牛乳って$R;" +
            "なんであんなに美味しいんだろうっ？$R;" +
            "$Rう～ん、悩むわ。$R;", "悩める女");
        }


    }
}


