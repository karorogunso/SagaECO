using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000256 : Event
    {
        public S13000256()
        {
            this.EventID = 13000256;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ようこそ、ノーザン魔法学校へ！$R;" +
            "ここが、俺が$R;" +
            "毎日通っている学校だ。$R;" +
            "$R新しい呪文の開発や$R;" +
            "薬草や毒の研究をしたり$R;" +
            "しているんだぜ！$R;" +
            "$P冒険者じゃないから$R;" +
            "まあ、闇の愛みたいに$R;" +
            "実際に、呪文を使って戦ったりは$R;" +
            "しないけどさっ。$R;" +
            "$R研究結果が$R;" +
            "みんなの役に立つように$R;" +
            "頑張っているんだ！$R;", "ヘンピコ");

            Say(pc, 131, "そうだっ！$R;" +
            "闇の愛をつれてきたことを$R;" +
            "先生に報告してこなくっちゃ！$R;", "ヘンピコ");

            Say(pc, 131, "ソーウェンの儀式は$R;" +
            "学校の一番上の広場でやってるから$R;" +
            "闇の愛は、そこの階段から$R;" +
            "ぐる〜っと外周を一周して$R;" +
            "城の中央の広場まで来てくれ！$R;" +
            "$Rじゃあ、先に行くよ！$R;", "ヘンピコ");
            ShowEffect(pc, 13000256, 4011);
            //消失
            NPCHide(pc, 13000256);
        }
    }
}