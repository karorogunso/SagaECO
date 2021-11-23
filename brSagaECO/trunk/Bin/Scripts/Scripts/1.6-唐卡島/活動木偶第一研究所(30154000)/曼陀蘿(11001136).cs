using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30154000
{
    public class S11001136 : Event
    {
        public S11001136()
        {
            this.EventID = 11001136;
        }

        public override void OnEvent(ActorPC pc)
        {

            if (pc.Marionette != null)
            {
                Say(pc, 131, "這位計算能力好像有重大的問題呀。$R;");
                return;
            }
            Say(pc, 131, "……$R;");

        }
    }
}