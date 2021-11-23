using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130002
{
    public class S11001369 : Event
    {
        public S11001369()
        {
            this.EventID = 11001369;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "アークはなんでブリーダーに$R;" +
            "なろうと思ったんだ？$R;", "イオン");

            Say(pc, 11001370, 131, "ペットを連れている$R;" +
            "ペットに釣られて寄ってくる女性$R;" +
            "ハートフル。$R;" +
            "$Rそれ以外に何がある？$R;" +
            "$Pお前だって似たような$R;" +
            "もんなんじゃないのか？$R;", "アーク");

            Say(pc, 131, "それ、ブリーダーじゃなくても$R;" +
            "できるじゃないか。$R;", "イオン");

            Say(pc, 11001370, 131, "アホか。$R;" +
            "ブリーダーってところに$R;" +
            "意味があるんだよ。$R;" +
            "後、制服がかっこいい。$R;", "アーク");

            Say(pc, 11001368, 131, "不純な奴め。$R;", "フラム");

            Say(pc, 131, "たまに思うけどお前って$R;" +
            "すごいよな。$R;", "イオン");

            Say(pc, 11001370, 131, "ドウモネ☆！$R;", "アーク");
        }
    }
}