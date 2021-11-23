using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S11000549 : Event
    {
        public S11000549()
        {
            this.EventID = 11000549;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "都來到這裡了，好不容易啊！$R;" +
                "$P從這裡開始就比較危險了$R;" +
                "經過這裡，就能看見鐵火山$R;" +
                "那就是到了阿伊恩市了$R;" +
                "$R還有小心火山碳啊！$R;");
        }
    }
}