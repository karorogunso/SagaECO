using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50036000
{
    public class S11001342 : Event
    {
        public S11001342()
        {
            this.EventID = 11001342;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                CountItem(pc,10011002) > 0)
            {
                Say(pc, 0, 131, "（…よし、これで$Rネコマタたちの心はそろったかな。$R;" +
                "$R『アイシーのプリズム』を$R使ってみるか…。）$R;", " ");
                if (Select(pc, "どうする？", "", "『アイシーのプリズム』を使う", "まだ使わない") == 1)
                {
                    bool neko_01 = true;
                    bool neko_02 = true;
                    bool neko_03 = true;
                    bool neko_04 = true;
                    bool neko_05 = true;
                    if (Neko_01_amask.Test(Neko_01.桃子任務完成))
                        if (!Neko_06_cmask.Test(Neko_06.獲得桃子的碎片))
                            neko_01 = false;
                    if (Neko_02_amask.Test(Neko_02.藍任務結束))
                        if (!Neko_06_cmask.Test(Neko_06.獲得青子的碎片))
                            neko_02 = false;
                    if (Neko_03_amask.Test(Neko_03.堇子任務完成))
                        if (!Neko_06_cmask.Test(Neko_06.獲得堇子的碎片))
                            neko_03 = false;
                    if (Neko_04_amask.Test(Neko_04.任務結束))
                        if (!Neko_06_cmask.Test(Neko_06.獲得山吹的碎片))
                            neko_04 = false;
                    if (Neko_05_amask.Test(Neko_05.茜子任务结束))
                        if (!Neko_06_cmask.Test(Neko_06.獲得茜子的碎片))
                            neko_05 = false;
                    if (neko_01 && neko_02 && neko_03 && neko_04 && neko_05)
                    {
                        if (PET(pc, 10017902))
                        {
                            Wait(pc, 990);
                            ShowEffect(pc, 5058);
                            Wait(pc, 990);
                            ShowEffect(pc, 5179);
                            Wait(pc, 1980);

                            Say(pc, 0, 131, pc.Name + "、まって！$R;" +
                             "まだ緑姉ちゃんいないよ！？$R;", "ネコマタ（杏）");

                            Say(pc, 0, 131, "（緑！$R;" +
                            "$R緑って…$R;" +
                            "…どこにいるんだろう…？）$R;" +
                            "$R（……！が緑色に光る）$R;", " ");
      

　

                            Say(pc, 0, 131, "…あれ？…あっ！！$R;" +
                             "緑姉ちゃん！？ここにいたんだ！$R;", "ネコマタ（杏）");

                            Say(pc, 0, 131, "…杏？$R;" +
                                "…ここは…？$R;" +
                             "…あ、…ご主人様…。$R;", "ネコマタ（緑）");

                            Wait(pc, 990);
                            PlaySound(pc, 4012, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 5940);

                            Say(pc, 0, 131, "『背負い魔・ネコマタ（緑）』$Rの心を取り戻した！$R;", " ");

                            Say(pc, 0, 131, "（ほっ…$R;" +
                            "ここだったんだ…よかった。）$R;", " "); 
                        }
                        TakeItem(pc, 10011002, 1);
                        Say(pc, 0, 131, "『アイシーのプリズム』を取り出した！$R;" +
                        "$Pまばゆい光が身体をつつんだ！$R;", " ");
                        Wait(pc, 990);
                        ShowEffect(pc, 5263);
                        Wait(pc, 990);
                        ShowEffect(pc, 5216);
                        Wait(pc, 495);
                        ShowEffect(pc, 7, 6, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 6, 3, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 7, 4, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 5, 5, 5277);
                        Wait(pc, 990);
                        ShowEffect(pc, 5232);
                        Wait(pc, 990);

                        Say(pc, 0, 131, "ねえねえ、杏ちゃん。$R;" +
                        "$Rご主人様、元に戻るんだよね？$R;", "ネコマタ（桃）");

                        Say(pc, 0, 131, "ゴシュジンサマ？……$R;" +
                        pc.Name + "のこと？$R;" +
                        "$Rうん、$R氷のお姉ちゃんがそう言ってたよ。$R;" +
                        "$R…桃姉ちゃんどうかしたの？$R;", "ネコマタ（杏）");

                        Say(pc, 0, 131, "ううん。$R;" +
                        "$R…ご主人様、$R今は杏の身体の中にいるんだよね？$R;" +
                        "$Pだったら言葉が通じればなって、$R;" +
                        "$Rお話ができたのにって思ったの。$R;" +
                        "$Rご主人様とお話するのが…$Rずっとずっと夢だったから。$R;", "ネコマタ（桃）");

                        Say(pc, 0, 131, "？…おはなしできるよ…？$R;" +
                        "$Rねっ♪$R;" +
                        pc.Name + "$R;", "ネコマタ（杏）");

                        Say(pc, 0, 131, "！！！…えええええっ！？$R;" +
                        "$R…ご主人様…？…本当に…？$R;", "ネコマタ（桃）");

                        Say(pc, 0, 131, "う、うん。$R;", " ");

                        Say(pc, 0, 131, "！！！！！$R;", "ネコマタ（桃）");

                        if (PET(pc, 10017906))
                        Say(pc, 0, 131, "まあっ！まあっ！？$R;" +
                        "$Rあらやだ。私、はしたないことを$Rしゃべってなかったかしら…。$R;" +
                        "$P…ご主人様、菫と申します。$R;" +
                        "$Rわたくしたち姉妹が$Rいつもお世話になっています。$R本当に感謝の気持ちでいっぱいですわ。$R;" +
                        "$Pこのたびはうちの杏が$Rとんだそそうをしでかしまして、$R申し訳ございません。$R;" +
                        "$Pいえね、$Rおしもの世話こそ、わたくし$Rしっかりとしていたんですけれども、$Rなにぶんしつけに手が回る前に$R姉妹そろって死んでしまいまして。$R;" +
                        "$Rほんっとお恥ずかしい話ですわぁ。$Rおっほっほっほっ$R;", "ネコマタ（菫）");

                        if (PET(pc, 10017905))
                        Say(pc, 0, 131, "菫姉、近所のおばちゃんなってるやん。$R;" +
                        "それより、ご主人ご主人？$R;" +
                        "$R寝てるときの歯ぎしりなんやけど…$R;", "ネコマタ（山吹）");

                        if (PET(pc, 10017907))
                        Say(pc, 0, 131, "こんなときに何話してんのよ！！$R;" +
                        "$Rねぇ～ご主人様っ$R;" +
                        "そろそろさあ～天まで続く塔に……$R;", "ネコマタ（茜）");

                        if (PET(pc, 10017903))
                        Say(pc, 0, 131, "あ、あの、そ、その、$R;" +
                        "$Rぬし様？藍です。$R;" +
                        "$Pお言葉、本当におわかりなんですか？$R;" +
                        "$R…うれしい…。$R;", "ネコマタ（藍）");

                        Say(pc, 0, 131, "ご主人様！ホントに本当！？$R;" +
                        "$Rあたしね♪ずっと、ずっと…$R;", "ネコマタ（桃）");
                        ShowEffect(pc, 7, 6, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 6, 3, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 7, 4, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 5, 5, 5277);
                        Wait(pc, 495);
                        ShowEffect(pc, 5235);
                        Wait(pc, 495);
                        ShowEffect(pc, 5241);
                        Wait(pc, 1980);

                        if (PET(pc, 10017905))
                            Say(pc, 0, 131, "ああっご主人！$R;" +
                            "もう戻ってしまうんちゃうん！？$R;", "ネコマタ（山吹）");
                        else
                            Say(pc, 0, 131, "ああっ！$R;" +
                            "ご主人様が元の身体に！？$R;" +
                            "$Rもっと、もっとお話したいのに…！$R;", "ネコマタ（桃）");

                        if (PET(pc, 10017902))
                        Say(pc, 0, 131, "…時間が無い…$R;" +
                        "$R桃姉さん。$R;", "ネコマタ（緑）");

                        if (PET(pc, 10017906))
                        Say(pc, 0, 131, "…そうね。$R;" +
                        "$R桃ちゃん、あなたがお話ししなさい。$R;" +
                        "$Rあたしたちみんなの分も。$R;", "ネコマタ（菫）");
                         
                        if (PET(pc, 10017905))
                            Say(pc, 0, 131, "桃姉！まかせた！$R;" +
                            "$Rウチらの分もご主人とお話ししい。$R;", "ネコマタ（山吹）");

                        Say(pc, 0, 131, "えええっっ！？$R;" +
                        "$Rそんな…、$Rま、まかされちゃうと緊張しちゃう…！$R;" +
                        "$Rえ、えっと、その、あの、$R;" +
                        "$P……。$R;" +
                        "$P…大好き…$R;" +
                        "$Rご主人様……だいすき！だいすきっ！$Rだい……$R;" +
                        "$R……$R;", "ネコマタ（桃）");
                        ShowEffect(pc, 2, 5, 9605);
                        Wait(pc, 4950);
                        pc.CInt["Neko_06_Map_01"] = CreateMapInstance(50035000, 10062000, 130, 180);
                        Warp(pc, (uint)pc.CInt["Neko_06_Map_01"], 4, 7);

                        Say(pc, 0, 131, "……にゃあっにゃあっ…にゃあっ$R;" +
                        "$Rにゃあっ…にゃあっ！$R;", "ネコマタ");

                        Say(pc, 0, 131, "……。$R;" +
                        "$P…あ！$R;" +
                        "$Rもとの身体に戻れた…！$R;" +
                        "$P……。$R;" +
                        "$Rあれ？…杏は…杏はどこに？$R;", " ");
                        Neko_06_cmask.SetValue(Neko_06.恢復身體, true);

                    }
                    else
                        Say(pc, 0, 131, "……？？？$R;" +
                        "沒有效果$R;", " ");
                }
                return;
            }

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與杏子對話))
            {
                Say(pc, 0, 131, "（…とんでもないことに…。$R;" +
                "$Rど、どうしよう…。）$R;" +
                "$P（と、とりあえず、$Rアクロポリスシティに戻って$Rあの人に相談してみようか…。$R;" +
                "$Rネコ魂のことに詳しいし、$R何か知っているかも。）$R;", " ");
                return;
            }
            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.檢查瓶子) &&
                !Neko_06_cmask.Test(Neko_06.與杏子對話))
            {
                Say(pc, 0, 131, "……。$R;" +
                "$P………！！！$R;" +
                "$Pきゃああああっ！！$R;" +
                "$Rあっ、あっあ、あたしがいる！！？$R;", " ");

                Say(pc, 0, 131, "…ダレなの？$R;", "？？？");

                Say(pc, 0, 131, "わっ！$R;" +
                "$R頭の中から声がする！？$R;", " ");
                int sel1 = Select(pc, "返事をする？", "", "どこから話しかけてるの？", "無視する");
                while (sel1 == 2)
                {
                    Say(pc, 0, 131, "（…と、とりあえず様子を見よう…。）$R;", " ");

                    Say(pc, 0, 131, "ねえ、ダレなの？$R;" +
                    "$Rボクの中に勝手に入ってこないでよ！$R;", "？？？");

                    sel1 = Select(pc, "返事をする？", "", "どこから話しかけてるの？", "無視する");
                }

                Say(pc, 0, 131, "…ええと、$R;" +
                pc.Name + "です。$R;", " ");

                Say(pc, 0, 131, "えー、出てってよ。$R;", "？？？");

                Say(pc, 0, 131, "いや、出てってよって言われても…。$R;" +
                "$R…キミは誰？$R;", " ");

                Say(pc, 0, 131, "ボク？$R;" +
                "$Rアンズだよ。$R;", "？？？");

                Say(pc, 0, 131, "…アンズ……杏か…。$R;" +
                "$R（ネコマタたちの姉妹なのかな…？）$R;" +
                "$P（どうやらネコ魂が$R壷から飛び出してきたときに$R何かが起こって…$R;" +
                "$R逆に私の方が$Rネコ魂に憑依してしまったみたい…。）$R;" +
                "$P（こんなことってあるんだ…。）$R;", " ");

                Say(pc, 0, 131, "杏って言うんだ。$R;" +
                "$Rいちばん下の妹なのかな？$R;", " ");

                Say(pc, 0, 131, "ボクは男の子だよ！！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "ええっ！！$R;" +
                "$Rあ、あれ！？$R;" +
                "$P……。$R;" +
                "$Rうわああああああぁっ$R;" +
                "$R…ホントだ。$R;", " ");

                Say(pc, 0, 131, "いやっ、$R変なトコさわんないでよ！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "えっ、あっ、ご、ごめん…$R;" +
                "$R（…ううぅ、$Rえ、えらいことになってしまった。$R;" +
                "$Rどうしよう…）$R;", " ");
                Neko_06_cmask.SetValue(Neko_06.與杏子對話, true);
            }
        }

        public bool PET(ActorPC pc, uint PETID)
        {
            if (CountItem(pc, PETID) >= 1)
                return true;
            else if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == PETID)
                    return true;
            return false;
        }
    }
}