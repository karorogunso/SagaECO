using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001821 : Event
    {
        public S11001821()
        {
            this.EventID = 11001821;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "あの人の価値観を押し付けられてもねぇ。$R;" +
            "$R正直、俺は付いていけないよ。$R;" +
            "いくら正論だったとしてもね。$R;", "興味の無い聴衆");
        }
    }
}