using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M30098001
{
    public class S18000064 : Event
    {
        public S18000064()
        {
            this.EventID = 18000064;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.黒い玉))
            {
                Say(pc, 0, 0, "さっきのは$R;" +
                "何だったんだ……。$R;", "");
                return;
            }
            if (obake_mask.Test(obake.第二目))
            {
                Say(pc, 0, 0, "いろいろと置いてある……。$R;" +
                "その中に$R;" +
                "スイッチのようなものがみえる。$R;", "");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "スイッチのそばに……。$R;" +
                "よくみると何か穴が開いている！$R;" +
                "$P穴の奥に何かみえる気がする。$R;" +
                "何か光っているような……。$R;", "");
                if (Select(pc, "穴の底に手をのばしてみる？", "", "のばしてみる！", "やめとく") == 1)
                {
                    Say(pc, 0, 0, "ゆっくりと手をいれてみた……。$R;" +
                    "案外深い。$R;" +
                    "テーブルの下をみても、$R;" +
                    "支柱の中を通っているような$R;" +
                    "感じがする。$R;", "");
                    PlaySound(pc, 2446, false, 100, 50);
                    PlaySound(pc, 2661, false, 100, 50);

                    Say(pc, 0, 0, "じっとりと汗がにじんできた。$R;" +
                    "$P（これは一体なんだろう？）$R;" +
                    "$P何かが……。$R;" +
                    "$P何かがこちらの腕を……。$R;" +
                    "$Pつ$R;" +
                    "$Pか$R;" +
                    "$Pん$R;" +
                    "$Pで$R;" +
                    "$Pいる！$R;", "");
                    PlaySound(pc, 2422, false, 100, 50);

                    Say(pc, 0, 0, "うわああああああ！$R;", "");
                    PlaySound(pc, 2445, false, 100, 50);

                    Say(pc, 0, 0, "あわてて振りほどき$R;" +
                    "手をひきぬいた$R;" +
                    "$Pいそいで中をのぞきこむ。$R;" +
                    "すると……。$R;" +
                    "穴の奥底で何かが光っている。$R;", "");
                    PlaySound(pc, 3261, false, 100, 50);

                    Say(pc, 0, 0, "何かががこちらを$R;" +
                    "にらんでいる。$R;" +
                    "$Pうわあああああああああ！$R;", "");
                    PlaySound(pc, 2443, false, 100, 50);

                    Say(pc, 0, 0, "アハハハハハハハ！$R;" +
                    "$Pミツケタ！$R;" +
                    "ミツケタ！！$R;" +
                    "それが……$R;" +
                    "わたしの……！$R;" +
                    "アハハハハハハハ！$R;", "少女");

                    Say(pc, 0, 0, "（もしかして……$R;" +
                    "外でひろった、$R;" +
                    "この黒い玉のことだろうか？）$R;", "");
                    obake_mask.SetValue(obake.黒い玉, true);
                }
                return;
            }
            if (obake_mask.Test(obake.鑰匙2))
            {
                Say(pc, 0, 0, "出口の扉に何か書いてある。$R;" +
                "$P「最後の出口より$R;" +
                "脱出せよ」$R;", "");
                return;
            }
            if (obake_mask.Test(obake.鑰匙))
            {
                Say(pc, 0, 0, "出口の扉に何か書いてある。$R;" +
                "$P「最後の出口より$R;" +
                "脱出せよ」$R;", "");


                Say(pc, 0, 0, "スイッチのそばに$R;" +
                "よくみると鍵穴のような$R;" +
                "ものがみえる……。$R;" +
                "$P古い時計で手に入れた$R;" +
                "鍵を試してみた。$R;", "");
                PlaySound(pc, 2101, false, 100, 50);

                Say(pc, 0, 0, "鍵のサイズはぴったりのようだ。$R;" +
                "鍵を回してみる……。$R;", "");
                PlaySound(pc, 2101, false, 100, 50);

                Say(pc, 0, 0, "どこかで扉が開く音がした……。$R;", "");
                obake_mask.SetValue(obake.鑰匙2, true);
                return;
            }
            if (obake_mask.Test(obake.開關))
            {
              Say(pc, 0, 0, "いろいろと置いてある……。$R;" + 
              "その中に$R;" + 
              "スイッチのようなものがみえる。$R;", "");
              if (Select(pc, "押してみる？", "", "押す！", "押さない！") == 1)
              {

                  PlaySound(pc, 2101, false, 100, 50);
                  Say(pc, 0, 0, "音をたてて背後のドアが$R;" +
                  "開いた！$R;", "");
                  PlaySound(pc, 2101, false, 100, 50);

                  Say(pc, 0, 0, "そのかわり$R;" +
                  "最後の出口の扉が$R;" +
                  "閉じてしまった……。$R;" +
                  "出口の扉に何か書いてある。$R;" +
                  "$P「マリオネットの部屋」を$R;" +
                  "探せ……。$R;", "");
                  obake_mask.SetValue(obake.開關, false);
                  return;
              }


            }
            Say(pc, 0, 0, "いろいろと置いてある……。$R;" +
            "その中に$R;" +
            "スイッチのようなものがみえる。$R;", "");
            if (Select(pc, "押してみる？", "", "押す！", "押さない！")==1)
            {
                PlaySound(pc, 2101, false, 100, 50);

                Say(pc, 0, 0, "音を立てて最後の出口の扉が$R;" +
                "開いた！$R;", "");
                PlaySound(pc, 2101, false, 100, 50);

                Say(pc, 0, 0, "そのかわり$R;" +
                "背後の扉が閉じて$R;" +
                "しまった……。$R;", "");
                obake_mask.SetValue(obake.開關, true);
                return;
            }


         }
     }
}
