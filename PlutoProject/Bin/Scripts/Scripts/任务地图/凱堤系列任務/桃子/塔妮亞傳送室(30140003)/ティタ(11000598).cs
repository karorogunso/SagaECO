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
                Say(pc, 11000597, 131, "谢谢！我不会忘记您的$R;");
                return;
            }
            Say(pc, 131, "……$R;");
            Say(pc, 11000597, 131, "告诉你也没关系……$R妹妹的心成为了结晶，$R散布于世界各地。$R;");
        }
    }
}