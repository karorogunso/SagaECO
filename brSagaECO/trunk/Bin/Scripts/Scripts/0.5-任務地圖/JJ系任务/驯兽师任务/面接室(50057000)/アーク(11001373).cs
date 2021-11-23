using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50057000
{
    public class S11001373 : Event
    {
        public S11001373()
        {
            this.EventID = 11001373;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            //int selection;
            Say(pc, 131, "私がブリーダーギルドのマスターだ。$R;" +
            "これから君たちの面接を開始する。$R;" +
            "思ったことをそのまま$R;" +
            "言ってくれて構わない。$R;" +
            "$Pいくつか質問をしようと思う$R;" +
            "その受け答えによって次の段階に$R;" +
            "進めるものを選定するぞ。$R;", "ブリーダーギルドマスター");

            Say(pc, 11001369, 131, "解りました。$R;", "イオン");

            Say(pc, 11001370, 131, "了～解です。$R;", "アーク");

            Say(pc, 131, "ふむ、では始めるぞ……$R;" +
            "$Rペットと主の関係にとって$R;" +
            "大事なものはなんだ？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "はい！絶対的な主従関係だと思います！$R;", "アーク");

            Say(pc, 11001372, 131, "信頼関係かな。$R;", "イオン");
            if (Select(pc, "大事なものはなんだ？", "", "上下関係？", "愛です！", "言うことさえ聞けば他に何もいらない") == 2)
                jjxs_mask.SetValue(jjxs.正确, true);
            第2问(pc);

        }

        void 第2问(ActorPC pc)
        {
            Say(pc, 131, "今、ペットを飼っているか？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "飼っていません！いつか飼います！$R;", "アーク");

            Say(pc, 11001372, 131, "昔、飼っていました。$R;", "イオン");
            Select(pc, "飼っているか？", "", "飼っている", "飼ってません");
            第3问(pc);
        }

        void 第3问(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);

            Say(pc, 131, "ペットがいることで$R;" +
            "何か変化があったと思う事は？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "飼ってないので解りません！$R;", "アーク");

            Say(pc, 11001372, 131, "育てるのが楽しかったです。$R;", "イオン");
            if (Select(pc, "変化があったと思う事は？", "", "戦闘が楽に", "金がかかる！", "毎日が楽しい") == 2)
                jjxs_mask.SetValue(jjxs.失败, true);
            第4问(pc);
        }

        void 第4问(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            Say(pc, 131, "何故、ペットを飼おうと思った？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "飼ってないんだって！$R;", "アーク");

            Say(pc, 11001372, 131, "一緒に冒険したくて。$R;", "イオン");
            if (Select(pc, "何故、飼おうと思った？", "", "さびしくて", "共に戦うため", "気まぐれです") != 2)
                jjxs_mask.SetValue(jjxs.正确, false);
            第5问(pc);
        }

        void 第5问(ActorPC pc)
        {
            Say(pc, 131, "まめにペットの世話はしているか？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "飼ったらしたいと思います。$R;", "アーク");

            Say(pc, 11001372, 131, "昔はそれなりにしてました。$R;", "イオン");
            Select(pc, "世話はしているか？", "", "めんどいです", "毎日しています", "それなりに");
            第6问(pc);
        }

        void 第6问(ActorPC pc)
        {
            Say(pc, 131, "ペットに求めることはなんだ？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "愛をください！$R;", "アーク");

            Say(pc, 11001372, 131, "い、癒しですかね。$R;", "イオン");
            Select(pc, "求めることはなんだ？", "", "機能性……？", "役に立つか立たないか", "そばにいるだけで良いんです");
            第7问(pc);
        }

        void 第7问(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);

            Say(pc, 131, "ブリーダーになりたい理由は何だ？$R;", "ブリーダーギルドマスター");

            Say(pc, 11001373, 131, "すぐ、なれるって聞いたんで。$R;" +
            "後、制服がかっこいいです。$R;", "アーク");

            Say(pc, 11001372, 131, "ペットをまた育てたくなったので。$R;", "イオン");
            switch (Select(pc, "理由は何だ？", "", "もっとペットと仲良く！", "誰でもなれると聞いたので……", "最強のペットを育てるため！"))
            {
                case 1:
                    jjxs_mask.SetValue(jjxs.失败, false);
                    结果(pc);
                    break;
                case 2:
                    jjxs_mask.SetValue(jjxs.失败, true);
                    结果(pc);
                    break;
                case 3:
                    jjxs_mask.SetValue(jjxs.失败, false);
                    结果(pc);
                    break;
            }
        }
        void 结果(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            //int selection;
            if (jjxs_mask.Test(jjxs.失败))
            {
                Say(pc, 131, "……なるほどな。$R;" +
                "$P……。$R;" +
                "……………ふむ。$R;", "ブリーダーギルドマスター");

                Say(pc, 131, "ちょっと君には合わないと思うよ$R;" +
                "この職業。$R;", "ブリーダーギルドマスター");

                Say(pc, 131, "特に真ん中の君。$R;" +
                "酷いよ。$R;", "ブリーダーギルドマスター");

                Say(pc, 11001373, 131, "なんですとぉ！？$R;", "アーク");
                jjxs_mask.SetValue(jjxs.失败, true);
                Warp(pc, 30130002, 10, 3);
                return;
            }
            if (jjxs_mask.Test(jjxs.正确))
            {
                Say(pc, 11001371, 131, "素質はありそうだな$R;" +
                    "次の試験に移ろうか。$R;" +
                    "$P次の試験の内容については$R;" +
                    "外の受付から聞いてくれ。$R;" +
                    "$R最終試験で待っているぞ。$R;", "ブリーダーギルドマスター");
                jjxs_mask.SetValue(jjxs.面试通过, true);
                Warp(pc, 30130001, 11, 3);
                return;
            }
            Say(pc, 11001371, 131, "……なるほどな。$R;" +
            "$P……。$R;" +
            "……………ふむ。$R;", "ブリーダーギルドマスター");

            Say(pc, 11001371, 131, "今一歩ってところだな。$R;" +
            "出直して来な。$R;", "ブリーダーギルドマスター");
            jjxs_mask.SetValue(jjxs.失败, true);
            Warp(pc, 30130002, 11, 3);
        }
    }
}