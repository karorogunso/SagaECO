using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000636 : Event
    {
        public S11000636()
        {
            this.EventID = 11000636;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "汪!汪汪汪$R;");
            Say(pc, 11000635, 131, "这家伙是守卫法伊斯特的$R;" +
                "绿盾军狗部队的隶属小狗$R;" +
                "$R可爱吧？$R;");
        }
    }
}