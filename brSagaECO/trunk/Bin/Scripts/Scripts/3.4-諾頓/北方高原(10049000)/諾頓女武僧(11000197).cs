using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10049000
{
    public class S11000197 : Event
    {
        public S11000197()
        {
            this.EventID = 11000197;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "諾頓市非常冷$R;" +
                "$R“請先做好準備，要不然會凍僵的呀”$R;");
        }
    }
}
