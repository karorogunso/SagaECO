using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001820 : Event
    {
        public S11001820()
        {
            this.EventID = 11001820;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "あの人が言ってることもわかるけど、$R;" +
            "どうせ無駄だって。$R;" +
            "$R世の中はそういう風にできてるのさ。$R;", "興味の無い聴衆");
        }
    }
}