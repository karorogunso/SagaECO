using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000224 : Event
    {
        public S11000224()
        {
            this.EventID = 11000224;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "女士请回$R;");
                Warp(pc, 10065000, 32, 6);
                return;
            }
            Say(pc, 131, "这里是诺森王国军的本部$R;");
        }
    }
}
