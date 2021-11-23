using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地:天空のオープンカフェ(20081000)NPC情報:11001017-クエスト受付　シエル- X:14 Y:8
namespace SagaScript.M20081000
{
    public class S11001017 : Event
    {
        public S11001017()
        {
            this.EventID = 11001017;
            this.notEnoughQuestPoint = "哎呀，剩余的任务点数是0啊$R;" +
                "下次再来吧$R;";
            this.leastQuestPoint = 1;
            this.alreadyHasQuest = "任务还顺利么$R需要做什么呢♪$R;";
            this.gotNormalQuest = "那么、拜托了哦！$R;" +
                "$R任务完成以后，$R;" +
                "再过来找我报告$R;" +
                "任务完成了吧♪$R;";
            this.gotTransportQuest = "是啊、道具太重了$R;" +
                "不能一次性给我的话、不管$R;" +
                "分几次给我都行哦♪$R;";
            this.questCompleted = "辛苦您了。$R;" +
                "任务成功完成！$R;";
            this.transport = "哦哦…全部收来了吗？;";
            this.questCanceled = "很遗憾……。$R;";
            this.questFailed = "失败了么？$R;" +
                "$R……很遗憾。$R;" +
                "下次再加油哦！$R;";
            this.leastQuestPoint = 0;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 302, "需要接受什么任務呢？");
            switch (Select(pc, "做什么呢？", "", "迷宮的水晶体任务！(VIP专用)", "90武器任務！", "先不忙！"))
            {
                case 1:
                    if (pc.Account.GMLevel >= 1)
                    {
                        HandleQuest(pc, 41);
                        return;
                    }
                    else
                    {
                        Say(pc, 131, "你不是vip用户$R;");
                    }
                    break;

                case 2:
                    if (CountItem(pc, 10021000) < 1)
                    {
                        Say(pc, 131, "此任务需要武器库出入证$R;");
                        break;
                    }
                    if (pc.Quest != null)
                    {
                        Say(pc, 131, "你身上有任务了哦$R;");
                        break;
                    }
                    pc.CInt["LV90_Weapon"] = 0;
                    TakeItem(pc, 10021000, 1);
                    HandleQuest(pc, 72);
                    break;

                case 3:
                    break;
            }
        }
    }
}