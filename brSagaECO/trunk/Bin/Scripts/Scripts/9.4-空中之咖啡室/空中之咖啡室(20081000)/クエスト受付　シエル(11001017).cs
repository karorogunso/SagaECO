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
            this.notEnoughQuestPoint = "哎呀，剩餘的任務點數是0啊$R;" +
                "下次再來吧$R;";
            this.leastQuestPoint = 1;
            this.alreadyHasQuest = "任務還順利么$R需要做什麽呢♪$R;";
            this.gotNormalQuest = "那麼、拜託了哦！$R;" +
                "$R任務完成以後，$R;" +
                "再過來找我報告$R;" +
                "任務完成了吧♪$R;";
            this.gotTransportQuest = "是啊、道具太重了$R;" +
                "不能一次性給我的話、不管$R;" +
                "分幾次給我都行哦♪$R;";
            this.questCompleted = "辛苦您了。$R;" +
                "任務成功完成！$R;";
            this.transport = "哦哦…全部收來了嗎？;";
            this.questCanceled = "很遺憾……。$R;";
            this.questFailed = "失敗了么？$R;" +
                "$R……很遺憾。$R;" +
                "下次再加油哦！$R;";
            this.leastQuestPoint = 0;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 302, "需要接受什麽任務呢？");
            switch (Select(pc, "做什麼呢？", "", "迷宮的水晶體任務！(VIP专用)", "90武器任務！", "先不忙！"))
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
                        Say(pc, 131, "此任務需要武器庫出入證$R;");
                        break;
                    }
                    if (pc.Quest != null)
                    {
                        Say(pc, 131, "你身上遺跡有任務了哦$R;");
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