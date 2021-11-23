using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001861 : Event
    {
        public S11001861()
        {
            this.EventID = 11001861;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "ねぇねぇ、聞いた聞いた？$R;" +
            "$R女子脱衣室の見えざる$R;" +
            "謎の視線の噂……。$R;", "噂好きのエル");

            Say(pc, 11001862, 0, "なにそれ、なにそれ～？$R;", "口軽のクレー");

            Say(pc, 0, "噂なんだけど、女子脱衣室で$R;" +
            "着替えをしていると$R;" +
            "誰もいないはずの脱衣室から$R;" +
            "強烈な視線を感じることが$R;" +
            "あるらしいの！$R;", "噂好きのエル");

            Say(pc, 11001862, 0, "それで、それで？$R;", "口軽のクレー");

            Say(pc, 0, "それでね、噂だとその視線の主は$R;" +
            "……この温泉で溺れ死んだ$R;" +
            "インスマウスの亡霊らしいのっ！$R;", "噂好きのエル");

            Say(pc, 11001862, 0, "えぇ～、本当っ！？$R;" +
            "$R怖くて私、もう一人で$R;" +
            "脱衣室に入れないよ～。$R;", "口軽のクレー");

            Say(pc, 0, "ここで話したことは$R;" +
            "あなたと私だけの秘密よっ！$R;", "噂好きのエル");

            Say(pc, 11001862, 0, "う、うんっ！$R;", "口軽のクレー");
        }


    }
}


