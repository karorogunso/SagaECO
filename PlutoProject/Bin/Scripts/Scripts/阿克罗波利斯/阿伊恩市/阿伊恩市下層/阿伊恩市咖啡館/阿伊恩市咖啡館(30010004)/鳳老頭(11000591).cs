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
            this.alreadyHasQuest = "任务顺利吗？$R;";
            this.gotNormalQuest = "那拜托了$R;" +
                "$R等任务结束后，再来找我吧;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任务成功了$R来！收报酬吧！;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questFailed = "失败了$R;" +
                "您的能力只是这样而已?$R;";
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "嗯…$R;" +
                "$R现在没有要特别拜托的事情啊$R;" +
                "再去冒险怎么样？$R;";
            this.questTooEasy = "唔…但是对你来说$R;" +
                "说不定是太简单的事情$R;" +
                "$R那样也没关系嘛？$R;";
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
                Say(pc, 131, "拿着认证书$R;" +
                    "回到阿克罗波利斯就行了$R;" +
                    "$R辛苦了$R;");
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.轉職完成) || Job2X_02_mask.Test(Job2X_02.轉職完成))//_3A37 || _3A38)
            {
                Say(pc, 131, "什么？$R;" +
                    "想得到我的任务？$R;");
                Say(pc, 131, "我的任务$R;" +
                    "是给『队伍』做的$R;" +
                    "$R队伍中如果有人$R;" +
                    "得到一样的任务的话$R;" +
                    "可以共享击退魔物的数量$R;");

                switch (Select(pc, "做什么呢？", "", "任务", "什么也不做"))
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
                Say(pc, 131, "我的任务$R;" +
                    "是给『队伍』做的$R;" +
                    "$R队伍中如果有人$R;" +
                    "得到一样的任务的话$R;" +
                    "可以共享击退魔物的数量$R;");

                switch (Select(pc, "做什么呢？", "", "任务", "什么也不做"))
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
                    Say(pc, 131, "呵呵，拿来了?$R;" +
                        "现在可以好好的用了$R;" +
                        "$P要给您写认证书阿$R;" +
                        "等一下……$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P噢，忘了！$R;" +
                        "给您认证书的话$R;" +
                        "要给我报告$R;" +
                        "请确认一下$R;" +
                        "$P要给您任务$R;" +
                        "可以再说一遍吗?$R;");
                    Job2X_01_mask.SetValue(Job2X_01.給予黑醋, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "『黑醋』还没好嗎？$R;");
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.收集冰罐頭))//_3A35)
            {

                if (CountItem(pc, 10033904) >= 1)
                {
                    TakeItem(pc, 10033904, 1);
                    Say(pc, 131, "拿来了?$R;" +
                        "现在轻松点了$R;" +
                        "$P要给您写认证书阿$R;" +
                        "等一下……$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P噢，忘了！$R;" +
                        "给您认证书的话$R;" +
                        "要给我报告$R;" +
                        "请确认一下$R;" +
                        "$P要给您任务$R;" +
                        "再说一遍好吗?$R;");
                    Job2X_02_mask.SetValue(Job2X_02.給予冰罐頭, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "『冰罐头』还没好吗？$R;");
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.進階轉職開始))//_3A32)
            {
                Say(pc, 131, "呵呵，$R;" +
                    "想在我这里得到认证书$R;" +
                    "成为『剑圣』啊？$R;");

                switch (Select(pc, "要成为剑圣吗？", "", "剑圣是什么？", "嗯，我想成为剑圣", "不要"))
                {
                    case 1:
                        Say(pc, 131, "『剑圣』要比剑士更具攻撃性$R;" +
                            "是对武器熟能生巧的一种职业$R;" +
                            "如果转职成为『剑圣』的话$R;" +
                            "您以剑士身份所累积的$R;" +
                            "职业等级将回到「1」$R;" +
                            "您要想好了$R;");
                        break;

                    case 2:
                        Say(pc, 131, "要把『黑醋』拿来才能写呢$R;" +
                            "不然手一直抖，写不了认证书呢$R;" +
                            "$R那，拜托您了$R;");
                        Job2X_01_mask.SetValue(Job2X_01.收集黑醋, true);
                        //_3A34 = true;
                        break;

                    case 3:
                        Say(pc, 131, "是吗?$R;");
                        break;
                }
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.進階轉職開始))//_3A33)
            {
                Say(pc, 131, "呵呵！$R;" +
                    "想在我这里得到认证书$R;" +
                    "成为『圣骑士』啊？$R;");

                switch (Select(pc, "要成为圣骑士吗？", "", "圣骑士…是什么？", "嗯，我想成为圣骑士", "不要"))
                {
                    case 1:
                        Say(pc, 131, "『圣骑士』防御力要比骑士强多了。$R;" +
                            "是守护我军的卓越职业。$R;" +
                            "如果转职成为『圣骑士』的话$R;" +
                            "您以骑士身份所累积的$R;" +
                            "职业等级将回到「1」$R;" +
                            "您要想好了$R;");
                        break;

                    case 2:
                        Say(pc, 131, "那么拿来『冰罐头』好吗？$R;" +
                            "不然太热，写不了认证书呢$R;" +
                            "$R那，拜托您了$R;");
                        Job2X_02_mask.SetValue(Job2X_02.收集冰罐頭, true);
                        //_3A35 = true;
                        break;

                    case 3:
                        Say(pc, 131, "…$R;");
                        break;
                }
                return;
            }
            
            Say(pc, 131, "还是这个最好啊$R;" +
                "呵呵$R;");

        }
    }
}
