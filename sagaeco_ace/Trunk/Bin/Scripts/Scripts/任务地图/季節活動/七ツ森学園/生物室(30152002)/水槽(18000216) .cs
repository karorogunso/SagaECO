using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152002
{
    public class S18000216 : Event
    {
        public S18000216()
        {
            this.EventID = 18000216;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<nanatumori> nanatumori_mask = new BitMask<nanatumori>(pc.CMask["nanatumori"]);
            //int selection;
            if (nanatumori_mask.Test(nanatumori.水槽第二次))
            {
                Say(pc, 0, 0, "いろいろな水槽が置いてある。$R;" +
                "保存溶液のにおいがする。$R;" +
                "$Pここは生物室のようだ……。$R;", "");

                Say(pc, 0, 0, "さきほどの現象は$R;" +
                "何だったのだろうか……。$R;", "");
                return;
            }
            if (nanatumori_mask.Test(nanatumori.第二次雕像))
            {
                Say(pc, 0, 0, "いろいろな水槽が置いてある。$R;" +
                "保存溶液のにおいがする。$R;" +
                "$Pここは生物室のようだ……。$R;", "");
                PlaySound(pc, 2554, false, 100, 50);

                Say(pc, 0, 0, "何か違和感を感じる……。$R;" +
                "何に対して感じるのだろう？$R;", "");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "わかった……。$R;" +
                "$P全てがこちらをみている。$R;" +
                "$P全ての生物がだ。$R;" +
                "$P……保存溶液の中の$R;" +
                "生物たち全てがこちらを$R;" +
                "見ているのだ。$R;", "");

                Say(pc, 0, 0, "一歩動く。$R;" +
                "$P全ての生物がやはり$R;" +
                "こちらを見ている……。$R;" +
                "$P思わずぞっとし、$R;" +
                "よろけて$R;" +
                "目の前の大きな水槽に$R;" +
                "手をついた。$R;", "");
                PlaySound(pc, 2508, false, 50, 50);

                Say(pc, 0, 0, "……。$R;" +
                "$Pどこからか$R;" +
                "水がはねるような音が$R;" +
                "聞こえる。$R;" +
                "$P……幻覚だ。$R;" +
                "$P……幻覚だと思いたい。$R;", "");
                PlaySound(pc, 2508, false, 100, 50);

                Say(pc, 0, 0, "しかし、その水音は$R;" +
                "大きくなっていった。$R;" +
                "$P幻覚ではない。$R;" +
                "$Pこの大きな水槽から$R;" +
                "聞こえているようだ……。$R;", "");

                Say(pc, 0, 0, "……。$R;" +
                "水槽を覗き込んでみた。$R;", "");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "フライフィッシュの$R;" +
                "生物標本の全てが$R;" +
                "こちらをみていた。$R;" +
                "$P黄色くにごった目が$R;" +
                "はっきりとこちらを$R;" +
                "向いている。$R;" +
                "$Pそして、口がぱくぱくと$R;" +
                "開き、何かを語りかけて$R;" +
                "来ているように見えた。$R;", "");

                Say(pc, 0, 0, "間違いなく幻覚ではない……。$R;" +
                "$Pこの学校ではいま$R;" +
                "何が起きているの$R;" +
                "だろうか？$R;", "");
                nanatumori_mask.SetValue(nanatumori.水槽第二次, true);
                return;
            }
            if (nanatumori_mask.Test(nanatumori.水槽第一次))
            {
                Say(pc, 0, 0, "いろいろな水槽が置いてある。$R;" +
                "保存溶液のにおいがする。$R;" +
                "$Pここは生物室のようだ……。$R;", "");

                Say(pc, 0, 0, "いろいろな薬品や、$R;" +
                "保存溶液の満ちた$R;" +
                "水槽などが置いてある……。$R;", "");
                return;
            }
            Say(pc, 0, 0, "いろいろな水槽が置いてある。$R;" +
            "保存溶液のにおいがする。$R;" +
            "$Pここは生物室のようだ。$R;", "");
            if (Select(pc, "どうする？", "", "周囲を見回す", "水槽を調べてみる") == 2)
            {
                PlaySound(pc, 2516, false, 100, 50);

                Say(pc, 0, 0, "水槽に入っている液体は$R;" +
                "白くにごっているようにみえた。$R;" +
                "$P何かが水槽に$R;" +
                "入っているようにみえる……。$R;" +
                "$P水槽に、何かホコリ$R;" +
                "のようなものがこびりついて、$R;" +
                "よく中がみえない。$R;", "");
                if (Select(pc, "どうする？", "", "ホコリを取り除いてみる") == 1)
                {
                    PlaySound(pc, 2516, false, 100, 50);

                    Say(pc, 0, 0, "指の隙間から、何かが$R;" +
                    "こちらを見ている！$R;" +
                    "$P黄色く変色した目玉が　$R;" +
                    "こちらをにらんでいるのだ！$R;" +
                    "$Pあわてて手を離す。$R;" +
                    "$P……。$R;" +
                    "$Pそれは、$R;" +
                    "保存溶液の中で漂う$R;" +
                    "魚のような生物だった$R;" +
                    "$P落ち着いてみてみると、$R;" +
                    "どうやら、$R;" +
                    "フライフィッシュの$R;" +
                    "生物標本のようだ……。$R;" +
                    "$Pその目が、ちょうど$R;" +
                    "こちらを見ているように$R;" +
                    "見えたのだ。$R;" +
                    "$P他の水槽にも、$R;" +
                    "とくに変わった点は$R;" +
                    "なさそうだ……。$R;", "");

                    Say(pc, 0, 0, "ミーシアの姿は$R;" +
                    "みられない。$R;" +
                    "$P一度クリムのところに、$R;" +
                    "話を聞きにいってみよう……。$R;", "");
                    nanatumori_mask.SetValue(nanatumori.水槽第一次, true);
                    
                }
                return;
            }
            Say(pc, 0, 0, "いろいろな薬品が$R;" +
            "棚に並んでいる。$R;" +
            "$P茶色く変色したラベル、$R;" +
            "にごった液体の入った$R;" +
            "薬品のビンなどがある……。$R;" +
            "$Pしかし、とくに変わった$R;" +
            "ところはないようだ。$R;", "");
        }
    }
}