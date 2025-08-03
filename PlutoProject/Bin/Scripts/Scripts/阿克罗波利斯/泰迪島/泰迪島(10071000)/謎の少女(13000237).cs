using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10071000
{
    public class S13000237 : Event
    {
        public S13000237()
        {
            this.EventID = 13000237;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
            //int selection;
            if (hanabi1_mask.Test(hanabi1.狐狸羽織入手后))
            {
                Say(pc, 131, "前と一緒で入り口は神社の裏だよ～♪$R;" +
                "$Rお楽しみくださいませ♪$R;" +
                "クスクスッ！$R;", "狐の少女");
                if (hanabi1_mask.Test(hanabi1.狐和服入手后))
                {
                    return;
                }

                狐の着物(pc);
                return;
            }
            if (hanabi1_mask.Test(hanabi1.第二次对话后))
            {
                第二次对话后(pc);
                return;
            }
            if (hanabi1_mask.Test(hanabi1.第一次对话后))
            {

                if (hanabi1_mask.Test(hanabi1.泰迪对话后))
                {
                    Say(pc, 132, "クスクスッ…どう？ 楽しかった？$R;", "謎の少女");

                    Say(pc, 132, "クスクスッ！ 騙しちゃってゴメンね～！$R;" +
                    "狐に化かされた気分はどうカナ？$R;" +
                    "$Pまぁまぁ、$R;" +
                    "そんなに不貞腐れないでよ♪$R;" +
                    "許して！ ね？$R;", "狐の少女");

                    Say(pc, 131, "あ～、でも楽しかったぁ！$R;" +
                    "これだから悪戯は$R;" +
                    "止められないんだよね～♪$R;" +
                    "$P付き合ってくれて、ありがとっ♪$R;" +
                    "またいつでも$R;" +
                    "スキンクの園に連れてってあげるよ♪$R;" +
                    "$Pあ、そうそう、$R;" +
                    "あのスキンクたちって、$R;" +
                    "面白そうなものを持ってるんだよね！$R;" +
                    "$Pもし手に入れたら譲ってね♪$R;" +
                    "クスクスッ！$R;", "狐の少女");
                     Say(pc, 131, "あれれ？ また来た？$R;" + 
                     "$Rひょっとして…$R;" + 
                     "またスキンクと遊びたいの～？$R;", "狐の少女");
                     if (Select(pc, "スキンクと遊ぶ？", "", "クセになっちゃった", "遠慮しておきます") == 2)
                     {
                         Say(pc, 132, "クスクスッ、そりゃそうだよね！$R;" +
                         "まぁ、行きたくなったら$R;" +
                         "いつでも言ってね♪$R;", "狐の少女");
                         hanabi1_mask.SetValue(hanabi1.第二次对话后, true);
                         return;
                     }
                     Say(pc, 162, "あははっ！$R;" +
                     "君、面白いね！$R;" +
                     "$P良いよ～、また行ってきなよ！$R;" +
                     "入り口は開けておいたからね！$R;", "狐の少女");
                     hanabi1_mask.SetValue(hanabi1.第二次对话后, true);
                     return;
                }
                第一次对话后(pc);
                return;
            }
            Say(pc, 131, "きゃっ！$R;" +
            "アナタすっごく私の好み！$R;" +
            "$Pね～ね～、$R;" +
            "神社の裏に行って、遊ぼうよぉ～$R;" +
            "$Rいいでしょ？ ね？$R;", "謎の少女");
            if (Select(pc, "遊ぼうよぉ～", "", "いいよ～遊ぼうよ～", "嫌だッ！") == 1)
            {
                Say(pc, 133, "やったぁ！$R;" +
                "$Pそれじゃ、$R;" +
                "裏に秘密の入り口があるから$R;" +
                "先に入って待っててね♪$R;" +
                "$Pすぐに私も行くから、先に行ってて♪$R;", "謎の少女");
                hanabi1_mask.SetValue(hanabi1.第一次对话后, true);
                return;
            }
            Say(pc, 134, "そう…$R;" +
            "寂しいな…$R;" +
            "一人ぼっちだよぉ…$R;", "謎の少女");


        }
        void 第一次对话后(ActorPC pc)
        {
            Say(pc, 132, "クスクスッ…どう？ 楽しかった？$R;", "謎の少女");
        }
        void 第二次对话后(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
           
            if (CountItem(pc, 10066100) >= 1)
            {
                Say(pc, 131, "あ！$R;" +
                "それって爆愛の塊！$R;" +
                "$P爆愛の塊って、人狐に取っては$R;" +
                "凄く欲しいものなんだよね！$R;" +
                "$Rちょっと加工してやれば、$R;" +
                "激辛香辛料になるんだ♪$R;" +
                "$Pねぇねぇ、それくれない？$R;", "狐の少女");
                if (Select(pc, "ちょうだいよ～", "", "あげるよ", "あげない！")==1)
                {
                    Say(pc, 133, "やったぁ！ ありがと♪$R;" +
                    "$Pそうだ、お礼にこれあげるね！$R;", "狐の少女");

                    Say(pc, 111, "これで狐に伝わる激辛団子が…$R;" +
                    "$Rあ、ネタばれしてるから、$R;" +
                    "君には食べさせないよ！$R;" +
                    "$P次は誰に悪戯しようかな～♪$R;" +
                    "クスクスッ！$R;", "狐の少女");
                    TakeItem(pc, 10066100, 1);
                    GiveItem(pc, 60114000, 1);
                    hanabi1_mask.SetValue(hanabi1.狐狸羽織入手后, true);
                    return;

                }
            }
            Say(pc, 131, "あれれ？ また来た？$R;" +
            "$Rひょっとして…$R;" +
            "またスキンクと遊びたいの～？$R;", "狐の少女");
           switch (Select(pc, "スキンクと遊ぶ？", "", "クセになっちゃった", "遠慮しておきます"))
           {

               case 1:
                   Say(pc, 132, "クスクスッ、そりゃそうだよね！$R;" +
                   "まぁ、行きたくなったら$R;" +
                   "いつでも言ってね♪$R;", "狐の少女");
                   break;
               case 2:
                   Say(pc, 162, "あははっ！$R;" +
                   "君、面白いね！$R;" +
                   "$P良いよ～、また行ってきなよ！$R;" +
                   "入り口は開けておいたからね！$R;", "狐の少女");
                   break;
           }
        }
        void 狐の着物(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
            
            if (CountItem(pc, 10048562) >= 1 && CountItem(pc, 10048563) >= 1 && CountItem(pc, 10048564) >= 1 && CountItem(pc, 10048565) >= 1 && CountItem(pc, 10048566) >= 1)
            {
                Say(pc, 131, "あれ？$R;" +
                "うわぁ、何かいっぱい花火もってるね！$R;" +
                "$P面白そうだなぁ～$R;" +
                "特に、この偽モノが良いね～！$R;" +
                "騙すときに色々使えそう！$R;" +
                "クスクスッ！$R;" +
                "$Pねぇねぇ、それ全部1つずつくれない？$R;", "狐の少女");
                if (Select(pc, "ちょうだいよ～", "", "あげるよ", "あげない！") == 1)
                {
                    Say(pc, 133, "やったぁ！ ありがと♪$R;" +
                    "$Pそうだ、お礼にこれあげるね！$R;", "狐の少女");

                    Say(pc, 111, "狐に伝わる、伝統衣装！$R;" +
                    "これを着れば、君も狐になれるよっ♪$R;" +
                    "$Pもちろん、嘘なんだけどね～♪$R;", "狐の少女");
                    TakeItem(pc, 10048566, 1);
                    TakeItem(pc, 10048565, 1);
                    TakeItem(pc, 10048564, 1);
                    TakeItem(pc, 10048563, 1);
                    TakeItem(pc, 10048562, 1);
                    GiveItem(pc, 60113900, 1);
                    hanabi1_mask.SetValue(hanabi1.狐和服入手后, true);
                }
            }
            else
            {
            }
        }
    }
}