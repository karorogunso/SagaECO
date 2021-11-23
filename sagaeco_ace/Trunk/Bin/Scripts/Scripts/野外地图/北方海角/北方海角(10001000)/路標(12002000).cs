using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S12002000 : Event
    {
        public S12002000()
        {
            this.EventID = 12002000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "北方诺森吊桥$R;" +
                "南方斯诺普山道$R;");
        }
    }
}
