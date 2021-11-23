using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S12002090 : Event
    {
        public S12002090()
        {
            this.EventID = 12002090;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "ゲートハ開イテイマス。$R;", " ");
        }
    }
}