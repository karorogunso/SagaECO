using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M30154002
{
    public class S18000065 : Event
    {
        public S18000065()
        {
            this.EventID = 18000065;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                if (obake_mask.Test(obake.木偶2))
                {
                    Say(pc, 0, 0, "とにかく$R;" +
                    "全部調べてみよう……。$R;", "");
                    return;
                }
                PlaySound(pc, 2446, false, 100, 50);

                Say(pc, 0, 0, "台の上に$R;" +
                "壊れかけたマリオネットが$R;" +
                "無造作においてある……。$R;" +
                "$Pマリオネットに$R;" +
                "触れてみた……。$R;", "");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "体が動かない！$R;", "");
                PlaySound(pc, 2410, false, 60, 50);
                Wait(pc, 990);
                PlaySound(pc, 2424, false, 60, 50);

                Say(pc, 0, 0, "何かが扉の外にいる！？$R;", "");
                PlaySound(pc, 2410, false, 80, 50);
                Wait(pc, 1980);
                PlaySound(pc, 2424, false, 80, 50);

                Say(pc, 0, 0, "外から声が聞こえてくる……。$R;", "");
                PlaySound(pc, 2444, false, 50, 255);

                Say(pc, 0, 0, "うふふ……。$R;" +
                "うふふふ……。$R;" +
                "$P何か、直接$R;" +
                "頭にひびいてくるような$R;" +
                "声が聞こえてくる……。$R;", "");
                PlaySound(pc, 2446, false, 100, 50);

                Say(pc, 0, 0, "そのお人形は$R;" +
                "わたしのお人形なの。$R;" +
                "そのお人形は姉さんが$R;" +
                "買ってくれたの。$R;" +
                "$Pわたしはとてもそれを$R;" +
                "大事にしていたわ。$R;" +
                "$P毎日毎日。$R;", "少女");

                Say(pc, 0, 0, "毎日毎日毎日毎日毎日$R;" +
                "毎日毎日毎日毎日毎日$R;" +
                "毎日毎日毎日毎日毎日。$R;" +
                "$Pとっても大事にしていたの。$R;" +
                "お人形をとって$R;" +
                "壁にたたきつけたり。$R;" +
                "お人形を煮てみたり。$R;" +
                "$Pそのお人形もきっと$R;" +
                "楽しい思い出でいっぱいの$R;" +
                "はずよ。$R;", "少女");

                Say(pc, 0, 0, "よくみると$R;" +
                "人形の表面に$R;" +
                "無数の傷跡や$R;" +
                "壊れたあと$R;" +
                "修理した様子が$R;" +
                "みられた。$R;", "");
                PlaySound(pc, 2443, false, 100, 50);

                Say(pc, 0, 0, "あの日も、とても$R;" +
                "お人形を大事にしてたの。$R;" +
                "でも、わたしの姉さんが$R;" +
                "お人形を傷つけたの。$R;" +
                "$Pわたしは、姉さんを$R;" +
                "憎んだわ。$R;" +
                "$Pものすごく、ものすごく$R;" +
                "ものすごく、ものすごく$R;" +
                "ものすごく、ものすごく。$R;", "少女");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "だからそうしたの。$R;" +
                "姉さんも$R;" +
                "大事にすることにしたの。$R;", "少女");

                Say(pc, 0, 0, "もちろんわたし自身もね。$R;", "少女");
                PlaySound(pc, 2444, false, 50, 255);

                Say(pc, 0, 0, "アハハハハハハハハハハ$R;" +
                "ハハハハハハハハハハハハ！$R;" +
                "$Pうふふ……。$R;" +
                "わたしの絵をしらべて。$R;" +
                "深い、深い、どこまでも$R;" +
                "暗いぽっかりとあいた$R;" +
                "穴も調べて。$R;" +
                "ぜんぶ調べたら$R;" +
                "地下室に来るといいわ。$R;", "少女");
                obake_mask.SetValue(obake.木偶2, true);
                return;

            }
            if (obake_mask.Test(obake.木偶))
            {
                Say(pc, 0, 0, "（とにかく地下室に$R;" +
                "いってみよう……）$R;", "？");
                return;
            }
            Say(pc, 0, 0, "台の上に$R;" +
            "壊れかけたマリオネットが$R;" +
            "無造作においてある……。$R;" +
            "$Pマリオネットに$R;" +
            "触れてみた……。$R;", "");
            PlaySound(pc, 2236, false, 100, 50);

            Say(pc, 0, 0, "体が動かない！$R;", "");
            PlaySound(pc, 2410, false, 60, 50);
            Wait(pc, 990);
            PlaySound(pc, 2424, false, 60, 50);

            Say(pc, 0, 0, "何かが扉の外にいる！？$R;", "");
            PlaySound(pc, 2410, false, 80, 50);
            Wait(pc, 1980);
            PlaySound(pc, 2424, false, 80, 50);

            Say(pc, 0, 0, "外から声が聞こえてくる……。$R;", "");
            PlaySound(pc, 2444, false, 50, 255);

            Say(pc, 0, 0, "鍵を探せ……。$R;" +
            "$P鍵を……。$R;" +
            "$P暗く、深いところに$R;" +
            "隠した……。$R;" +
            "$P時を刻む箱の下に。$R;" +
            "$Pその鍵があれば、$R;" +
            "扉を開けたままに$R;" +
            "しておくことができるだろう……。$R;", "？");
            PlaySound(pc, 2234, false, 100, 50);

            Say(pc, 0, 0, "サモナクバ$R;" +
            "ココカラゼッタイニ$R;" +
            "デラレナイ。$R;", "？");

            Say(pc, 0, 0, "（暗く、深いところ？$R;" +
            "地下室か何かだろうか……）$R;", "？");
            obake_mask.SetValue(obake.木偶, true);
         }
     }
}
