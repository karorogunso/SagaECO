using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10069001
{
    public class S13000272 : Event
    {
        public S13000272()
        {
            this.EventID = 13000272;
            this.alreadyHasQuest = "次に出る選択ウィンドウで$Rキャンセルすることもできますよ♪$R;";
            this.gotNormalQuest = "それじゃ、おねがいします！$R;" +
                "$Rクエストが無事達成できたら$R;" +
                "クエストカウンターまで$R;" +
                "報告しに来てくださいね♪$R;";
            this.gotTransportQuest = "えっとぉ、アイテムが重すぎて$R;" +
                "１回で渡せない場合は、何度かに$R;" +
                "わけて渡してください♪$R;";
            this.questCompleted = "ご苦労様でした。$R;" +
                "クエストは成功です！$R;";
            this.transport = "";
            this.questCanceled = "残念です……。$R;";
            this.questFailed = "失敗ですかぁ？$R;" +
                "$R……残念です。$R;" +
                "次は頑張ってくださいね！$R;";
            this.leastQuestPoint = 0;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10048009) >= 15)
            {
                Say(pc, 131, "ノーザンの魔法チケットが15枚！$R;" +
                "お手伝い、よくがんばりましたね！$R;" +
                "$Rお礼にこれを差し上げましょう。$R;", "監督生");
                switch (Select(pc, "どうする？", "", "いらない", "こうもりカチューシャ（紫）", "こうもりカチューシャ（黒）"))
                {
                    case 1:
                        return;
                    case 2:
                        TakeItem(pc, 10048009, 15);
                        GiveItem(pc, 50030850, 1);
                        Say(pc, 0, 131, "「こうもりカチューシャ（紫）」を$R手に入れた！$R;", " ");
                        return;
                    case 3:
                        TakeItem(pc, 10048009, 15);
                        GiveItem(pc, 50030851, 1);
                        Say(pc, 0, 131, "「こうもりカチューシャ（黒）」を$R手に入れた！$R;", " ");
                        return;
                }
                
            }
            Say(pc, 131, "お庭の掃除をお手伝い願えませんか？$R;", "監督生");
            if (Select(pc, "どうする？", "", "お断り！", "いいよ！") == 2)
            {
                HandleQuest(pc, 63);
            }
        }
    }
}