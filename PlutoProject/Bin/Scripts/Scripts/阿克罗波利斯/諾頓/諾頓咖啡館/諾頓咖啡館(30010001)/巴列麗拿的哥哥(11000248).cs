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

            this.questTransportDest = "给我的东西？$R;" +
                "呀！这是…$R;" +
                "$P谢谢$R;" +
                "我会珍惜的$R;";
            this.questTransportSource = "您要帮我给他呀？$R;" +
                "那么拜託了$R;";
            this.notEnoughQuestPoint = "哎呀，剩余的任务点数是0啊$R;" +
                "下次再来吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失败了？$R;" +
                "$R真是遗憾阿$R;" +
                "下次一定要成功喔$R;";
            this.alreadyHasQuest = "任务顺利吗？$R;";
            this.gotNormalQuest = "那拜托了$R;" +
                "$R等任务结束后，再来找我吧;";
            this.gotTransportQuest = "是啊，道具太重了吧$R;" +
                "所以不能一次送达的话$R;" +
                "分几次给就可以！;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任务成功了$R来！收报酬吧！;";
            this.transport = "哦哦…全部收来了吗？;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questTooEasy = "唔…但是对你来说$R;" +
                "说不定是太简单的事情$R;" +
                "$R那样也没关系嘛？$R;";
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

            switch (Select(pc, "欢迎到酒馆1号店！", "", "买东西", "卖东西", "任务服务台", "什么也不做"))
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
                        Say(pc, 111, "想承接任务吗？$R;" +
                        "$R酒馆不论您的职业或经验，$R;" +
                        "都会给您介绍各种任务$R;" +
                        "$P其中也有困难的，好好选吧$R;");
                        mask.SetValue(NDFlags.巴列麗拿的哥哥第一次对话, true);
                        return;
                    }
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    Say(pc, 111, "欢迎再来$R;");
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
