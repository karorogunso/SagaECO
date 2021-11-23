using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S12002002 : Event
    {
        public S12002002()
        {
            this.EventID = 12002002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "西方熊之叢林$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "用『牛油』炒『蟹粉』$R;" +
                        "$P把『蟹粉』放到大鍋裡$R;" +
                        "放適量的水$R;" +
                        "$P然後放『骨頭』$R;" +
                        "煮兩三個小時就可以了$R;" +
                        "$P配『麵包樹果實』一起吃的話$R;" +
                        "很好吃的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
