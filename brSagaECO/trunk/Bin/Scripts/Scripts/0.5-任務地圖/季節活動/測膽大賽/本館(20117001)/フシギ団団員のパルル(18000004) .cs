using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M20117001
{
    public class S18000004 : Event
    {
        public S18000004()
        {
            this.EventID = 18000004;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 138, "ひゃーっ！$R;" +
            "$Pだだだ、誰！？$R;", "フシギ団団員のパルル");

            Say(pc, 0, 65535, "わっ！$R;" +
            "$P（こっちもびっくりした$R;" +
            "……）$R;" +
            "$Pもしかして、パルルも$R;" +
            "お化け屋敷が怖いの？$R;", " ");

            Say(pc, 131, "う、うししっ。$R;" +
            "あ、あたしは$R;" +
            "怖くないよ。$R;" +
            "ほ、ほんとだよっ！$R;", "フシギ団団員のパルル");
            ShowEffect(pc, 18000004, 4508);
            Say(pc, 131, "えっと、ここの非常口から$R;" +
            "外に出ることもできるよ。$R;" +
            "$P外に出ちゃうと$R;" +
            "ギブアップということで$R;" +
            "最初からになっちゃうけど$R;" +
            "出ちゃう？$R;", "フシギ団団員のパルル");
            Say(pc, 134, "できたらあたしも$R;" +
            "外に出たい気持ちで$R;" +
            "いっぱいだよ……。$R;", "フシギ団団員のパルル");
            if (Select(pc, "ギブアップ？", "", "ギブアップ！", "まだまだ余裕") == 1)
            {
                Warp(pc, 10023000, 177, 101);
            }
            else
            {
                Say(pc, 131, "うぅ、勇気あるねー……。$R;" +
                "がんばってねっ。$R;" +
                "あ、あたしも$R;" +
                "がんばるよ……。$R;", "フシギ団団員のパルル");
                ShowEffect(pc, 18000004, 4505);
            }

         }
     }
}
