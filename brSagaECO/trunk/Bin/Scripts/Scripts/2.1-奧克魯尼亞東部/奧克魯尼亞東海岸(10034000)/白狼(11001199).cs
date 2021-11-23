using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11001199 : Event
    {
        public S11001199()
        {
            this.EventID = 11001199;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000346, 131, "這個孩子我經常梳呢$R;" +
                "$R怎麽樣?毛梳的漂亮吧?$R;");
            Say(pc, 131, "汪汪!!$R;");
        }
    }
}
