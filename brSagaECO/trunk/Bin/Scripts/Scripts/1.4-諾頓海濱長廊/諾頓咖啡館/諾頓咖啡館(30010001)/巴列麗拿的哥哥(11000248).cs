using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010001
{
    public class S11000248 : Event
    {
        public S11000248()
        {
            this.EventID = 11000248;

            this.questTransportDest = "給我的東西？$R;" +
                "呀！這是…$R;" +
                "$P謝謝$R;" +
                "我會珍惜的$R;";
            this.questTransportSource = "您要幫我給他呀？$R;" +
                "那麼拜託了$R;";
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
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.对话2))
            {
                万圣节(pc);
            }
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);

            switch (Select(pc, "歡迎到咖啡館1號店！", "", "買東西", "賣東西", "任務服務台", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;
                case 2:
                    OpenShopSell(pc, 4);
                    break;
                case 3:
                    if (!mask.Test(NDFlags.巴列麗拿的哥哥第一次对话))
                    {
                        Say(pc, 111, "想承接任務嗎？$R;" +
                        "$R咖啡館不論您的職業或經驗，$R;" +
                        "都會給您介紹各種任務$R;" +
                        "$P其中也有困難的，好好選吧$R;");
                        mask.SetValue(NDFlags.巴列麗拿的哥哥第一次对话, true);
                        return;
                    }
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    Say(pc, 111, "歡迎再來$R;");
                    break;
            }
        }

        void 万圣节(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (pc.Gender > 0)
            {
                Say(pc, 111, "はい！？$R;" +
                "$R男を騙すために$R;" +
                "なにか作ってくれだって！？$R;" +
                "$P女ってこわ……。$R;" +
                "$Rいえいえ、なにも言いませんよ。$R;" +
                "なんか作ればいいんでしょ。$R;", "ヴァレリアの兄");

                Say(pc, 111, "はい、どうぞ。$R;", "ヴァレリアの兄");
                PlaySound(pc, 2040, false, 100, 50);

                Say(pc, 0, 131, "「手作り風サンド」を手に入れた！$R;", " ");
                GiveItem(pc, 10043303, 1);
                wsj_mask.SetValue(wsj.对话2, false);
            }
            else
            {
                Say(pc, 111, "『ばらの花束』だって！？$R;" +
                "$Pなんっで、俺の$R;" +
                "トップシークレットを知ってるんだ？$R;" +
                "$Pまあ、いいや。$R;" +
                "$R口説きたい人がいるんだろう？$R;" +
                "街一番の口説き上手！$R;" +
                "俺のテクニックならうまくいくよ！$R;", "ヴァレリアの兄");

                Say(pc, 111, "女は、甘い言葉と花に弱いのさ！$R;", "ヴァレリアの兄");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "『ばらの花束』を手に入れた！$R;", " ");
                GiveItem(pc, 10005581, 1);
                wsj_mask.SetValue(wsj.对话2, false);
            }

        }
    }
}
