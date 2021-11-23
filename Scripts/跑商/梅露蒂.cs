using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaMap;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Manager;
using SagaMap.Network.Client;
namespace SagaScript.M30210000
{
    public class S60050005 : Event
    {
        public S60050005()
        {
            this.EventID = 60050005;
        }

        void ResetRate(ActorPC pc)
        {
            pc.AInt["跑商坦塔库尔Rate"] = Global.Random.Next(910, 1100);
            pc.AInt["跑商水晶Rate"] = Global.Random.Next(910, 1100);
            pc.AInt["跑商安布雷拉Rate"] = Global.Random.Next(870, 1150);
            pc.AInt["跑商南部挖矿Rate"] = Global.Random.Next(900, 1800);
            if (Global.Random.Next(0, 100) <= 10)
                pc.AInt["跑商南部挖矿Rate"] = Global.Random.Next(850, 1800);

            pc.AInt["跑商西部钓鱼Rate"] = Global.Random.Next(900, 1150);
            查看情报(pc);
        }
        void 查看情报(ActorPC pc)
        {

            Say(pc, 131, "根据其他来的商人的情报，$R现在各地的采购价格浮动为：$R$R" +
    "-诺顿地区：" + pc.AInt["跑商水晶Rate"] / 10 + "％$R" +
    "-艾恩萨乌斯：" + pc.AInt["跑商坦塔库尔Rate"] / 10 + "％$R" +
    "-摩戈地区：" + pc.AInt["跑商安布雷拉Rate"] / 10 + "％$R" +
    "-西部渔场：" + pc.AInt["跑商西部钓鱼Rate"] / 10 + "％$R" +
    "-南部矿区：" + pc.AInt["跑商南部挖矿Rate"] / 10 + "％$R" +
    "$R今日全服已累积换取EP：" + SInt["服务器今天累积兑换EP数量"]
    , "梅露蒂");
        }
        void 查看搬运次数(ActorPC pc)
        {
            Say(pc, 131, "以下是你已完成的搬运次数：$R$R" +
       "-诺顿地区：" + pc.AInt["北国搬运完成"] + "次$R" +
       "-艾恩萨乌斯：" + pc.AInt["南国搬运完成"] + "次$R" +
       "-摩戈地区：" + pc.AInt["西国搬运完成"] + "次$R" +
       "-西部渔场：" + pc.AInt["西部渔场搬运完成"] + "次$R" +
       "-南部矿区：" + pc.AInt["南部矿区搬运完成"] + "次$R"
       , "梅露蒂");
        }
        void AcceptQuest(ActorPC pc)
        {
            //查看搬运次数(pc);
            uint itemID = 0;
            int questpoint = 0;
            switch (Select(pc, "请选择运送路线", "", "诺顿地区(20点任务点)[报酬:5wG~8wG]", "艾恩萨乌斯(20点任务点)[报酬:5wG~8wG]", "摩戈地区(20点任务点)[报酬:5wG~8wG]", "西部开拓地(30点任务点)[报酬:8wG~12wG]", "南部地牢B3F(50点任务点)[报酬:18wG~25wG]", "还是算了"))
            {
                case 1:
                    itemID = 953200000;
                    questpoint = 20;
                    break;
                case 2:
                    itemID = 953200001;
                    questpoint = 20;
                    break;
                case 3:
                    itemID = 953200002;
                    questpoint = 20;
                    break;
                case 4:
                    itemID = 953200004;
                    questpoint = 30;
                    break;
                case 5:
                    itemID = 953200003;
                    questpoint = 50;
                    break;
                case 6:
                    return;
            }
            if (itemID == 0)
                return;
            if (pc.Inventory.Items[ContainerType.BODY].Count + pc.Inventory.Equipments.Count > 100)
            {
                Say(pc, 131, "身上的东西是不是太多了呢？", "梅露蒂");
                return;
            }
            if (pc.QuestRemaining < questpoint)
            {
                Say(pc, 131, "你的任务点似乎不够？", "梅露蒂");
                return;
            }
            pc.AInt["搬运次数"]++;
            pc.AInt["本周消耗任务点"] += questpoint;
            pc.QuestRemaining -= (ushort)questpoint;
            MapClient.FromActorPC(pc).SendQuestPoints();
            GiveItem(pc, itemID, 1);
            pc.AInt["接受了搬运任务"] = 1;
            Say(pc, 131, "那么就交给你咯！$R路上可能会遇到劫匪，$R但我相信你一定不会怕他们的！", "梅露蒂");
        }
        public override void OnEvent(ActorPC pc)
        {
            pc.AInt["跑商次数卡死2"] = 0;
            if (SStr["梅露蒂每日重置"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                SStr["梅露蒂每日重置"] = DateTime.Now.ToString("yyyy-MM-dd");
                SInt["服务器今天累积兑换EP数量"] = 0;
            }

            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            if (Golem == null && pc.Account.GMLevel < 10)
            {
                SInt[pc.Name + "跑商异常"]++;
                return;
            }
            if (pc.AStr["跑商次数重置"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pc.AStr["跑商次数重置"] = DateTime.Now.ToString("yyyy-MM-dd");
                pc.AInt["跑商次数"] = 0;
                pc.AInt["搬运次数"] = 0;
                pc.AInt["跑商次数卡死2"] = 0;
            }

            Say(pc, 0, "欢迎光临鱼缸商会！$R$R请问您有什么需要呢？", "梅露蒂");
            switch (Select(pc, "怎么办呢？当前拥有E币:" + pc.ECoin, "", "接受搬运任务[每日限制：" + pc.AInt["搬运次数"] + "/3]", "出售商品(刷新价格每小时刷新)", "换取5万E币(5万金币和15点任务点)[每日限制：" + pc.AInt["跑商次数"] + "/5]", "换取20万E币(20万金币和90点任务点)[每日限制：" + pc.AInt["跑商次数"] + "/5]", "E币有什么用？", "我可以从哪里买到你要的商品？", "查看价格情报及完成的搬运次数", "再见"))
            {
                case 1:
                    if (pc.AInt["搬运次数"] >= 3)
                    {
                        Say(pc, 131, "你今天已经完成了足够多次啦$R$R请明天再来吧", "梅露蒂");
                        return;
                    }
                    if (pc.AInt["接受了搬运任务"] == 1)
                    {
                        Say(pc, 131, "你似乎有商品还没有送到呢。$R$R请先送到后再来找我哦。", "梅露蒂");
                        if (Select(pc, "是否取消任务呢？", "", "我想取消掉了..", "离开") == 1)
                        {
                            pc.AInt["接受了搬运任务"] = 0;
                            Say(pc, 131, "真是太可惜了...$R$R已经为你取消掉了，下次加油哦", "梅露蒂");
                            PlaySound(pc, 4008, false, 100, 50);
                        }
                        return;
                    }
                    Say(pc, 131, "啊，我这里正好$R有一些需要送出去的货物，$R$R你能顺便帮忙带过去吗？", "梅露蒂");
                    AcceptQuest(pc);
                    break;
                case 2:
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 3:
                    if (Select(pc, "确定要『换取5万E币』吗？", "", "是的，换50000的E币", "还是算了") == 2) return;
                    if (pc.AInt["跑商次数"] >= 5)
                    {
                        Say(pc, 131, "你今天已经完成了足够多次啦$R$R请明天再来吧", "梅露蒂");
                        return;
                    }
                    if (pc.Gold < 50000)
                    {
                        Say(pc, 131, "嗯？你似乎钱不够呢。", "梅露蒂");
                        return;
                    }
                    else if (pc.ECoin > 20000)
                    {
                        Say(pc, 131, "你身上还有超过20000以上的E币，$R我不能继续给你换哦。", "梅露蒂");
                        return;
                    }
                    else if (pc.QuestRemaining < 15)
                    {
                        Say(pc, 131, "你的任务点似乎不够？", "梅露蒂");
                        return;
                    }
                    else
                    {
                        SInt["服务器今天累积兑换EP数量"] += 50000;
                        pc.AInt["跑商次数"]++;
                        pc.Gold -= 50000;
                        pc.ECoin += 50000;
                        TitleProccess(pc, 57, 50000);
                        pc.AInt["本周消耗任务点"] += 15;
                        pc.QuestRemaining -= 15;
                        MapClient.FromActorPC(pc).SendQuestPoints();
                        Say(pc, 131, "换好啦！$R请采购一些品质优秀的东西来哦。", "梅露蒂");
                        ResetRate(pc);
                        if (pc.AInt["接受了搬运任务"] != 1 && pc.AInt["搬运次数"] < 3)
                        {
                            Say(pc, 131, "那个...$R你不如再来接个搬运任务呢？", "梅露蒂");
                            if (Select(pc, "是否接受搬运任务？", "", "接受搬运任务[每日限制：" + pc.AInt["搬运次数"] + "/3]", "还是算了") == 1)
                                AcceptQuest(pc);
                        }
                        return;
                    }
                case 4:
                    if (Select(pc, "确定要『换取20万E币』吗？", "", "是的，换200000的E币", "还是算了") == 2) return;
                    if (pc.AInt["跑商次数"] >= 5)
                    {
                        Say(pc, 131, "你今天已经完成了足够多次啦$R$R请明天再来吧", "梅露蒂");
                        return;
                    }
                    if (pc.Gold < 200000)
                    {
                        Say(pc, 131, "嗯？你似乎钱不够呢。", "梅露蒂");
                        return;
                    }
                    else if (pc.ECoin > 20000)
                    {
                        Say(pc, 131, "你身上还有超过20000以上的E币，$R我不能继续给你换哦。", "梅露蒂");
                        return;
                    }
                    else if (pc.QuestRemaining < 90)
                    {
                        Say(pc, 131, "你的任务点似乎不够？", "梅露蒂");
                        return;
                    }
                    else
                    {
                        SInt["服务器今天累积兑换EP数量"] += 200000;
                        pc.AInt["跑商次数"]++;
                        pc.Gold -= 200000;
                        pc.ECoin += 200000;
                        pc.QuestRemaining -= 90;
                        pc.AInt["本周消耗任务点"] += 90;
                        TitleProccess(pc, 57, 200000);
                        MapClient.FromActorPC(pc).SendQuestPoints();
                        Say(pc, 131, "换好啦！$R请采购一些品质优秀的东西来哦。", "梅露蒂");
                        ResetRate(pc);
                        if (pc.AInt["接受了搬运任务"] != 1 && pc.AInt["搬运次数"] < 3)
                        {
                            Say(pc, 131, "那个...$R你不如再来接个搬运任务呢？", "梅露蒂");
                            if (Select(pc, "是否接受搬运任务？", "", "接受搬运任务[每日限制：" + pc.AInt["搬运次数"] + "/3]", "还是算了") == 1)
                                AcceptQuest(pc);
                        }
                        return;
                    }
                    break;
                case 5:
                    Say(pc, 131, "E币可以在旧世界大陆从$R其他NPC处购买商品$R$R然后我会用更高的价格收购这些商品哦。", "梅露蒂");
                    break;
                case 6:
                    Say(pc, 131, "目前的货源在南国、北国、还有西国，$R你可以从这三个地方采购商品喔。", "梅露蒂");
                    break;
                case 7:
                    if (pc.ECoin > 0)
                    {
                        查看情报(pc);
                        查看搬运次数(pc);
                    }
                    else
                    {
                        Say(pc, 131, "现在没有价格情报哦。", "梅露蒂");
                    }
                    break;
            }
        }
        void SetBuylList(ActorGolem golem)
        {
            golem.BuyLimit = 100000000;

            if (SInt["梅露蒂自动刷新"] != DateTime.Now.Hour)
            {
                SInt["梅露蒂自动刷新"] = DateTime.Now.Hour;
                golem.BuyShop.Clear();
            }

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 10015500;
                if (SagaLib.Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 2500);
                else
                    item.Price = (uint)Global.Random.Next(500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(1, item);
            }
            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 10023400;
                if (SagaLib.Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 2500);
                else
                    item.Price = (uint)Global.Random.Next(500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(2, item);
            }
            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 10023501;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 4800);
                else
                    item.Price = (uint)Global.Random.Next(500, 3500);
                item.Count = 9999;
                golem.BuyShop.Add(3, item);
            }
            if (!golem.BuyShop.ContainsKey(4))
            {
                item = new GolemShopItem();
                item.ItemID = 10023200;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(8000, 24200);
                else
                    item.Price = (uint)Global.Random.Next(8000, 16000);
                item.Count = 9999;
                golem.BuyShop.Add(4, item);
            }
            if (!golem.BuyShop.ContainsKey(5))
            {
                item = new GolemShopItem();
                item.ItemID = 10023100;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(3000, 12200);
                else
                    item.Price = (uint)Global.Random.Next(3000, 9000);
                item.Count = 9999;
                golem.BuyShop.Add(5, item);
            }
            if (!golem.BuyShop.ContainsKey(6))
            {
                item = new GolemShopItem();
                item.ItemID = 10019303;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 2700);
                else
                    item.Price = (uint)Global.Random.Next(500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(6, item);
            }
            if (!golem.BuyShop.ContainsKey(7))
            {
                item = new GolemShopItem();
                item.ItemID = 10020551;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 2700);
                else
                    item.Price = (uint)Global.Random.Next(500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(7, item);
            }
            if (!golem.BuyShop.ContainsKey(8))
            {
                item = new GolemShopItem();
                item.ItemID = 10019350;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(10000, 36000);
                else
                    item.Price = (uint)Global.Random.Next(10000, 29000);
                item.Count = 9999;
                golem.BuyShop.Add(8, item);
            }
            if (!golem.BuyShop.ContainsKey(9))
            {
                item = new GolemShopItem();
                item.ItemID = 10018209;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(5000, 20800);
                else
                    item.Price = (uint)Global.Random.Next(5000, 14000);
                item.Count = 9999;
                golem.BuyShop.Add(9, item);
            }
            if (!golem.BuyShop.ContainsKey(10))
            {
                item = new GolemShopItem();
                item.ItemID = 10011600;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(2000, 7300);
                else
                    item.Price = (uint)Global.Random.Next(2000, 5000);
                item.Count = 9999;
                golem.BuyShop.Add(10, item);
            }
            if (!golem.BuyShop.ContainsKey(11))
            {
                item = new GolemShopItem();
                item.ItemID = 10002580;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(2000, 7300);
                else
                    item.Price = (uint)Global.Random.Next(2000, 5000);
                item.Count = 9999;
                golem.BuyShop.Add(11, item);
            }
            if (!golem.BuyShop.ContainsKey(12))
            {
                item = new GolemShopItem();
                item.ItemID = 10009480;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(1000, 2500);
                else
                    item.Price = (uint)Global.Random.Next(1000, 1800);
                item.Count = 9999;
                golem.BuyShop.Add(12, item);
            }
            if (!golem.BuyShop.ContainsKey(13))
            {
                item = new GolemShopItem();
                item.ItemID = 10022389;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(2000, 13000);
                else
                    item.Price = (uint)Global.Random.Next(2000, 9000);
                item.Count = 9999;
                golem.BuyShop.Add(13, item);
            }
            if (!golem.BuyShop.ContainsKey(14))
            {
                item = new GolemShopItem();
                item.ItemID = 10018600;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(15000, 34300);
                else
                    item.Price = (uint)Global.Random.Next(15000, 26000);
                item.Count = 9999;
                golem.BuyShop.Add(14, item);
            }
            if (!golem.BuyShop.ContainsKey(15))
            {
                item = new GolemShopItem();
                item.ItemID = 10016700;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(1500, 4830);
                else
                    item.Price = (uint)Global.Random.Next(1500, 3500);
                item.Count = 9999;
                golem.BuyShop.Add(15, item);
            }
            if (!golem.BuyShop.ContainsKey(16))
            {
                item = new GolemShopItem();
                item.ItemID = 10020408;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(3000, 13000);
                else
                    item.Price = (uint)Global.Random.Next(3000, 10000);
                item.Count = 9999;
                golem.BuyShop.Add(16, item);
            }
            if (!golem.BuyShop.ContainsKey(17))
            {
                item = new GolemShopItem();
                item.ItemID = 953210005;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(300, 1300);
                else
                    item.Price = (uint)Global.Random.Next(300, 1000);
                item.Count = 9999;
                golem.BuyShop.Add(17, item);
            }
            if (!golem.BuyShop.ContainsKey(18))
            {
                item = new GolemShopItem();
                item.ItemID = 953210006;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(500, 2400);
                else
                    item.Price = (uint)Global.Random.Next(500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(18, item);
            }
            if (!golem.BuyShop.ContainsKey(19))
            {
                item = new GolemShopItem();
                item.ItemID = 953210007;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(1800, 4830);
                else
                    item.Price = (uint)Global.Random.Next(1800, 3000);
                item.Count = 9999;
                golem.BuyShop.Add(19, item);
            }
            if (!golem.BuyShop.ContainsKey(20))
            {
                item = new GolemShopItem();
                item.ItemID = 953210008;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(5000, 24000);
                else
                    item.Price = (uint)Global.Random.Next(5000, 20000);
                item.Count = 9999;
                golem.BuyShop.Add(20, item);
            }
            if (!golem.BuyShop.ContainsKey(21))
            {
                item = new GolemShopItem();
                item.ItemID = 953210009;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(10000, 50000);
                else
                    item.Price = (uint)Global.Random.Next(10000, 40000);
                item.Count = 9999;
                golem.BuyShop.Add(21, item);
            }

            if (!golem.BuyShop.ContainsKey(22))
            {
                item = new GolemShopItem();
                item.ItemID = 953210000;
                if (Global.Random.Next(0, 100) <= 8)
                    item.Price = (uint)Global.Random.Next(1500, 3500);
                else
                    item.Price = (uint)Global.Random.Next(1500, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(22, item);
            }
            if (!golem.BuyShop.ContainsKey(23))
            {
                item = new GolemShopItem();
                item.ItemID = 953210001;
                if (Global.Random.Next(0, 100) <= 8)
                    item.Price = (uint)Global.Random.Next(4000, 10500);
                else
                    item.Price = (uint)Global.Random.Next(4000, 6000);
                item.Count = 9999;
                golem.BuyShop.Add(23, item);
            }
            if (!golem.BuyShop.ContainsKey(24))
            {
                item = new GolemShopItem();
                item.ItemID = 953210002;
                if (Global.Random.Next(0, 100) <= 8)
                    item.Price = (uint)Global.Random.Next(5000, 14000);
                else
                    item.Price = (uint)Global.Random.Next(5000, 8000);
                item.Count = 9999;
                golem.BuyShop.Add(24, item);
            }
            if (!golem.BuyShop.ContainsKey(25))
            {
                item = new GolemShopItem();
                item.ItemID = 953210003;
                if (Global.Random.Next(0, 100) <= 8)
                    item.Price = (uint)Global.Random.Next(7000, 17500);
                else
                    item.Price = (uint)Global.Random.Next(7000, 10000);
                item.Count = 9999;
                golem.BuyShop.Add(25, item);
            }
            if (!golem.BuyShop.ContainsKey(26))
            {
                item = new GolemShopItem();
                item.ItemID = 953210004;
                if (Global.Random.Next(0, 100) <= 8)
                    item.Price = (uint)Global.Random.Next(15000, 35000);
                else
                    item.Price = (uint)Global.Random.Next(15000, 20000);
                item.Count = 9999;
                golem.BuyShop.Add(26, item);
            }
            return;
        }
    }
}

