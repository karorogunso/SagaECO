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
            Say(pc, 131, "变身活动木偶之前，掌握那个$R;" +
                "活动木偶的特征非常重要呀。$R;" +
                "$R在这里要研究的，就是研究特征，$R有兴趣就听听吧。$R;");
        }
    }
}