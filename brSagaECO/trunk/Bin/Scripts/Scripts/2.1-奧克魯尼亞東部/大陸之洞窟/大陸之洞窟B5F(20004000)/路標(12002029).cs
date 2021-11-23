using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20004000
{
    public class S12002029 : Event
    {
        public S12002029()
        {
            this.EventID = 12002029;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "呵呵呵$R;" +
                "$R這裡面是魚的世界！$R;" +
                "是秘密…秘密…$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 0, 131, "組隊伍的時候！！！$R;" +
                        "吃這道菜就最適合不過$R;" +
                        "$P把各種喜歡的材料$R;" +
                        "放在鍋裡煮$R;" +
                        "$P『肉包子』『煮鷄蛋』$R;" +
                        "『蘋果乾』『奇怪的磨菇』等等$R;" +
                        "$P成功的話會很好吃的$R;" +
                        "失敗的話……$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}