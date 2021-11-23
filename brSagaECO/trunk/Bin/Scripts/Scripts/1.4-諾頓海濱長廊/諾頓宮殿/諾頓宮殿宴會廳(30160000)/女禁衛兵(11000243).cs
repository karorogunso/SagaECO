using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000243 : Event
    {
        public S11000243()
        {
            this.EventID = 11000243;

            this.notEnoughQuestPoint = "女王陛下委託的事情要$R;" +
                                       "消耗任務點數『3』喔$R;" +
                                       "$R是很重要的事情$R;" +
                                       "請您真誠對待唷$R;";
            this.leastQuestPoint = 3;
            this.questFailed = "…$R;" +
                               "$R女王陛下是寬宏大量的人$R;" +
                               "一定會原諒您的$R;";
            this.alreadyHasQuest = "任務順利嗎？$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任務結束後，再來找我吧;";
            this.questCompleted = "祝賀成功$R;" +
                                  "請收下報酬吧$R;";
            this.questCanceled = "…真遺憾呀$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            
            if (mask.Test(NDFlags.职业装任务))
            {
                职业装任务(pc);
                return;
            }
            

            Say(pc, 131, "…$R;" +
                "從這裡開始，禁止出入喔$R;");

        }

        void 职业装任务(ActorPC pc)
        {
            Say(pc, 131, "從女王陛下那裡聽說了$R;" +
                "打算怎麼辦？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "任務服務台", "去寶物倉庫", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "這是地下王立魔法研究所的委託$R;" +
                        "$R需要的是$R;" +
                        "能力很強的冒險者$R;" +
                        "$R委託內容如下：$R;" +
                        "$P擊退迷你多$R;" +
                        "昨天實驗途中的迷您多逃走了，$R;" +
                        "好像去了北邊的洞窟裡。$R;" +
                        "$R按照現在繁殖速度繁殖，會很麻煩的$R;" +
                        "趕快把這件事情處理一下吧$R;" +
                        "$P…迷你多？$R;" +
                        "覺得很容易$R;" +
                        "也許不是一般的迷你多吧$R;" +
                        "$P怎麼辦呢？$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "接受委託", "不接受"))
                    {
                        case 1:
                            HandleQuest(pc, 26);
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "那麼請跟我來吧$R;");
                    Warp(pc, 20013002, 63, 60);
                    break;
            }
        }
    }
}