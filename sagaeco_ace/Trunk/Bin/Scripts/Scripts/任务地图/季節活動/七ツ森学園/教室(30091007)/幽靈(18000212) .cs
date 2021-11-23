using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091007
{
    public class S18000212 : Event
    {
        public S18000212()
        {
            this.EventID = 18000212;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<nanatumori> nanatumori_mask = new BitMask<nanatumori>(pc.CMask["nanatumori"]);
            //int selection;
            if (nanatumori_mask.Test(nanatumori.任務完成))
            {
                PlaySound(pc, 2521, false, 20, 40);
                Wait(pc, 990);
                PlaySound(pc, 2516, false, 30, 80);
                PlaySound(pc, 2561, false, 40, 50);

                Say(pc, 0, 0, "アリガトウ……。$R;" +
                "コレデボクタチモ……。$R;", "？");

                Say(pc, 0, 0, "感謝の気持ちが$R;" +
                "つたわってきた……。$R;", "");
                return;
            }
            if (nanatumori_mask.Test(nanatumori.幽靈第四次對話))
            {
                PlaySound(pc, 2521, false, 20, 40);
                Wait(pc, 990);
                PlaySound(pc, 2516, false, 30, 80);
                PlaySound(pc, 2561, false, 40, 50);

                Say(pc, 0, 0, "気配がどんどん$R;" +
                "弱まっている気がする。$R;" +
                "$Pしかし、かすかな声で$R;" +
                "こう聞こえた……。$R;" +
                "$P「オネガイ……。$R;" +
                "　アノ、アカクテ$R;" +
                "　コワイモノヲ　$R;" +
                "　タオシテ……」$R;" +
                "$Pいそごう。$R;", "");
                return;
            }
            if (nanatumori_mask.Test(nanatumori.幽靈第三次對話))
            {
                PlaySound(pc, 2521, false, 60, 10);

                Say(pc, 0, 0, "今まで感じていた気配が、$R;" +
                "少し弱まっている$R;" +
                "気がする……。$R;" +
                "$P気配の主が言うとおり、$R;" +
                "美術室にいってみよう。$R;", "");
                return;
            }
            
            if (nanatumori_mask.Test(nanatumori.對話5))
            {
                PlaySound(pc, 2521, false, 60, 10);
                PlaySound(pc, 2444, false, 60, 80);
                PlaySound(pc, 2525, false, 90, 10);

                Say(pc, 0, 0, "何かの気配を感じる……。$R;" +
                "$P何度かここで感じている$R;" +
                "気配の主だろうか？$R;", "");
                PlaySound(pc, 2446, false, 60, 50);

                Say(pc, 0, 0, "ソロソロキミモ$R;" +
                "アブナイ……。$R;" +
                "$Pイマノウチニ$R;" +
                "ニゲタホウガイイ……。$R;", "？");

                Say(pc, 0, 0, "危ない……？$R;" +
                "$Pどういうことだろう？$R;" +
                "$P何か危険が$R;" +
                "迫っているということ$R;" +
                "なのだろうか？$R;", "");
                PlaySound(pc, 2446, false, 60, 50);

                Say(pc, 0, 0, "アレガクル……。$R;" +
                "アレニトラワレテイルカラ、$R;" +
                "ボクタチハコノ$R;" +
                "ガッコウカラ$R;" +
                "ウゴクコトガ$R;" +
                "デキナイ……。$R;", "？");

                Say(pc, 0, 0, "アレって、何？$R;", "");

                Say(pc, 0, 0, "スゴクコワイモノ……。$R;" +
                "アカクテ、コワイ……。$R;", "？");

                Say(pc, 0, 0, "赤くて、怖い……？$R;" +
                "何のことだろう……。$R;", "");
                PlaySound(pc, 2554, false, 100, 50);

                Say(pc, 0, 0, "クリム、ミーシアハ、$R;" +
                "ミツカッタ……。$R;" +
                "$Pダカラ、ヘヤカラダサナイ。$R;" +
                "$Pアレニ、$R;" +
                "トジコメラレナイヨウニ……。$R;", "？");

                Say(pc, 0, 0, "……ん？$R;" +
                "ということは、$R;" +
                "つまり……？$R;", "");
                switch (Select(pc, "学校の七不思議は、", "", "ミーシアとクリムを守っている？", "災害そのもの？"))
                {
                    case 1:
                        Say(pc, 0, 0, "ミーシアとクリムを$R;" +
                        "守っている？$R;", "");
                        PlaySound(pc, 2446, false, 60, 50);

                        Say(pc, 0, 0, "……。$R;" +
                        "$P気のせいか、$R;" +
                        "目の前にいる$R;" +
                        "「気配」から、同意が$R;" +
                        "伝わってくる。$R;", "");
                        PlaySound(pc, 2446, false, 60, 50);

                        Say(pc, 0, 0, "ホントウニコワイノハ、$R;" +
                        "アカイモノ……。$R;" +
                        "スゴクコワイモノ……。$R;" +
                        "$Pボクタチハ、$R;" +
                        "ソレカラ、$R;" +
                        "フタリヲマモッテル……。$R;", "？");

                        Say(pc, 0, 0, "ということは、$R;" +
                        "地下室や教室で感じた$R;" +
                        "大勢の気配というのは、$R;" +
                        "その「何か」から、$R;" +
                        "クリムとミーシアを$R;" +
                        "守ろうとして$R;" +
                        "集まっていたということか。$R;" +
                        "$Rだから、$R;" +
                        "クリムとミーシアが、$R;" +
                        "その「何か」に$R;" +
                        "合わないように、$R;" +
                        "とどめていたんだ。$R;" +
                        "$Pやり方は$R;" +
                        "荒っぽいようだけど……。$R;", "");

                        Say(pc, 0, 0, "……モシカスルト、$R;" +
                        "アナタナラ……。$R;" +
                        "$Pナントカシテクレル$R;" +
                        "カモシレナイ……。$R;" +
                        "$Pオネガイ。$R;" +
                        "$Pガッコウト$R;" +
                        "ハナシヲシテ……。$R;", "？");

                        Say(pc, 0, 0, "学校と話を？$R;" +
                        "どういうことだろう？$R;", "");
                        PlaySound(pc, 2446, false, 60, 50);

                        Say(pc, 0, 0, "ビジュツシツ……。$R;" +
                        "$Pソコニイケバワカル……。$R;" +
                        "$Pボクタチモモウ、$R;" +
                        "ゲンカイ……。$R;" +
                        "$Pコノママダト……。$R;" +
                        "$Pクリム、$R;" +
                        "ミーシアモ……。$R;" +
                        "$Pサヤガ$R;" +
                        "ガンバッテイルウチニ……。$R;", "？");

                        Say(pc, 0, 0, "サヤ？$R;" +
                        "サヤっていったの？$R;" +
                        "ミーシアの妹の？$R;", "");

                        Say(pc, 0, 0, "イソイデ……。$R;" +
                        "$Pビジュツシツ……。$R;", "？");

                        Say(pc, 0, 0, "気配が弱くなっていく……。$R;" +
                        "$Pとにかく、美術室に$R;" +
                        "いってみよう。$R;", "");
                        nanatumori_mask.SetValue(nanatumori.幽靈第三次對話, true);
                        break;
                    case 2:
                        PlaySound(pc, 2554, false, 100, 50);

                        Say(pc, 0, 0, "災害そのもの？$R;" +
                        "$P……。$R;" +
                        "$Pそういうことでは$R;" +
                        "なさそうに思える……。$R;", "");
                        break;
                }
                return;
            }
            if (nanatumori_mask.Test(nanatumori.幽靈第一次對話2))
            {
                PlaySound(pc, 2521, false, 90, 40);
                Wait(pc, 990);
                PlaySound(pc, 2516, false, 50, 80);
                PlaySound(pc, 2561, false, 100, 50);
                return;
            }
            if (nanatumori_mask.Test(nanatumori.對話2))
            {
                PlaySound(pc, 2521, false, 90, 40);
                Wait(pc, 990);
                PlaySound(pc, 2516, false, 50, 80);
                PlaySound(pc, 2561, false, 100, 50);

                Say(pc, 0, 0, "何かの気配を感じる……。$R;" +
                "ずっとみられているような、$R;" +
                "そんな感じだ……。$R;", "");
                PlaySound(pc, 2446, false, 60, 50);

                Say(pc, 0, 0, "アブナイヨ……。$R;", "？");

                Say(pc, 0, 0, "（何かいま$R;" +
                "　声が聞こえたような……？）$R;" +
                "$P（……）$R;" +
                "$P（なんだか、あまり……。$R;" +
                "　嫌な感じはしないな……）$R;", "");

                Say(pc, 0, 0, "心の中でそう思ったとき、$R;" +
                "何かの気配を感じた。$R;" +
                "$P確かに嫌な感じはしない……。$R;", "");
                PlaySound(pc, 2446, false, 60, 50);

                Say(pc, 0, 0, "ボクノコエガ、$R;" +
                "キコエルノ……？$R;", "？");

                Say(pc, 0, 0, "少しは聞こえるかも？$R;", "");
                PlaySound(pc, 2446, false, 60, 50);

                Say(pc, 0, 0, "ヒサビサニヒトト$R;" +
                "ハナセタ……。$R;", "？");
                PlaySound(pc, 2446, false, 90, 50);

                Say(pc, 0, 0, "気配が強まってきた……。$R;" +
                "$P何だか、$R;" +
                "うれしそうな雰囲気を$R;" +
                "感じる。$R;", "");

                Say(pc, 0, 0, "ボクタチハココカラ$R;" +
                "デラレナイ……。$R;" +
                "$Pトジコメラレタママ……。$R;" +
                "$Pダシテホシイ。$R;", "？");

                Say(pc, 0, 0, "（出してほしい？$R;" +
                "　これは誰なんだろう……。）$R;", "");

                Say(pc, 0, 0, "……。$R;" +
                "目をこらすと、$R;" +
                "ぼんやりと声の主の$R;" +
                "姿が見える気がする……。$R;" +
                "$P何か、制服のようなものを$R;" +
                "着ているようだ。$R;" +
                "$Pこの学校の生徒だろうか？$R;", "");

                Say(pc, 0, 0, "ボクハコノガッコウノ$R;" +
                "セイト……。$R;", "？");
                PlaySound(pc, 2521, false, 60, 10);
                PlaySound(pc, 2444, false, 60, 80);
                PlaySound(pc, 2525, false, 90, 10);

                Say(pc, 0, 0, "タスケテ……。$R;" +
                "$Pボクタチハミンナ……。$R;" +
                "$Pコノガッコウニ$R;" +
                "トラワレテイル……。$R;", "？");
                PlaySound(pc, 2521, false, 30, 10);

                Say(pc, 0, 0, "気配が弱まっていく……。$R;" +
                "$P……。$R;" +
                "$Pあれは何だったのだろう……。$R;" +
                "$Pこの学校の生徒たちも、$R;" +
                "とらわれているのだろうか？$R;" +
                "$P一体だれに……？$R;" +
                "$Pこの学校には何が$R;" +
                "棲んでいるのだろうか……。$R;", "");
                nanatumori_mask.SetValue(nanatumori.幽靈第一次對話2, true);
                return;
            }
            if (nanatumori_mask.Test(nanatumori.幽靈第一次對話))
            {
                PlaySound(pc, 2223, false, 100, 50);
                return;
            }
            PlaySound(pc, 2223, false, 100, 50);
            Wait(pc, 990);
            PlaySound(pc, 2521, false, 60, 50);
            Wait(pc, 990);

            Say(pc, 0, 0, "教室のドアに手をかけた……$R;" +
            "$Pそのとき……$R;" +
            "$P何かの気配を感じた。$R;", "");
            PlaySound(pc, 2521, false, 60, 10);
            PlaySound(pc, 2444, false, 60, 80);
            PlaySound(pc, 2525, false, 90, 10);

            Say(pc, 0, 0, "この先から、$R;" +
            "何か異様な気配を感じる。$R;" +
            "$Pしかし、もう後戻りは$R;" +
            "できない……。$R;" +
            "$P覚悟を決めて先に$R;" +
            "進もう……。$R;", "");
            nanatumori_mask.SetValue(nanatumori.幽靈第一次對話, true);
        }
    }
}