using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001907 : Event
    {
        public S11001907()
        {
            this.EventID = 11001907;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "恐れ入りますが、こちらは$R;" +
            "入り口専用となっております。$R;", "イケメンボーイ");
        }
    }
}


