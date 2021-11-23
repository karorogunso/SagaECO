using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000237 : Event
    {
        public S11000237()
        {
            this.EventID = 11000237;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "这里是诺森王国军$R;" +
                    "男武僧专用本部$R;" +
                    "$R欢迎您$R;");
                return;
            }
            Say(pc, 131, "这里是诺森王国军$R;" +
                "男武僧专用本部$R;" +
                "$R女士请回去吧$R;");
        }
    }
}