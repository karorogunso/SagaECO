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
            Say(pc, 131, "東邊帕斯特吊橋$R;" +
                "南邊魔物喝水的地方$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『牛奶』在室溫下$R;" +
                        "$P快速地放入黃麥子$R;" +
                        "加入奶油和砂糖攪拌$R;" +
                        "$P再用烤箱180度烤20分鐘$R;" +
                        "$R變得鬆軟就完成$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
