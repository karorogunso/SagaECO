using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000236 : Event
    {
        public S11000236()
        {
            this.EventID = 11000236;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.MALE)
            {
                Say(pc, 131, "這裡是諾頓王國軍$R;" +
                    "女武僧專用本部$R;" +
                    "$R歡迎您唷$R;");
                return;
            }
            Say(pc, 131, "這裡是諾頓王國軍$R;" +
                "女武僧專用本部$R;" +
                "$R男士請回去吧$R;");
        }
    }
}