using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10024000
{
    public class S13000007 : Event
    {
        public S13000007()
        {
            this.EventID = 13000007;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.糖))
            {
                Say(pc, 131, "ヒーヒッヒッヒッヒ！$R;" +
                "ハッピー……ハロウィン！$R;" +
                "$Rお菓子をあげてきたようだな？$R;" +
                "どの子にあげたんだね？$R;" +
                "いや、あえて聞くまい……。$R;" +
                "$Pさあ、お前にもこのお菓子をあげよう。$R;" +
                "手を差し出しなさい。$R;", "ハロウィンの使者");

                Say(pc, 131, "これは、お前の分だ。$R;" +
                "$Rもちろん、おまえが守りたい$R;" +
                "大切な人にあげてもかまわん。$R;" +
                "$Pさあ、お前のおかげて$R;" +
                "街の子どもが１人、救われた。$R;" +
                "$Rお前も、そのお菓子を口にすれば$R;" +
                "ひとまず大丈夫と言えるだろう。$R;" +
                "$Pしかし、決して気を緩めるな！$R;" +
                "悪霊はいつでもお前を狙っている。$R;" +
                "$Rハロウィンの間は、そのマスクでも$R;" +
                "かぶっていることだな……。$R;" +
                "$Pもしかしたら、街の人が$R;" +
                "お前を本物のお化けと$R;" +
                "まちがえるかもしれないぞ！$R;" +
                "$Rヒーヒッヒッヒッヒ！$R;", "ハロウィンの使者");
                return;
            }
            Say(pc, 131, "ヒーヒッヒッヒッヒ！$R;" +
            "ハロウィンは死者の祭りだ。$R;" +
            "$R怖いだろう、恐ろしいだろう！$R;" +
            "悪霊が、子どもをさらいに来るぞ！$R;" +
            "お前のたましいにイタズラするぞ！$R;" +
            "$P悪霊から身を守るのはただ１つ。$R;" +
            "『秘密のキャンディ』を食べるのだ。$R;" +
            "$R１つ、このお菓子をあげよう。$R;" +
            "気に入った子どもにあげるといい。$R;" +
            "$Pヒッヒッヒッヒ。$R;" +
            "与えられたお菓子は１つだけ……。$R;" +
            "$R救える子どもも１人だけ……。$R;", "ハロウィンの使者");
            GiveItem(pc, 10001080, 1);
            wsj_mask.SetValue(wsj.糖, true);
        }
    }
}