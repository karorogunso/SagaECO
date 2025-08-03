using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000395 : Event
    {
        public S11000395()
        {
            this.EventID = 11000395;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000391, 131, "我们是把强烈颜色$R;" +
                "标榜突击的军团队员$R;");
            Say(pc, 11000391, 140, "热血，队长，红怀特$R;");
            Say(pc, 11000392, 141, "一点红的蓝布$R;");
            Say(pc, 11000393, 140, "有意思的黄耶劳$R;");
            Say(pc, 11000394, 140, "天下壮士绿杰$R;");
            Say(pc, 11000395, 140, "孤单的狼黑百特!$R;");
            Say(pc, 11000391, 131, "5个聚在一起颜色丰富的突击队员!$R;");
        }
    }
}