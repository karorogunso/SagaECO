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
                Say(pc, 131, "這裡是諾頓王國軍$R;" +
                    "男武僧專用本部$R;" +
                    "$R歡迎您唷$R;");
                return;
            }
            Say(pc, 131, "這裡是諾頓王國軍$R;" +
                "男武僧專用本部$R;" +
                "$R女士請回去吧$R;");
        }
    }
}