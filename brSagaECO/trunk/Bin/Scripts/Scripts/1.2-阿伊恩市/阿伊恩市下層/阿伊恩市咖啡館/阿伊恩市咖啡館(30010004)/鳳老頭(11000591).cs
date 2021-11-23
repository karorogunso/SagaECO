using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Quests;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010004
{
    public class S11000591 : Event
    {
        public S11000591()
        {
            this.EventID = 11000591;
            this.alreadyHasQuest = "任務順利嗎？$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任務結束後，再來找我吧;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任務成功了$R來！收報酬吧！;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questFailed = "失敗了$R;" +
                "您的能力只是這樣而已?$R;";
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "嗯…$R;" +
                "$R現在沒有要特別拜託的事情啊$R;" +
                "再去冒險怎麼樣？$R;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛？$R;";
        }

        /*
        public override void OnQuestUpdate(ActorPC pc, Quest quest)
        {
            if (pc.Quest.ID == 10031000 && pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
            {
                HandleQuest(pc, 23);
                return;
            }
        }
        */

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];


            if (CountItem(pc, 10020600) >= 1)
            {
                Say(pc, 131, "拿著認證書$R;" +
                    "回到阿高普路斯就行了$R;" +
                    "$R辛苦了$R;");
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.轉職完成) || Job2X_02_mask.Test(Job2X_02.轉職完成))//_3A37 || _3A38)
            {
                Say(pc, 131, "什麼？$R;" +
                    "想得到我的任務？$R;");
                Say(pc, 131, "我的任務$R;" +
                    "是給『隊伍』做的$R;" +
                    "$R隊伍中如果有人$R;" +
                    "得到一樣的任務的話$R;" +
                    "可以共享擊退魔物的數量$R;");

                switch (Select(pc, "做什麼呢？", "", "任務", "什麼也不做"))
                {
                    case 1:
                        //HandleQuest(pc, 23);
                        break;

                    case 2:
                        break;
                }
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.給予黑醋) || Job2X_02_mask.Test(Job2X_02.給予冰罐頭))//_3A36)
            {
                Say(pc, 131, "我的任務$R;" +
                    "是給『隊伍』做的$R;" +
                    "$R隊伍中如果有人$R;" +
                    "得到一樣的任務的話$R;" +
                    "可以共享擊退魔物的數量$R;");

                switch (Select(pc, "做什麼呢？", "", "任務", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 23);
                        break;

                    case 2:
                        break;
                }
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.收集黑醋))//_3A34)
            {

                if (CountItem(pc, 10033910) >= 1)
                {
                    TakeItem(pc, 10033910, 1);
                    Say(pc, 131, "呵呵，拿來了?$R;" +
                        "現在可以好好的用了$R;" +
                        "$P要給您寫認證書阿$R;" +
                        "等一下……$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P噢，忘了！$R;" +
                        "給您認證書的話$R;" +
                        "要給我報告$R;" +
                        "請確認一下$R;" +
                        "$P要給您任務$R;" +
                        "可以再說一遍嗎?$R;");
                    Job2X_01_mask.SetValue(Job2X_01.給予黑醋, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "『黑醋』還沒好嗎？$R;");
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.收集冰罐頭))//_3A35)
            {

                if (CountItem(pc, 10033904) >= 1)
                {
                    TakeItem(pc, 10033904, 1);
                    Say(pc, 131, "拿來了?$R;" +
                        "現在輕鬆點吧$R;" +
                        "$P要給您寫認證書阿$R;" +
                        "等一下……$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P噢，忘了！$R;" +
                        "給您認證書的話$R;" +
                        "要給我報告$R;" +
                        "請確認一下$R;" +
                        "$P要給您任務$R;" +
                        "再說一遍好嗎?$R;");
                    Job2X_02_mask.SetValue(Job2X_02.給予冰罐頭, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "『冰罐頭』還沒好嗎？$R;");
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.進階轉職開始))//_3A32)
            {
                Say(pc, 131, "呵呵，$R;" +
                    "想在我這裡得到認證書$R;" +
                    "成為『光戰士』啊？$R;");

                switch (Select(pc, "要成為光戰士嗎？", "", "光戰士是什麼？", "嗯，我想成為光戰士", "不要"))
                {
                    case 1:
                        Say(pc, 131, "『光戰士』要比劍士更具攻撃性$R;" +
                            "是對武器熟能生巧的一種職業唷$R;" +
                            "如果轉職成為『光戰士』的話$R;" +
                            "您以劍士身分所累積的$R;" +
                            "職業LV將回到「1」$R;" +
                            "您要想好了$R;");
                        break;

                    case 2:
                        Say(pc, 131, "要把『黑醋』拿來才能寫呢$R;" +
                            "不然手一直抖，寫不了認證書呢$R;" +
                            "$R那，拜託您了$R;");
                        Job2X_01_mask.SetValue(Job2X_01.收集黑醋, true);
                        //_3A34 = true;
                        break;

                    case 3:
                        Say(pc, 131, "是嗎?$R;");
                        break;
                }
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.進階轉職開始))//_3A33)
            {
                Say(pc, 131, "呵呵！$R;" +
                    "想在我這裡得到認證書$R;" +
                    "成為『聖騎士』啊？$R;");

                switch (Select(pc, "要成為聖騎士嗎？", "", "聖騎士…是什麼？", "嗯，我想成為聖騎士", "不要"))
                {
                    case 1:
                        Say(pc, 131, "『聖騎士』防禦力要比騎士強多了。$R;" +
                            "是守護我軍的卓越職業唷。$R;" +
                            "如果轉職成為『聖騎士』的話$R;" +
                            "您以劍士身分所累積的$R;" +
                            "職業LV將回到「1」$R;" +
                            "您要想好了$R;");
                        break;

                    case 2:
                        Say(pc, 131, "那麼拿來『冰罐頭』好嗎？$R;" +
                            "不然太熱，寫不了認證書呢$R;" +
                            "$R那，拜託您了$R;");
                        Job2X_02_mask.SetValue(Job2X_02.收集冰罐頭, true);
                        //_3A35 = true;
                        break;

                    case 3:
                        Say(pc, 131, "…$R;");
                        break;
                }
                return;
            }
            
            Say(pc, 131, "還是這個最好啊$R;" +
                "呵呵$R;");

        }
    }
}
