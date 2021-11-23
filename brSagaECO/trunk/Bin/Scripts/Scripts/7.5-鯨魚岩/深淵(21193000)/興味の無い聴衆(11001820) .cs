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
            Say(pc, 0, "即便你明白那個人在說的事、$R;" +
            "可是無論如何也沒有意義。$R;" +
            "$R說得世界就像以這樣的風氣構成似的。$R;", "沒有興趣的聽眾");
            //
            /*
            Say(pc, 0, "あの人が言ってることもわかるけど、$R;" +
            "どうせ無駄だって。$R;" +
            "$R世の中はそういう風にできてるのさ。$R;", "興味の無い聴衆");
            */
        }
    }
}
 