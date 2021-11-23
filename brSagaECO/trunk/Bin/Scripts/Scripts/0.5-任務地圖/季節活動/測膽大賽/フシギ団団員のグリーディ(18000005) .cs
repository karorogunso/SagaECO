using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M10023000
{
    public class S18000005 : Event
    {
        public S18000005()
        {
            this.EventID = 18000005;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 122, "うぉぉぉぉぉぉお！$R;" +
            "ただいまフシギ団が作った$R;" +
            "「お化け屋敷」を開催中ッス！$R;", "フシギ団団員のグリーディ");

            Say(pc, 122, "もしよかったら、夏を$R;" +
            "ヒンヤリ楽しめる$R;" +
            "「お化け屋敷」で、$R;" +
            "ちょっとしたスリルを$R;" +
            "体験してみないッスか？$R;", "フシギ団団員のグリーディ");
            if (Select(pc, "お化け屋敷に行く？", "", "行く", "行かない") == 1)
            {
                Say(pc, 122, "行ってらっしゃいッス！$R;" +
                "中に入ったら、$R;" +
                "フシギ団団員のクリムが$R;" +
                "いるので、$R;" +
                "奴に話しかけてほしいッス！$R;", "フシギ団団員のグリーディ");
                Warp(pc, 30091006, 6, 14);
            }

         }
     }
}
