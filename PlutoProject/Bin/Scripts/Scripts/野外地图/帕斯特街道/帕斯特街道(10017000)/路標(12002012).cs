using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S12002012 : Event
    {
        public S12002012()
        {
            this.EventID = 12002012;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "东边法伊斯特吊桥$R;" +
                "南边魔物喝水的地方$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『牛奶』在室温下$R;" +
                        "$P快速地放入巨麦$R;" +
                        "加入奶油和砂糖搅拌$R;" +
                        "$P再用烤箱180度烤20分钟$R;" +
                        "$R变得送软就完成$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
