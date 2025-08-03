using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131001
{
    public class S11000768 : Event
    {
        public S11000768()
        {
            this.EventID = 11000768;
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "咦？$R;" +
    "$R任务点数好像不够喔$R;" +
    "不好意思，可以明天再来吗？$R;";
            this.questFailed = "失败了？呜呜呜呜$R;";
            this.questCompleted = "做得好！$R;" +
    "看来您挺有天份啊。$R;";
            this.questCanceled = "取消？呜呜呜！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];
            if (!mask.Test(MZTYSFlags.交給謎語團面具))//_6a31)
            {
                if (mask.Test(MZTYSFlags.幫忙送謎語團面具))//_6a27)
                {
                    if (CountItem(pc, 10000306) == 0)
                    {
                        Say(pc, 131, "什么？把药丢了？$R;" +
                            "真是个糊涂的傢伙$R;" +
                            "给您，这次不要再丢了$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜托您了$R;" +
                                "$R一定要送到圣女岛的洋子那里喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "这样不行喔$R;");
                    }

                    Say(pc, 131, "怎么了？去圣女岛$R;" +
                        "可以从那边的秘密通道过去$R;" +
                        "$R拜托了$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.收到謎語團面具))//_6a30)
                {
                    mask.SetValue(MZTYSFlags.幫忙送謎語團面具, true);
                    //_6a27 = true;
                    TakeItem(pc, 50025080, 1);
                    GiveItem(pc, 10000306, 1);
                    Say(pc, 131, "啊，就是这个$R;" +
                        "我的『谜之团面具』！！$R;" +
                        "幸好有它，这下好了。$R;" +
                        "$R现在不用担心坏掉了$R;" +
                        "可以出去了$R;" +
                        "$P因为担心在没有面具的情况下$R;" +
                        "和海盗见面$R;" +
                        "所以我一直不敢出去呢$R;" +
                        "$P对了，不用再给洋子送一次药吗？$R;" +
                        "$R洋子对我来说，是个非常重要的人$R;" +
                        "如果不保持健康的话，就麻烦了！$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.交給藥))//_6a28)
                {
                    Say(pc, 131, "已经给我送过去了吗?$R;" +
                        "真是感谢呢$R;" +
                        "$R那，洋子没有让您给我带点什么吗？$R;" +
                        "$P什么…没有？$R;" +
                        "$R那真是不好意思，您能不能$R;" +
                        "到洋子在的地方再问一次$R;" +
                        "有没有要给我的东西呢？$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.幫忙送藥))//_6a26)
                {
                    if (CountItem(pc, 10000306) == 0)
                    {
                        Say(pc, 131, "什么？把药丢了？$R;" +
                            "真是个糊涂的傢伙$R;" +
                            "给您，这次不要再丢了$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜托您了$R;" +
                                "$R一定要送到圣女岛的洋子那里喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "这样不行喔$R;");
                    }
                    Say(pc, 131, "怎么了？去圣女岛$R;" +
                        "可以从那边的秘密通道过去$R;" +
                        "$R拜托了$R;");
                    return;
                }
                Say(pc, 131, "啊，这可怎么办啊？$R;" +
                    "洋子身体又出问题了$R;" +
                    "$P现在那个也没了啊$R;" +
                    "$R那个要是破碎了，$R;" +
                    "那我该怎么办啊？$R;" +
                    "$P哦，是您，太好了！$R;" +
                    "$R您把这药$R;" +
                    "转交给住在圣女岛的洋子好吗？$R;" +
                    "$P因为我现在不能出去喔$R;" +
                    "$R拜托了$R;");
                switch (Select(pc, "怎么办呢？", "", "做", "不做"))
                {
                    case 1:
                        Say(pc, 131, "您真的会转交给他吧$R;" +
                            "真是不错的傢伙呢$R;" +
                            "$R这样险恶的世界上$R;" +
                            "还让我遇到您这样的好人$R;" +
                            "谢谢您$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜托您了$R;" +
                                "$R一定要送到圣女岛的洋子那里喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "这样不行喔$R;");
                        break;
                }
                return;
            }


            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    int a = Global.Random.Next(1, 2);

                    if (a == 1)
                    {
                        Say(pc, 131, "是来见团长的吗？$R;" +
                            "$R团长现在不在$R;" +
                            "他跟别的干部出去巡查了$R;");
                        return;
                    }
                    Say(pc, 131, "同伴啊!$R;" +
                        "来得正好，快进去歇会儿吧！$R;");
                    return;
                }
            }
            Say(pc, 131, "呵呵$R;" +
                "$R带著神秘的面具，$R;" +
                "坚持信念活下去的，就是谜之团$R;" +
                "$R我是谜之团的干部布鲁。$R;");

            if (!mask.Test(MZTYSFlags.詢問謎團2))//_6a24)
            {
                if (mask.Test(MZTYSFlags.詢問謎團1))//_6a23)
                {
                    mask.SetValue(MZTYSFlags.詢問謎團1, true);
                    //_6a23 = true;
                    Say(pc, 131, "呵呵，$R;" +
                        "好像对我们谜之团略知一二，$R;" +
                        "那么要不要来参加我们的活动？$R;");

                    switch (Select(pc, "参加吗？", "", "参加", "不参加"))
                    {
                        case 1:
                            mask.SetValue(MZTYSFlags.詢問謎團2, true);
                            //_6a24 = true;
                            Say(pc, 131, "我们是热爱自由的谜之团$R;" +
                                "$R不可以受到束缚！$R;" +
                                "规则是用来打破的!$R;" +
                                "$R这就是谜之团存在的意义！$R;" +
                                "$P或许您知道『和古代民族的约定』吧？$R;" +
                                "$R那是什么呢？$R;" +
                                "那就是，大海是鱼人的领域$R;" +
                                "我们不能进去的约定$R;" +
                                "$P就是因为这个约定，$R;" +
                                "这个世界就没了进大海的习惯$R;" +
                                "所以船也不用$R;" +
                                "也不能去大海畅泳$R;" +
                                "穿泳装的女人也没有$R;" +
                                "$P所以我们决定破坏那个约定$R;" +
                                "一定可以的$R;" +
                                "$P但……$R;" +
                                "$P问题就是$R;" +
                                "有人比我们更早的破坏了那个约定$R;" +
                                "$R他们就是『地狱的海盗』$R;" +
                                "$P呜呜……那些海盗傢伙$R;" +
                                "$R绝对不能饶恕！绝对不能饶恕！$R;" +
                                "竟然比我们更早破坏约定$R;" +
                                "太令人气愤了！$R;" +
                                "$P所以我们$R;" +
                                "正在为了占据大海，和海盗战斗$R;" +
                                "$P所以希望您们能够帮我们打仗$R;" +
                                "$R具体内容呢$R;" +
                                "等到您们接受任务再说吧！$R;");
                            任務(pc);
                            break;
                    }
                    return;
                }
                Say(pc, 131, "来到这里$R;" +
                    "是不是你也想加入谜之团？$R;");
                switch (Select(pc, "想加入谜之团吗？", "", "不想加入", "想加入"))
                {
                    case 2:
                        if (pc.Level <= 63)
                        {
                            Say(pc, 131, "对不起，你还是回去吧$R;" +
                                "谜之团是以实力来筛选成员的$R;" +
                                "我们不需要弱者$R;");
                            return;
                        }

                        Say(pc, 131, "不好意思，你不可以入团$R;" +
                            "$R加入谜之团，要通过$R;" +
                            "严格的规则和考试，还有血统、$R;" +
                            "政治理由、人脉、钱……$R;" +
                            "$P反正，有很多条件，所以不行！！$R;" +
                            "$P但我想知道$R;" +
                            "你到底知不知道谜之团$R;" +
                            "是怎样的团体啊？$R;");

                        switch (Select(pc, "你知道吗?", "", "知道", "不知道"))
                        {
                            case 1:
                                ShowEffect(pc, 11000768, 4520);
                                //NPCMOTION_ONE 361 0 11000768
                                Wait(pc, 2000);
                                Say(pc, 131, "什么？$R;" +
                                    "怎么回事？难道是泄密了？$R;" +
                                    "不可能吧？$R;");
                                break;
                            case 2:
                                mask.SetValue(MZTYSFlags.詢問謎團1, true);
                                //_6a23 = true;
                                Say(pc, 131, "呵呵，$R;" +
                                    "好像对我们谜之团略知一二，$R;" +
                                    "那么要不要来参加我们的活动？$R;");

                                switch (Select(pc, "参加吗？", "", "参加", "不参加"))
                                {
                                    case 1:
                                        mask.SetValue(MZTYSFlags.詢問謎團2, true);
                                        //_6a24 = true;
                                        Say(pc, 131, "我们是热爱自由的谜之团$R;" +
                                            "$R不可以受到束缚！$R;" +
                                            "规则是用来打破的!$R;" +
                                            "$R这就是谜之团存在的意义！$R;" +
                                            "$P或许您知道『和古代民族的约定』吧？$R;" +
                                            "$R那是什么呢？$R;" +
                                            "那就是，大海是鱼人的领域$R;" +
                                            "我们不能进去的约定$R;" +
                                            "$P就是因为这个约定，$R;" +
                                            "这个世界就没了进大海的习惯$R;" +
                                            "所以船也不用$R;" +
                                            "也不能去大海畅泳$R;" +
                                            "穿泳装的女人也没有$R;" +
                                            "$P所以我们决定破坏那个约定$R;" +
                                            "一定可以的$R;" +
                                            "$P但……$R;" +
                                            "$P问题就是$R;" +
                                            "有人比我们更早的破坏了那个约定$R;" +
                                            "$R他们就是『地狱的海盗』$R;" +
                                            "$P呜呜……那些海盗傢伙$R;" +
                                            "$R绝对不能饶恕！绝对不能饶恕！$R;" +
                                            "竟然比我们更早破坏约定$R;" +
                                            "太令人气愤了！$R;" +
                                            "$P所以我们$R;" +
                                            "正在为了占据大海，和海盗战斗$R;" +
                                            "$P所以希望您们能够帮我们打仗$R;" +
                                            "$R具体内容呢$R;" +
                                            "等到您们接受任务再说吧！$R;");
                                        任務(pc);
                                        break;
                                }
                                break;
                        }
                        break;
                }
                return;
            }
            任務(pc);
        }

        void 任務(ActorPC pc)
        {
            switch (Select(pc, "有什么事吗?", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    HandleQuest(pc, 27);

                    if (pc.Quest != null)
                    {
                        if (pc.Quest.ID == 10031400 || pc.Quest.ID == 10031401)
                        {

                            Say(pc, 131, "这个要塞的东北方$R;" +
                                "有一个叫中立岛的岛屿$R;" +
                                "$R为了占据那个岛$R;" +
                                "希望你能够铲除那里的妖怪$R;" +
                                "$P可是，海盗们$R;" +
                                "好像也在打那个岛的主意呢$R;" +
                                "$R如果发现那些家伙$R;" +
                                "一定要好好教训他们啊！$R;");
                        }

                        if (pc.Quest.ID == 10031402 || pc.Quest.ID == 10031403)
                        {

                            Say(pc, 131, "这个要塞的西北方$R;" +
                                "有个叫海盗岛的岛屿$R;" +
                                "是我们死对头的岛$R;" +
                                "$R到那些家伙们的地盘$R;" +
                                "教训他们一顿再回来吧!!$R;");

                        }

                        if (pc.Quest.ID == 10031404)
                        {

                            Say(pc, 131, "那些傢伙偶尔会掉一些『血板』$R;" +
                                "$R那就是海盗的盟誓证明书$R;" +
                                "就像我们谜之团的面具一样$R;" +
                                "$P很明显，这对那些傢伙很重要的$R;" +
                                "去把那个抢回来吧!$R;" +
                                "$R拜托了$R;");
                        }

                        Say(pc, 131, "那么就拜托了$R;" +
                            "使用里面的秘密道路吧!$R;");
                    }
                    break;
            }
        }
    }
}