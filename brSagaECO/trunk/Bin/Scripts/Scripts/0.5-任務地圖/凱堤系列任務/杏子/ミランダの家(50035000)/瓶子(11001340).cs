using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50035000
{
    public class S11001340 : Event
    {
        public S11001340()
        {
            this.EventID = 11001340;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.恢復身體) &&
                CountItem(pc, 10017909) > 0)
            {
                Say(pc, 0, 131, "にゃお～～～ん！！$R;", " ");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);
                TakeItem(pc, 10017909, 1);
                GiveItem(pc, 10017908, 1);
                Neko_06_amask.SetValue(Neko_06.获得杏子, true);
                Neko_06_cmask.SetValue(Neko_06.恢復身體, false);
                Neko_06_cmask.SetValue(Neko_06.獲得堇子的碎片, false);
                Neko_06_cmask.SetValue(Neko_06.獲得茜子的碎片, false);
                Neko_06_cmask.SetValue(Neko_06.獲得青子的碎片, false);
                Neko_06_cmask.SetValue(Neko_06.獲得山吹的碎片, false);
                Neko_06_cmask.SetValue(Neko_06.獲得桃子的碎片, false);
                Neko_06_cmask.SetValue(Neko_06.獲知恢復的方法, false);
                Neko_06_cmask.SetValue(Neko_06.檢查瓶子, false);
                Neko_06_cmask.SetValue(Neko_06.杏子任务开始, false);
                Neko_06_cmask.SetValue(Neko_06.尋找阿伊斯, false);
                Neko_06_cmask.SetValue(Neko_06.詢問裁縫阿姨, false);
                Neko_06_cmask.SetValue(Neko_06.詢問塔尼亞代表, false);
                Neko_06_cmask.SetValue(Neko_06.与喜歡花的米嵐多对话, false);
                Neko_06_cmask.SetValue(Neko_06.與埃米爾對話, false);
                Neko_06_cmask.SetValue(Neko_06.與瑪莎對話, false);
                Neko_06_cmask.SetValue(Neko_06.與提多對話, false);
                Neko_06_cmask.SetValue(Neko_06.與杏子對話, false);

                Say(pc, 0, 131, "『あんず色の三角巾』を失った！$R;" +
                "$P『背負い魔・ネコマタ（杏）』$Rを手に入れた！$R;", " ");

                if (PET(pc, 10017906))
                Say(pc, 0, 131, "…ふう。$Rよかったわ、みんな無事にもとに戻って。$R;" +
                "$Rあら？桃ちゃん、どうしたの？$R元気が無いわよ？$R;", "ネコマタ（菫）");

                Say(pc, 0, 131, "…あたし、あたし、$R;" +
                "$R…上がっちゃって$Rご主人様に伝えられなかった…。$R;" +
                "$R…思ってたこと…みんなの気持ちを…$R;" +
                "$Pせっかくみんなが時間をくれたのに…。$R;", "ネコマタ（桃）");

                if (PET(pc, 10017902))
                {
                    Say(pc, 0, 131, "そんなことないよ。$R;", "ネコマタ（緑）");

                    if (PET(pc, 10017906))
                    Say(pc, 0, 131, "ええ、緑の言うとおりよ。$R;" +
                    "$R桃ちゃんは、私たち姉妹の気持ちを$Rちゃんとご主人様に伝えてくれたわ。$R;", "ネコマタ（菫）");
                }
                else
                {
                    if (PET(pc, 10017906))
                    Say(pc, 0, 131, "そんなことない。$R;" +
                    "$R桃ちゃんは、私たち姉妹の気持ちを$Rちゃんとご主人様に伝えてくれたわ。$R;", "ネコマタ（菫）");
                }
                if (PET(pc, 10017903))
                Say(pc, 0, 131, "そうですわ、桃姉さま。$R;", "ネコマタ（藍）");

                if (PET(pc, 10017905))
                Say(pc, 0, 131, "ばっちりやったと思う、桃姉。$R;" +
                "元気だし～な♪$R;", "ネコマタ（山吹）");

                if (PET(pc, 10017907))
                Say(pc, 0, 131, "まあね、$R;" +
                "なかなかだったと思うわ。$R;", "ネコマタ（茜）");

                Say(pc, 0, 131, "…ホントに…？$R;" +
                "$R…ありがとう。$R;", "ネコマタ（桃）");

                if (PET(pc, 10017906))
                Say(pc, 0, 131, "さてと、……杏。$R;", "ネコマタ（菫）");

                Say(pc, 0, 131, "な、なんだよう。$R;", "ネコマタ（杏）");

                if (PET(pc, 10017906))
                Say(pc, 0, 131, "ごめんなさいしなさい、杏！$Rご主人様にまで迷惑をかけて！$R;" +
                "$R女の子なんだから$Rもう少しおしとやかにならないと…$R;", "ネコマタ（菫）");

                Say(pc, 0, 131, "ボクは男の子だよ！！！$R;" +
                "$Rどうして女の子あつかいするの！！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "だって可愛いんだもん♪$R;", "ネコマタ（桃）");

                if (PET(pc, 10017906))
                Say(pc, 0, 131, "あたしたちの中で一番小さいからよ。$R;", "ネコマタ（菫）");

                if (PET(pc, 10017905))
                Say(pc, 0, 131, "別にええやん、女の子で。$R;", "ネコマタ（山吹）");

                if (PET(pc, 10017907))
                Say(pc, 0, 131, "う～ん、もっと男らしくならないと$R男の子って認められないかな。$R;", "ネコマタ（茜）");

                if (PET(pc, 10017902))
                Say(pc, 0, 131, "……おもしろいから。$R;", "ネコマタ（緑）");

                Say(pc, 0, 131, "にゃんにゃにゃあにゃんにゃん$R;" +
                "$Rにゃあにゃにゃあにゃあ！$R;", " ");

                Say(pc, 0, 131, "…また一段と騒がしくなった…$R;" +
                "$R…杏か～、まあいいか。$R;", " ");
                return;
            }

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                !Neko_06_cmask.Test(Neko_06.檢查瓶子))
            {
                Say(pc, 0, 131, "この壷かな……？$R;", " ");

                Say(pc, 0, 131, "にゃん！にゃああ！！$R;" +
                "$Rにゃう～んん……$R;", "ネコ魂？？");

                Say(pc, 0, 131, "…この壷のようだ。$R;", " ");

                if (Select(pc, "どうする？", "", "壷を手に取る", "やめておく") == 1)
                {
                    if (PET(pc, 10017906))
                        Say(pc, 0, 131, "にゃん、にゃあ！$R;", "ネコマタ（菫）");

                    if (PET(pc, 10017900))
                        Say(pc, 0, 131, "にゃんっ！にゃぁにゃあぁ♪$R;", "ネコマタ（桃）");

                    if (PET(pc, 10017907))
                        Say(pc, 0, 131, "にゃ、にゃにゃあ！$R;", "ネコマタ（茜）");

                    if (PET(pc, 10017905))
                        Say(pc, 0, 131, "にゃにゃん、にゃんにゃん～ん$R;", "ネコマタ（山吹）");

                    if (PET(pc, 10017903))
                        Say(pc, 0, 131, "にゃあ～、にゃにゃあ～$R;", "ネコマタ（藍）");

                    if (PET(pc, 10017902))
                        Say(pc, 0, 131, "にゃ、$R;", "ネコマタ（緑）");

                    Say(pc, 0, 131, "にゃっ…、$Rにゃん！にゃにゃんにゃあっ！！$R;", "ネコ魂？？");

                    Say(pc, 0, 131, "うわ、$R;" +
                    "$R…いつもにもまして$Rネコマタが騒がしいな…。$R;", " ");

                    Say(pc, 0, 131, "重たそうだ…$R;" +
                    "$R…よっこらしょっと。$R;", " ");

                    Say(pc, 0, 131, "にゃあっ！！！$R;", "ネコ魂？？");
                    Wait(pc, 660);
                    ShowEffect(pc, 11001340, 8014);
                    ShowEffect(pc, 8013);
                    PlaySound(pc, 2430, false, 100, 50);
                    Wait(pc, 990);

                    Say(pc, 0, 131, "壷の中から何かが飛び出してきた！！$R;", " ");
                    ShowEffect(pc, 2, 5, 9605);
                    Neko_06_cmask.SetValue(Neko_06.檢查瓶子, true);
                    Wait(pc, 5940);
                    pc.CInt["Neko_06_Map_02"] = CreateMapInstance(50036000, 10062000, 130, 180);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_02"], 6, 4);
                }
                return;
            }
            Say(pc, 0, 131, "！！！$R;", "");
        }

        public bool PET(ActorPC pc,uint PETID)
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