using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000247 : Event
    {
        public S13000247()
        {
            this.EventID = 13000247;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "すまねぇな、イカ焼きは売れ切れだよ$R;" +
            "買い占めていった奴がいてなぁ$R;" +
            "$R何か、くじ何たらで沢山必要とか$R;" +
            "言ってたような…$R;", "いかやき屋");

        }
    }
}