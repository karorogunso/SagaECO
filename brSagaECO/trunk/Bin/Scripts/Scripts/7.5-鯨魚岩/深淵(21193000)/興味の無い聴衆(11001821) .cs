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
            Say(pc, 0, "那個人押上了價值觀呢。$R;" +
            "$R 可是呢、說實話、俺就不會押上了。$R;" +
            "無論他多麼正確也好。$R;", "沒有興趣的聽眾");
            //
            /*
            Say(pc, 0, "あの人の価値観を押し付けられてもねぇ。$R;" +
            "$R正直、俺は付いていけないよ。$R;" +
            "いくら正論だったとしてもね。$R;", "興味の無い聴衆");
            */
        }
    }
}
 