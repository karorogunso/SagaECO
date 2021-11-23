using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023000
{
    public class S18000014 : Event
    {
        public S18000014()
        {
            this.EventID = 18000014;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                if (obake_mask.Test(obake.完成2))
                {
                    Say(pc, 131, "あたいが幹部になったら$R;" +
                    "" + pc.Name + "を$R;" +
                    "特別待遇で$R;" +
                    "フシギ団に迎えてあげるから$R;" +
                    "応援してねぇん。$R;", "准幹部のミーシア");
                    return;
                }
                if (obake_mask.Test(obake.完成))
                {
                    Say(pc, 131, "何か晴れ晴れとした顔を$R;" +
                    "してるわねぇん。$R;" +
                    "幽霊らしき女の子は$R;" +
                    "どうなったのぉん？$R;", "准幹部のミーシア");

                    Say(pc, 0, 65535, "（さっきの少女とのことを$R;" +
                    "話した……）$R;", " ");

                    Say(pc, 131, "やっぱりそうだったのねぇ。$R;", "准幹部のミーシア");

                    Say(pc, 0, 65535, "あの少女は、$R;" +
                    "どこか満足したような$R;" +
                    "笑顔で、$R;" +
                    "さようならって$R;" +
                    "言ってたよ。$R;", " ");

                    Say(pc, 131, "そう。$R;" +
                    "なんだかもの悲しい話ねぇ。$R;" +
                    "$Pあら？$R;" +
                    "その手にもっているものは$R;" +
                    "何かしらぁん？$R;" +
                    "とても綺麗ねぇ。$R;", "准幹部のミーシア");
                    PlaySound(pc, 2684, false, 100, 50);

                    Say(pc, 0, 65535, "（手の中に入っていたのは$R;" +
                    "虹色のように$R;" +
                    "輝く髪飾りだった）$R;" +
                    "$P（いつのまににぎりしめて$R;" +
                    "いたんだろう？）$R;" +
                    "$P（これが彼女の言ってた$R;" +
                    "「ちょっとした贈り物」$R;" +
                    "なのだろうか）$R;" +
                    "$P（その虹色の輝きをみていると$R;" +
                    "あの少女の穏やかな瞳が$R;" +
                    "思い出された）$R;" +
                    "$P（あの子はどこにいくのだろう？$R;" +
                    "別の家という意味なのか$R;" +
                    "それとも……）$R;", " ");

                    Say(pc, 131, "あらぁ、何か楽しそうな$R;" +
                    "顔をしてるわねぇん。$R;" +
                    "$P……何かあったのぉん？$R;" +
                    "$P……まぁいいわぁん。$R;" +
                    "$Pそういうのって、$R;" +
                    "自分の心の中だけに$R;" +
                    "しまっておいたほうが$R;" +
                    "いいらしいわよぉん。$R;", "准幹部のミーシア");

                    Say(pc, 131, "本物の幽霊がいたなんて$R;" +
                    "いい宣伝になるわぁん。$R;" +
                    "まだその子がいるのか$R;" +
                    "わからないけど。$R;" +
                    "このお化け屋敷を$R;" +
                    "大成功させてやるわ！$R;" +
                    "$Pホホホ！$R;" +
                    "$Pぜったい幹部に$R;" +
                    "なってやるんだからっ。$R;", "准幹部のミーシア");

                    Say(pc, 131, "あたいが幹部になったら$R;" +
                    "" + pc.Name + "を$R;" +
                    "特別待遇で$R;" +
                    "フシギ団に迎えてあげるから$R;" +
                    "応援してねぇん。$R;", "准幹部のミーシア");
                    GiveItem(pc, 50100550, 1);
                    obake_mask.SetValue(obake.完成2, true);
                    return;
                    

                }
                return;
            }
            if (obake_mask.Test(obake.第一目))
            {
                Say(pc, 131, "もし何かあったら、$R;" +
                "何でも言ってねぇん。$R;" +
                "あたいはこのお化け屋敷を、$R;" +
                "何としても$R;" +
                "成功させたいのよぉ。$R;" +
                "変なうわさになるのも$R;" +
                "困るしねぇん。$R;", "准幹部のミーシア");

                Say(pc, 131, "妹の名前は$R;" +
                "エリス・リリィよぉん。$R;" +
                "相当、強い想いが$R;" +
                "遺されているみたいだけど$R;" +
                "ねぇん。$R;", "准幹部のミーシア");
                GiveItem(pc, 50057000, 1);

                obake_mask.SetValue(obake.第二目, true);
                return;
            }
            Say(pc, 0, 0, "……。$R;", "");

         }
     }
}
