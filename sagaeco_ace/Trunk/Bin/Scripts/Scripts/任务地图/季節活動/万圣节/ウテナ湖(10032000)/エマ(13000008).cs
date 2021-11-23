using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S13000008 : Event
    {
        public S13000008()
        {
            this.EventID = 13000008;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "こんにちは。$R;" +
            "ここは、ハロウィンの$R;" +
            "始まりの地であり終わりの地。$R;" +
            "$R毎年、この時期になると湖から$R;" +
            "物凄い量のかぼちゃが流れていくわ。$R;" +
            "$Pかぼちゃは大陸の$R;" +
            "いろいろなところに辿りつくと$R;" +
            "人が寝ている間にいたずらをするの。$R;" +
            "$Pと、言うことでー……。$R;" +
            "どこにいたずらしたか探してみよう！$R;" +
            "$R『かぼちゃスタンプラリー』開催中！$R;" +
            "参加料は１００ゴールドよ！$R;" +
            "あなたも、やってみない？$R;", "エマ");
            if (Select(pc, "かぼちゃスタンプラリー！", "", "興味ない！", "１００ゴールドならやってみる！") == 2)
            {
                Say(pc, 190, "いつもと様子が違う場所に$R;" +
                "スタンプラリー会員がいるわ。$R;" +
                "$R彼らに話しかけて$R;" +
                "スタンプを押してもらってきてね。$R;" +
                "$Pスタンプの数に応じて$R;" +
                "豪華景品をプレゼントしちゃいます！$R;" +
                "$R会員は全部で６人いるから$R;" +
                "探してみてね！$R;", "エマ");

                Say(pc, 0, 131, "『スタンプ帳』を手に入れた！$R;", " ");

            }
        }
    }
}
