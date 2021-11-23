using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001819 : Event
    {
        public S11001819()
        {
            this.EventID = 11001819;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "這個人、又在說這些。$R;" +
            "$R無論如何說甚麼也改變不了吧。$R;", "沒有興趣的聽眾");

            //
            /*
            Say(pc, 0, "まーた言ってるよ、この人。$R;" +
            "$Rどうせ何にも変わりはしないさ。$R;", "興味の無い聴衆");
            */
        }
    }
}
 