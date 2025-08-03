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
            this.questTransportDest = "噢噢噢,来了吗? 真是太及时了.";
            this.questTransportCompleteDest = "噢~.非常感谢你的东西,快去拿你的奖励吧";
            this.gotTransportQuest = "快把我要的东西送过来!";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];
            BitMask<鬼斩破加多任务标记> mask = new BitMask<鬼斩破加多任务标记>(pc.CMask["GhostSlashQuestFlag"]);

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
                Say(pc, 11000382, 131, "$R您看起来脸色不好，$R有什么事吗？$R;");
                Say(pc, 0, 131, "告诉加多发生什么事$R;");
                Say(pc, 11000382, 131, "什么?$R;" +
                    "哇哈哈哈哈，您是开玩笑吧！？$R;" +
                    "那种事，$R您问我是不是没做过，$R我说没有，您也不信吧$R;" +
                    "哪有女孩愿意跟我这样的家伙结婚?$R;" +
                    "$P现在更担心的是，$R被混成骑士团抓走的玛莎啊$R;" +
                    "$P我以前不懂事的时候$R欠她妈妈的人情呢。$R;" +
                    "$R不可以光看着！$R;" +
                    "$P好！我要救玛莎！$R;" +
                    "$P那么被骑士团抓走的孩子……$R;" +
                    "$R就找猜谜大叔帮忙吧。$R;" +
                    "$R说我的名字的话$R会告诉您行会宫殿的$R地下的秘密通道。$R;" +
                    "$P他对阿克罗波利斯的小路很熟悉的$R;");
                return;
            }
            if (pc.CInt["GhostSlashQuestCounter"] == 0)
            {
                Say(pc, 131, "我是抓鬼的加多。$R;" +
                    "是个有点名气的冒险家$R;" +
                    "本来打算去旅行的$R;" +
                    "最后来到艾恩萨乌斯这里来$R;" +
                    "但是这里太热了。$R;");
                return;
            }
            if (pc.CInt["GhostSlashQuestCounter"] == 1)
            {
                if (!mask.Test(鬼斩破加多任务标记.领取了第一次奖励))
                {
                    if (CheckInventory(pc, 10000201, 1))
                    {
                        Say(pc, 131, "呵呵，上次替我辦事，$R;" + "真是感謝呢。$R;" + "$R小小的謝禮$R;");
                        //EVT1100038210
                        //_2a70 = true;
                        mask.SetValue(鬼斩破加多任务标记.领取了第一次奖励, true);
                        Say(pc, 131, "获得了一个[" + SagaDB.Item.ItemFactory.Instance.Items[10000201].name + "]$R;");
                        GiveItem(pc, 10000201, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "這只是我的心意，不用客氣$R;" +
                            "$R應該可以造出好武器的唷$R;");
                        //EVENTEND
                    }
                    else
                    {
                        Say(pc, 131, "$P您的行李太多了，$R;" +
                            "$R先去整理行李後$R;" +
                            "然後再來吧$R;");
                    }
                }
                //EVENTEND
                //EVT1100038211
                //_2a71 = true;
                if (!mask.Test(鬼斩破加多任务标记.领取了技能点奖励))
                {
                    SkillPointBonus(pc, 1);
                    Wait(pc, 2000);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    Say(pc, 131, "技能點數上升1點$R;");
                    Say(pc, 131, "這只是我的心意，$R;" +
                        "不用客氣阿$R;");
                    mask.SetValue(鬼斩破加多任务标记.领取了技能点奖励, true);
                }
                //EVENTEND
                if (mask.Test(鬼斩破加多任务标记.领取了第一次奖励) && mask.Test(鬼斩破加多任务标记.领取了技能点奖励))
                {
                    Say(pc, 131, "虽然您给我带来了一些好东西.$R;" +
                        "但是这里依旧很热啊.....$R;" +
                        "能不能再给我带一些来呢?$R;" +
                        "放心,我不会亏待您的!$R;");
                    return;
                }
            }
            if (pc.CInt["GhostSlashQuestCounter"] >= 2)
            {
                Say(pc, 131, "非常感谢您多次的帮助.$R;" +
                    "那现在就让您看看我珍藏的装备吧!$R;");
                int reducerate = pc.CInt["GhostSlashQuestCounter"];
                if (reducerate > 500)
                    reducerate = 500;
                uint[] goodslist = new uint[] { 60001450, 60002350, 60001550, 60002450, 50006950, 50003851, 50006850, 50014050, 50062050, 50004150, 50012650 };
                //OpenShopByList(pc, (uint)(1000 - reducerate), SagaDB.Npc.ShopType.None, goodslist);
                OpenShopBuy(pc, 112);
            }
        }

        void 菫任务(ActorPC pc)
        {

            //EVT1100038212
            //_7A36 = true;
            Say(pc, 131, "$R您看起来脸色不好，$R有什么事吗？$R;");
            Say(pc, 131, "告诉加多发生什么事$R;");
            Say(pc, 131, "什么?$R;" +
                "哇哈哈哈哈，您是开玩笑吧！？$R;" +
                "那种事，$R您问我是不是没做过，$R我说没有，您也不信吧$R;" +
                "哪有女孩愿意跟我这样的家伙结婚?$R;" +
                "$P现在更担心的是，$R被混成骑士团抓走的玛莎啊$R;" +
                "$P我以前不懂事的时候$R欠她妈妈的人情呢。$R;" +
                "$R不可以光看着！$R;" +
                "$P好！我要救玛莎！$R;" +
                "$P那么被骑士团抓走的孩子……$R;" +
                "$R就找猜谜大叔帮忙吧。$R;" +
                "$R说我的名字的话$R会告诉您行会宫殿的$R地下的秘密通道。$R;" +
                "$P他对阿克罗波利斯的小路很熟悉的$R;");
            //EVENTEND
        }
    }
    public enum 鬼斩破加多任务标记
    {
        领取了第一次奖励 = 0x1,
        领取了技能点奖励 = 0x2
    }
}
