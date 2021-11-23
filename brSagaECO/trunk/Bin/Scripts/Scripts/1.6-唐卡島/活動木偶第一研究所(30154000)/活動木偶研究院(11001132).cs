using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30154000
{
    public class S11001132 : Event
    {
        public S11001132()
        {
            this.EventID = 11001132;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001132, 131, "這個孩子很聰明唷$R;" +
                "看看這個$R;" +
                "$R382乘293是多少?$R;");
            Say(pc, 11001136, 131, "是111926$R;");
            Say(pc, 11001132, 131, "嗯，先等等$R;" +
                "2乘3等於6，2乘9等於8…$R;" +
                "$R最後…嗯…$R;");
        }
    }
}