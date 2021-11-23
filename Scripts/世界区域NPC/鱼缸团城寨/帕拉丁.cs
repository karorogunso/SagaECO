
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S80000009: Event
    {
        public S80000009()
        {
            this.EventID = 80000009;
            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任務進行的還順利嗎?$R;";

            this.gotNormalQuest = "那就拜託了。$R;" +
                                  "$R等任務完成以後，再來找我吧。$R;";

            this.gotTransportQuest = "是阿，道具太重了吧?$R;" +
                                     "$R所以不能一次傳送的話，$R;" +
                                     "分成幾次給我也可以的。$R;";

            this.questCompleted = "真是辛苦了。$R;" +
                                  "$R恭喜你，任務完成了。$R;" +
                                  "$P來! 收下報酬吧!$R;";

            this.transport = "哦哦…全部都收集好了嗎?$R;";

            this.questCanceled = "嗯…如果是你，我相信你能做到的，$R;" +
                                 "很期待呢……$R;";

            this.questFailed = "……$R;" +
                               "$P失敗了?$R;" +
                               "$R真是鬧了大事，$R;" +
                               "不知道該說什麼好啊?$R;" +
                               "$P這次實在沒辦法了，$R;" +
                               "下次一定要成功啊!$R;" +
                               "$R知道了吧?$R;";

            this.notEnoughQuestPoint = "嗯…$R;" +
                                       "$R現在沒有要特別拜託的事情啊，$R;" +
                                       "再去冒險怎麼樣?$R;";

            this.questTooHard = "這對你來說有點困難啊?$R;" +
                                "$R這樣也沒關係嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            string s = "帅哥";
            if (pc.Gender == PC_GENDER.FEMALE)
                s = "小姑娘";
            Say(pc, 0, "那位" + s + "!$R请留步!$R$R是否可以帮我点忙呢？$R我绝对不会亏待你的。", "帕拉丁");
                HandleQuest(pc, 2);
        }
    }
}