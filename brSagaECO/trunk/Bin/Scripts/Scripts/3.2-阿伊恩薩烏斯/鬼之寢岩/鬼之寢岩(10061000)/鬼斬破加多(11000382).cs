using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10061000
{
    public class S11000382 : Event
    {
        public S11000382()
        {
            this.EventID = 11000382;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];


            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                !Neko_06_cmask.Test(Neko_06.獲得堇子的碎片))
            {
                Wait(pc, 990);
                ShowEffect(pc, 137, 32, 5058);
                Wait(pc, 990);
                ShowEffect(pc, 137, 32, 5179);
                Wait(pc, 1980);

                Say(pc, 0, 131, "（……あっ$R;" +
                "$Rなんだか懐かしい気配がする…？$R;" +
                "$Rこの気配は………菫！）$R;", " ");

                Say(pc, 0, 131, "あ～っ、かっこいいおじちゃん！$R;" +
                "$Rボクもいつか$Rこんなかっこいい男の子になるの！$R;" +
                "$P…ねえねえ$R;" +
                "KOU$R;" +
                "$Rりろって子はここにいるの？$R;", "ネコマタ（杏）");

                Say(pc, 131, "な、なんだ、なんだ？$R;" +
                "$Rたしかにリロは$R後ろの小屋で昼寝しているが…。$R;", "鬼斬りガドゥ");

                Say(pc, 0, 131, "にゃお～～～ん！$R;", " ");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);

                Say(pc, 0, 131, "『背負い魔・ネコマタ（菫）』$Rの心を取り戻した！$R;", " ");

                Say(pc, 0, 131, "お姉ちゃん！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "まあ、杏！？$R;" +
                "$R…わたし、どうしてここに…？$R;", "ネコマタ（菫）");

                Say(pc, 0, 131, "（ほっ…$Rやっぱりここだった…よかった。）$R;", " ");
                Neko_06_cmask.SetValue(Neko_06.獲得堇子的碎片, true);
                return;
            }

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話) &&
                !Neko_03_cmask.Test(Neko_03.與鬼斬破多加對話))
            {
                Neko_03_cmask.SetValue(Neko_03.與鬼斬破多加對話, true);
                Say(pc, 11000382, 131, "$R您看起來臉色不好，$R有什麼事嗎？$R;");
                Say(pc, 0, 131, "告訴加多發生什麼事$R;");
                Say(pc, 11000382, 131, "什麼?$R;" +
                    "哇哈哈哈哈，您是開玩笑吧！？$R;" +
                    "那種事，$R您問我是不是沒做過，$R我說沒有，您也不信吧$R;" +
                    "哪有女孩願意跟我這樣的傢伙結婚?$R;" +
                    "$P現在更擔心的是，$R被混成騎士團抓走的瑪莎阿$R;" +
                    "$P我以前不懂事的時候$R欠她媽媽的人情呢。$R;" +
                    "$R不可以光看著！$R;" +
                    "$P好！我要救瑪莎！$R;" +
                    "$P那麼被騎士團抓走的孩子……$R;" +
                    "$R就找解決師大叔幫忙吧。$R;" +
                    "$R說我的名字的話$R會告訴您行會宮殿的$R地下秘密通道唷。$R;" +
                    "$P他對阿高普路斯市的小路很熟悉的$R;");
                return;
            }
            Say(pc, 131, "我是抓鬼的加多。$R;" +
                "是個有點名氣的冒險家唷$R;" +
                "本來打算去旅行的$R;" +
                "最後來到阿伊恩薩烏斯這裡來$R;" +
                "但是這裡太熱了。$R;");
            //搬运任务,技能点和商店
            /*
            //EVT1100038202
            Say(pc, 131, "呵呵，上次替我辦事，$R;" +
                "真是感謝呢。$R;" +
                "$R小小的謝禮$R;" +
                "特別給您看這個珍貴的裝備$R只有您可以看啊！$R;");
            //SHOP BUY 112
            //EVENTEND
            //EVT1100038209
            if (CheckInventory(pc, 10000201, 1))
            {
                //EVT1100038210
                //_2a70 = true;
                GiveItem(pc, 10000201, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "這只是我的心意，不用客氣$R;" +
                    "$R應該可以造出好武器的唷$R;");
                //EVENTEND
                return;
            }
            Say(pc, 131, "$P您的行李太多了，$R;" +
                "$R先去整理行李後$R;" +
                "然後再來吧$R;");
            //EVENTEND
            //EVT1100038211
            //_2a71 = true;
            SkillPointBonus(pc, 1);
            Wait(pc, 2000);
            PlaySound(pc, 3087, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 2000);
            Say(pc, 131, "技能點數上升1點$R;");
            Say(pc, 131, "這只是我的心意，$R;" +
                "不用客氣阿$R;");
            //EVENTEND
            */
        }

        void 菫任务(ActorPC pc)
        {
        
            //EVT1100038212
            //_7A36 = true;
            Say(pc, 131, "$R您看起來臉色不好，$R有什麼事嗎？$R;");
            Say(pc, 131, "告訴加多發生什麼事$R;");
            Say(pc, 131, "什麼?$R;" +
                "哇哈哈哈哈，您是開玩笑吧！？$R;" +
                "那種事，$R您問我是不是沒做過，$R我說沒有，您也不信吧$R;" +
                "哪有女孩願意跟我這樣的傢伙結婚?$R;" +
                "$P現在更擔心的是，$R被混成騎士團抓走的瑪莎阿$R;" +
                "$P我以前不懂事的時候$R欠她媽媽的人情呢。$R;" +
                "$R不可以光看著！$R;" +
                "$P好！我要救瑪莎！$R;" +
                "$P那麼被騎士團抓走的孩子……$R;" +
                "$R就找解決師大叔幫忙吧。$R;" +
                "$R說我的名字的話$R會告訴您行會宮殿的$R地下秘密通道唷。$R;" +
                "$P他對阿高普路斯市的小路很熟悉的$R;");
            //EVENTEND
    }
    }
}
