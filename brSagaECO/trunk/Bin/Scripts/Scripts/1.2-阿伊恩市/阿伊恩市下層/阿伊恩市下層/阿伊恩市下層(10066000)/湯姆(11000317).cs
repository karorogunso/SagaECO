using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000317 : Event
    {
        public S11000317()
        {
            this.EventID = 11000317;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "多講一些好聽的故事吧$R;");
        }
    }
}