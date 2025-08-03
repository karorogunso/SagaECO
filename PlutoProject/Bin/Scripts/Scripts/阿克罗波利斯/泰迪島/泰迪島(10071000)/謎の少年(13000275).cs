using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10071000
{
    public class S13000275 : Event
    {
        public S13000275()
        {
            this.EventID = 13000275;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
            //int selection;
            if (hanabi1_mask.Test(hanabi1.半纏))
            {
                Say(pc, 140, "おお君か。$R;", "謎の少年");
                return;
            }
            if (hanabi1_mask.Test(hanabi1.吃了))
            {
                if (CountItem(pc, 10066100) >= 3)
                {
                    Say(pc, 140, "おおこれだ。$R;", "謎の少年");
                    
                    switch (Select(pc, "半纏", "", "赤", "青", "白", "緑", "橙"))
                    {
                        case 1:
                            TakeItem(pc, 10066100, 3);
                            GiveItem(pc, 60118101, 1);

                            break;
                        case 2:
                            TakeItem(pc, 10066100, 3);
                            GiveItem(pc, 60118102, 1);

                            break;
                        case 3:
                            TakeItem(pc, 10066100, 3);
                            GiveItem(pc, 60118100, 1);

                            break;
                        case 4:
                            TakeItem(pc, 10066100, 3);
                            GiveItem(pc, 60118103, 1);

                            break;
                        case 5:
                            TakeItem(pc, 10066100, 3);
                            GiveItem(pc, 60118104, 1);


                            //break;
                            hanabi1_mask.SetValue(hanabi1.半纏, true);
                            return;
                    }
                }
                Say(pc, 132, "スキンクの居場所は、$R;" +
                "そこにいる浴衣の女の子が$R;" +
                "何か知っているかもね～。$R;", "狐の少年");

            }
            if (hanabi1_mask.Test(hanabi1.狐和服入手后))
            {
                if (hanabi1_mask.Test(hanabi1.给吃的))
                {
                    if (CountItem(pc, 10066001) >= 1)
                    {
                        Say(pc, 133, "やった～！$R;" +
                        "ありがとう！！$R;" +
                        "$Pもぐもぐ。$R;" +
                        "$P……わ～おいしいなぁ！！$R;" +
                        "あ、そうだお礼に$R;" +
                        "この半纏をあげようかな、$R;" +
                        "どうしようかな？$R;" +
                        "$P……ん～折角だから$R;" +
                        "もう少し付き合ってもらいたいな～。$R;", "謎の少年");
                        TakeItem(pc, 10066001, 1);
                        Say(pc, 140, "実は僕……。$R;", "謎の少年");

                        Say(pc, 131, "狐なんだ！$R;" +
                        "$Rへへっ、びっくりしたかな？$R;" +
                        "$Pキミって優しいんだね！$R;" +
                        "こんな僕の我儘聞いてくれて！$R;" +
                        "$Rそんな優しいキミに再びお願いっ！！$R;" +
                        "$Pあっ、そんな嫌な顔しないで～。$R;" +
                        "$Rどうしても欲しいアイテムが$R;" +
                        "あるんだけど、$R;" +
                        "僕、あいつら苦手で……。$R;" +
                        "$Pお願い！！僕につきあってよ！$R;" +
                        "$Rスキンクが持っている爆愛の塊を$R;" +
                        "３個持ってきて！！$R;", "狐の少年");
                        hanabi1_mask.SetValue(hanabi1.吃了, true);
                        return;
                    }
                    
                    Say(pc, 131, "早く何か買ってきてよ～。$R;", "謎の少年");

                }
                Say(pc, 131, "屋台で売っている物を食べたいのに$R;" +
                "お金がないよ～！$R;" +
                "$Pねぇねぇ人助けだと思って$R;" +
                "何か食べさせてくれない？$R;", "謎の少年");
                if (Select(pc, "どうする？", "", "おごってあげる", "お前にはおごらん！")==1)
                {
                    Say(pc, 159, "ありがとう！$R;" +
                    "そこで売っているものを$R;" +
                    "１つ買ってきてね。$R;" +
                    "$R僕が一番好きな食べ物をくれたら$R;" +
                    "何かいいことあるかもよ～♪$R;" +
                    "$Pあ、何が好きかは$R;" +
                    "教えてあげないんだからねっ！$R;", "謎の少年");
                    hanabi1_mask.SetValue(hanabi1.给吃的, true);
                }
                return;
            }
            Say(pc, 131, "謎の少女……。$R;" +
            "気になるな～。$R;", "謎の少年");
        }
    }
}
