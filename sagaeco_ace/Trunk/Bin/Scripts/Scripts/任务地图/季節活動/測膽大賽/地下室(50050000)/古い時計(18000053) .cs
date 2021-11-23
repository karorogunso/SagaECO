using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50050000
{
    public class S18000053 : Event
    {
        public S18000053()
        {
            this.EventID = 18000053;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                if (obake_mask.Test(obake.開門2))
                {
                    Say(pc, 0, 0, "とにかく$R;" +
                     "全部調べてみよう……。$R;", "");
                    return;
                }
                if (obake_mask.Test(obake.木偶2))
                {
                    Say(pc, 0, 65535, "古い時計だ。$R;" +
                    "……。$R;" +
                    "木目に、何か血のようなものが$R;" +
                    "みえる。$R;" +
                    "$P気のせいか、人の顔の$R;" +
                    "ようにみえる。$R;", " ");
                    PlaySound(pc, 2554, false, 100, 50);

                    Say(pc, 0, 65535, "時計の影にかくれて$R;" +
                    "よくみえなかったが$R;" +
                    "壁に穴のようなものがある。$R;" +
                    "$Pこの奥に何が$R;" +
                    "あるのだろうか？$R;" +
                    "$Pとても小さい穴だ。$R;" +
                    "$Pのぞいてみた……。$R;", " ");
                    PlaySound(pc, 2514, false, 100, 50);

                    Say(pc, 0, 65535, "かなり奥に何かがみえる。$R;" +
                    "あれは……。$R;", " ");
                    PlaySound(pc, 2554, false, 100, 50);

                    Say(pc, 0, 65535, "薄暗い部屋がこの先にみえる。$R;", " ");
                    PlaySound(pc, 2236, false, 100, 50);

                    Say(pc, 0, 65535, "部屋の真ん中に$R;" +
                    "あの少女がいるのがみえる。$R;", " ");

                    Say(pc, 0, 65535, "ないの……。$R;" +
                    "どこにもないの……。$R;", "少女");

                    Say(pc, 0, 65535, "やっぱり$R;" +
                    "何かを探してるんだろうか？$R;" +
                    "いったい何を$R;" +
                    "探しているんだろう……。$R;", " ");
                    PlaySound(pc, 2443, false, 100, 50);

                    Say(pc, 0, 65535, "うふふ……。$R;" +
                    "あはは……。$R;", "少女");
                    PlaySound(pc, 2236, false, 100, 50);
                    PlaySound(pc, 2445, false, 100, 50);

                    Say(pc, 0, 65535, "（少女がこちらを$R;" +
                    "むいている！）$R;", "少女");

                    Say(pc, 0, 65535, "ミツケタ……。$R;" +
                    "$Pミツケタ……。$R;" +
                    "$Pミツケタ……。$R;" +
                    "アハハハハハハハ！$R;", "少女");

                    Say(pc, 0, 65535, "うわあああ！$R;" +
                    "$Pあわてて穴から$R;" +
                    "体を離した。$R;" +
                    "$P（あの少女がいるあの部屋は$R;" +
                    "ほんとうにあったのか……）$R;" +
                    "$P（……。$R;" +
                    "出口のほうにいってみよう。$R;" +
                    "前みたいに、あの少女のいる$R;" +
                    "部屋にいけるかもしれない）$R;", " ");
                    obake_mask.SetValue(obake.開門2, true);
                    return;
                }

                Say(pc, 0, 65535, "古い時計だ。$R;" +
                "……。$R;" +
                "木目に、何か血のようなものが$R;" +
                "みえる。$R;" +
                "$P気のせいか、人の顔の$R;" +
                "ようにみえる。$R;", " ");
                return;
            }
            if (obake_mask.Test(obake.木偶))
            {
                Say(pc, 0, 65535, "（古い時計だ……）$R;" +
                "$P荒い木目が不気味にみえる。$R;" +
                "中に光るものがみえる。$R;" +
                "気のせいか$R;" +
                "時計の中で$R;" +
                "がさごそと音がする$R;" +
                "気がする。$R;", " ");
                PlaySound(pc, 2554, false, 100, 50);

                Say(pc, 0, 65535, "（よくみると$R;" +
                "鍵のようにみえる）$R;" +
                "$P（手をいれてみる）$R;" +
                "$P体が動かなくなった！$R;", " ");
                PlaySound(pc, 2410, false, 60, 20);
                Wait(pc, 1980);
                PlaySound(pc, 2410, false, 80, 30);
                Wait(pc, 1980);
                PlaySound(pc, 2410, false, 100, 50);
                Wait(pc, 1980);

                Say(pc, 0, 65535, "……な、何かが。$R;" +
                "$P何者かの気配が$R;" +
                "背後から近づいてくる！$R;", " ");

                Say(pc, 0, 65535, "うふふ……。$R;", "？");
                PlaySound(pc, 2443, false, 100, 50);

                Say(pc, 0, 65535, "アハハハハハハハハハハ！$R;", "？");
                PlaySound(pc, 2554, false, 100, 50);

                Say(pc, 0, 65535, "「鍵」を手に入れた！$R;" +
                "$P（今のは何の$R;" +
                "声だったんだろう……）$R;", " ");
                obake_mask.SetValue(obake.鑰匙, true);
                return;
            }

            Say(pc, 0, 65535, "（古い時計だ……。$R;" +
            "気のせいか$R;" +
            "時計の中で$R;" +
            "がさごそと音がする$R;" +
            "気がする）$R;", " ");
         }
     }
}
