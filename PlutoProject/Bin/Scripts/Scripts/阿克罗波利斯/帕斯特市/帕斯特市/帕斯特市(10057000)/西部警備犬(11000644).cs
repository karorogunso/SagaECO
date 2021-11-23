using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000644 : Event
    {
        public S11000644()
        {
            this.EventID = 11000644;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "汪汪！$R;");
            Say(pc, 11000645, 131, "那家伙好歹也是堂堂的军人！$R;" +
                "即使没有月薪也是！$R;" +
                "$R啊哈哈哈哈哈！$R;");
        }
    }
}