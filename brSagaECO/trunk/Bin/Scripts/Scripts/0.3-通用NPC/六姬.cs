using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;

namespace SagaScript
{
    public abstract class 六姬 : Event
    {
        public 六姬()
        {
            this.questFailed = "怎麽花這麽長時間阿！$R;" +
                "我都等累了$R;" +
                "$R這個任務失敗了呀！$R;";
            this.notEnoughQuestPoint = "現在無法接受任務喔";
            this.leastQuestPoint = 1;
            this.gotNormalQuest = "道具太重，無法一次搬走時$R;" +
                "就分批搬吧$R$P;" +
                "在這裡可以承接的任務種類是$R;" +
                "以『隊伍』為單位的討伐任務唷$R;" +
                "$R只要您跟隊友接了同一個任務$R;" +
                "狩獵魔物的數量會一起計算，不錯吧$R;";
            this.questCompleted = "呵呵…$R;" +
                "能得到您的幫忙，很開心啊$R;" +
                "$R請收下報酬吧$R;";
            this.alreadyHasQuest = "什麽？做了一半要放棄？";
            this.questCanceled = "……";
        }

        public override void  OnQuestUpdate(ActorPC pc, Quest quest)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            World_01_mask.SetValue(World_01.已經與六姬進行第一次對話, false);
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.已經與六姬進行第一次對話))
            {
                World_01_mask.SetValue(World_01.已經與六姬進行第一次對話, true);

                Say(pc, 131, "我很喜歡6這個數字喔!$R;" +
                             "$R6名，6個金幣…$R;" +
                             "還有…$R;" +
                             "$P只要完美的完成任務的話，$R;" +
                             "我就會給您獎賞的喔!$R;" +
                             "$R但是現在要您狩獵666隻魔物，$R;" +
                             "對您來說可能不太容易吧?$R;" +
                             "$P不過我也不是不講道理的人啊?$R;" +
                             "要是您跟一起隊友合作，$R;" +
                             "我也會承認您完成任務唷。$R;" +
                             "$R只要您跟隊友接了同一個任務，$R;" +
                             "狩獵魔物的數量也會一起計算，$R;" +
                             "怎麼樣，不錯吧?$R;" +
                             "$P想要挑戰任務嗎?$R;", "六姬");
            }
            else
            {
                HandleQuest(pc, 19);
            }
        }
    }
}
