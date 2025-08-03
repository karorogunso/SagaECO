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
            Say(pc, 131, "诺森非常冷$R;" +
                "$R请先做好准备，要不然会冻僵的呀$R;");
        }
    }
}
