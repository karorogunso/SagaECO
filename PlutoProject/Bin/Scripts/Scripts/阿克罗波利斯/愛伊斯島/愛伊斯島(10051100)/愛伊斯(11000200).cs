using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051100
{
    public class S11000200 : Event
    {
        public S11000200()
        {
            this.EventID = 11000200;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10011000) >= 1)
            {
                Say(pc, 131, "从您身上，可以感觉到强大的水晶波动啊$R;" +
                    "难道您身上有$R『不可思议的水晶』吗？$R;");
                return;
            }
            Say(pc, 131, "唉…$R不知多久没有$R和活着的人见面了…$R;");
        }
    }
}