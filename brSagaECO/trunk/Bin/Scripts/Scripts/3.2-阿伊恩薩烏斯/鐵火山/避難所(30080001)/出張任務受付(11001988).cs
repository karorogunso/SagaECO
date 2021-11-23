using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
using SagaDB.Quests;
//所在地圖:避難所(30080001) NPC基本信息:卡魯利涅(11001988) X:4 Y:1
namespace SagaScript.M30080001
{
    public class S11001988 : Event
    {
        public S11001988()
        {
            this.EventID = 11001988;
            this.notEnoughQuestPoint = "哎呀，剩餘的任務點數是0啊$R;" +
                "下次再來吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失敗了？$R;" +
                "$R真是遺憾阿$R;" +
                "下次一定要成功喔$R;";
            this.alreadyHasQuest = "任務順利嗎？$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任務結束後，再來找我吧;";
            this.gotTransportQuest = "是阿，道具太重了吧$R;" +
                "所以不能一次傳送的話$R;" +
                "分幾次給就可以！;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任務成功了$R來！收報酬吧！;";
            this.transport = "哦哦…全部收來了嗎？;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛？$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001988, 131, "雖然我說了不喜歡冷的地方$R;" +
                "$R但也不用把我丟到這種鬼地方吧!!$R;", "咖啡館店員");

            switch (Select(pc, "那麼今天是來做甚麼呢?", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 6);
                    break;

                case 2:
                    break;
            }
        }
    }
}