using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023000
{
    public class S13000000 : Event
    {
        public S13000000()
        {
            this.EventID = 13000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.魔法学校))
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.id == 60072100 ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.id == 60072101 ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.id == 60072102 ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.id == 60072103 ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.id == 60072104)
                    {
                        Say(pc, 161, "うわっ、と、飛んでる！！$R;" +
                        "あれっ、なんで飛んでるんだ？？？$R;" +
                        "$R（やべっ！$R;" +
                        "　魔法失敗しちゃったかなぁ？$R;" +
                        "$R　大導師様にばれたら……）$R;" +
                        "$P……ま、いっか。$R;" +
                        "$Rん、あ、いやいや、なんでもない。$R;" +
                        "気にすることじゃないよ。$R;" +
                        "$Rそうだ、せっかくだし$R;" +
                        "俺たちの学校に来ないか？$R;" +
                        "$P魔法のほうきでしか$R;" +
                        "行くことが出来ない$R;" +
                        "魔法使いたちが集う学びの館。$R;" +
                        "$R魔法ギルド総本山所属$R;" +
                        "ノーザン魔法学校へ！$R;" +
                        "$Pそれに、ちょうど今日は$R;" +
                        "ソーウェンのお祭りの日なんだ。$R;", "魔法ギルドのおちこぼれ少年");
                        switch (Select(pc, "どうする？", "", "行かない", "行く", "ソーウェンって？"))
                        {
                            case 1:
                                return;
                            case 2:
                                Say(pc, 131, "そうこなくっちゃ！$R;" +
                                "$Pじゃあ、飛ぶよ！$R;" +
                                "$R目を閉じて、風の声を聞いて$R;" +
                                "だんだん体が軽くなる……。$R;" +
                                "$R軽くなる……。$R;", "魔法ギルドのおちこぼれ少年");
                                ShowEffect(pc, 8066);
                                Wait(pc, 3960);
                                Warp(pc, 10069001, 141, 208);
                                ShowEffect(pc, 10824, 4539);
                                ShowEffect(pc, 11626, 4539);
                                ShowEffect(pc, 10215, 4539);
                                return;
                            case 3:

                                Say(pc, 131, "そっか、普通知らないよな。$R;" +
                                "$Rソーウェン祭ってのは$R;" +
                                "死者の霊を呼び出す$R;" +
                                "魔女の祝祭のひとつだよ。$R;" +
                                "$P今日はちょうど$R;" +
                                "一年と一年のはざまの日。$R;" +
                                "$R本来、相容れない$R;" +
                                "生者と死者の二つの世界が$R;" +
                                "交じり合うことが出来る特別な日。$R;" +
                                "$Pケルベロスが見守る死者の門より$R;" +
                                "死霊や、雪の精霊$R;" +
                                "氷の女王、死神たちを呼び戻し$R;" +
                                "大地にやすらかな死を与える……。$R;" +
                                "$Pこの街でやってる$R;" +
                                "ハロウィンとはちょっと違うぜ。$R;" +
                                "$Rもっと、もっと$R;" +
                                "本格的な魔法使いの儀式だ。$R;", "魔法ギルドのおちこぼれ少年");
                                return;
                        }
                        
                    }
                }
                Say(pc, 131, "掃除はやっぱ$R自分でしなきゃ駄目だよなぁー。$R;" +
                "$P…だけど、やっぱ面倒だから$R;" +
                "魔法を使ってこのほうきにさせようと$R;" +
                "思ったんだけど…$Rうまくいかないなあ。$R;", "魔法ギルドのおちこぼれ少年");
                return;
            }

            if (wsj_mask.Test(wsj.入手扫把))
            {
                Say(pc, 131, "ふぅ、なんだか$R;" +
                "お茶に誘ったのが嘘みたいだな。$R;" +
                "$Pこれ、掃除をサボろうと思って$R;" +
                "作った自動清掃用『魔法のほうき』$R;" +
                "$Pやっぱり、いらないから$R;" +
                "" + pc.Name + "が貰ってくれ。$R;" +
                "$Rおれは、自分で掃除することにする！$R;", "魔法ギルドのおちこぼれ少年");
                PlaySound(pc, 2040, false, 100, 50);

                Say(pc, 0, 131, "『魔法のほうきＤＸ』を手に入れた！$R;", " ");
                GiveItem(pc, 60072101, 1);
                wsj_mask.SetValue(wsj.魔法学校, true);

                Say(pc, 131, "" + pc.Name + "には$R;" +
                "本当に世話になったよ$R;" +
                "$Rほんっと、感謝するよ！$R;" +
                "$Pそれじゃあ、また会おう！$R;" +
                "お互い、がんばろうぜ！$R;", "魔法ギルドのおちこぼれ少年");
                return;
            }
            Say(pc, 112, "掃除当番さぼっちゃったよ！$R;" +
            "あー、やばいかな。$R;" +
            "叱られるかな？$R;" +
            "$Rでも今さらもどれないしなぁ！$R;", "魔法ギルドのおちこぼれ少年");
            if (Select(pc, "どうする？", "", "声をかける", "いじけ虫に用はない！") == 1)
            {
                Say(pc, 131, "なんだ、おまえ？$R;" +
                "$Rおれは世界一の大魔法使い！（予定）$R;" +
                "気安く声かけて欲しくないねっ！$R;", "魔法ギルドのおちこぼれ少年");
                if (Select(pc, "いいかえせ！", "", "僕は世界一の冒険者だ！（予定）", "私は世界一の冒険者よ！（予定）", "子どもは相手にしない") == 3)
                {
                    return;
                }
                Say(pc, 131, "……。$R;" +
                "$P……へへへっ！$R;" +
                "$Rなんか、お前面白いな！$R;" +
                "おれと友達になろうぜ！$R;" +
                "$Pおれの名前はヘンピコ。$R;" +
                "ノーザンシティの魔法ギルド総本山で$R;" +
                "修行してる魔法使いだ。$R;" +
                "$Rお前の名前は？$R;" +
                "$P……ふんふん。$R;" +
                "" + pc.Name + "っていうのか。$R;" +
                "よろしくな！$R;" +
                "$Pそーだ、どっかでお茶飲もうぜ。$R;" +
                "おいしいお店知ってるんだけど……。$R;" +
                "$Rちょっと、遠いんだよな。$R;" +
                "$Pあ、安心しろよ。$R;" +
                "おれの魔法で瞬間移動させてやるよ！$R;" +
                "$Rそ〜れっ！$R;", "魔法ギルドのおちこぼれ少年");
                ShowEffect(pc, 8066);
                Wait(pc, 990);
                Warp(pc, 50000000, 3, 3);
            }
        }
    }
}