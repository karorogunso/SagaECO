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
            this.questFailed = "怎么花这么长时间啊！$R;" +
                "我都等累了$R;" +
                "$R这个任务失败了呀！$R;";
            this.notEnoughQuestPoint = "现在无法接受任务喔";
            this.leastQuestPoint = 1;
            this.gotNormalQuest = "道具太重，无法一次搬走时$R;" +
                "就分批搬吧$R$P;" +
                "在这里可以承接的任务种类是$R;" +
                "以『队伍』为单位的讨伐任务唷$R;" +
                "$R只要您跟队友接了同一个任务$R;" +
                "狩猎魔物的数量会一起计算，不错吧$R;";
            this.questCompleted = "呵呵…$R;" +
                "能得到您的帮忙，很开心啊$R;" +
                "$R请收下报酬吧$R;";
            this.alreadyHasQuest = "什么？做了一半要放弃？";
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

                Say(pc, 131, "我很喜欢6 这个数字喔!$R;" +
                             "$R6名，6個金币…$R;" +
                             "还有…$R;" +
                             "$P只要完美的完成任务的话，$R;" +
                             "我就会给您奖赏的喔!$R;" +
                             "$R但是现在要您狩猎666只魔物，$R;" +
                             "对您來说可能不太容易吧?$R;" +
                             "$P不过我也不是不讲道理的人啊?$R;" +
                             "要是您跟队友一起合作，$R;" +
                             " 我也会承认您完成任务哦。$R;" +
                             "$R只要您跟队友接了同一个任务，$R;" +
                             "狩猎魔物的数量也会一起计算，$R;" +
                             "怎么样，不错吧?$R;" +
                             "$P想要挑战任务吗?$R;", "六姬");
            }
            else
            {
                HandleQuest(pc, 19);
            }
        }
    }
}
