using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054001
{
    public class S12002061 : Event
    {
        public S12002061()
        {
            this.EventID = 12002061;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "除管理者以外，禁止出入！$R;");
        }
    }
}