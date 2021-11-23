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
            Say(pc, 131, "東邊帕斯特吊橋$R;" +
                "南邊精靈$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "夏日推薦！$R;" +
                        "$R『礦泉水』裡放砂糖$R;" +
                        "就會變成大家都喜歡的『蘇打水』$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
