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
            Say(pc, 131, "都来到这里了，好不容易啊！$R;" +
                "$P从这里开始就比较危险了$R;" +
                "经过这里，就能看见铁火山$R;" +
                "那就是到了艾恩萨乌斯市了$R;" +
                "$R还有小心火山灰啊！$R;");
        }
    }
}