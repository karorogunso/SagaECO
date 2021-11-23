using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30203003
{
    public class S11002162 : Event
    {
        public S11002162()
        {
            this.EventID = 11002162;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, pc.Name + "様、$R;" +
            "はじめまして、ですわね。$R;" +
            "$R…もっとも、$R;" +
            "まだその名前ではありませんけども。$R;", "？？？");

            Say(pc, 132, "私はティタ。$R;" +
            "タイタニア第三氏族の大天使ですの。$R;" +
            "$Rお会いできて光栄ですわ。$R;" +
            "$P本来、あなたのような方が$R;" +
            "ここに来ることは無いのですが…。$R;" +
            "$Pどうやら、世界は希望に向かって$R;" +
            "変わってきたようですの。$R;" +
            "$Pあなたは生まれたばかりの人形…。$R;" +
            "$Rでも、ココロを持った、特別な人形…。$R;" +
            "$Pだってここは、$R;" +
            "あなたの夢の中なんですもの。$R;" +
            "$Pもう、目覚めの時間ですわ。$R;" +
            "$P安心してください。$R;" +
            "世界はあなたの味方ですわ…。$R;", "ティタ");
            Wait(pc, 990);

            pc.CInt["Beginner_Map"] = CreateMapInstance(50080000, 10023100, 250, 132);

            Warp(pc, (uint)pc.CInt["Beginner_Map"], 26, 25);
        }
    }
}