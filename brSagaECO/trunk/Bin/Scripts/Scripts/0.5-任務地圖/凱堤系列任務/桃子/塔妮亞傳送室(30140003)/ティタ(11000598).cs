using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30140003
{
    public class S11000598 : Event
    {
        public S11000598()
        {
            this.EventID = 11000598;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];

            if (mask.Test(Job2X_07.獲得神官的烙印))//_3A88)
            {
                Say(pc, 11000597, 131, "謝謝！我不會忘記您的$R;");
                return;
            }
            Say(pc, 131, "……$R;");
            Say(pc, 11000597, 131, "回答您也沒意思……$R妹妹的心成為了結晶，$R散佈世界各地。$R;");
        }
    }
}