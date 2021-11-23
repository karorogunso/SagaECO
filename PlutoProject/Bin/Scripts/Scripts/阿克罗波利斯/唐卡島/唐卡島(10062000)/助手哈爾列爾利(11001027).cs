using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001027 : Event
    {
        public S11001027()
        {
            this.EventID = 11001027;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "这个城市的人，对我这样的$R;" +
                    "活动木偶，也很亲切哦。$R;");
                return;
            }
            Say(pc, 131, "这里是飞空庭大工厂。$R;" +
                "有什么事情，$R就去问站在那座建筑物前面的$R漂亮女士吧。$R;");
        }
    }
}