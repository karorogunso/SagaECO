using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001031 : Event
    {
        public S11001031()
        {
            this.EventID = 11001031;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "變身活動木偶之前，掌握那個$R;" +
                "活動木偶的特徵非常重要呀。$R;" +
                "$R在這裡要研究的，就是研究特徵，$R有興趣就聽聽吧。$R;");
        }
    }
}