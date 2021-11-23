using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10018000
{
    public class S12002013 : Event
    {
        public S12002013()
        {
            this.EventID = 12002013;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "东边法伊斯特吊桥$R;" +
                "南边地之精灵$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "夏日推荐！$R;" +
                        "$R“矿泉水”里放砂糖$R;" +
                        "就会变成大家都喜欢的“苏打水”$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
