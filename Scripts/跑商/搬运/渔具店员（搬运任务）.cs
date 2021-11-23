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
using SagaMap.Network.Client;
using SagaMap.Manager;
namespace SagaScript.M30210000
{
    public class S83000001 : Event
    {
        public S83000001()
        {
            this.EventID = 83000001;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*if (pc.AInt["跑商次数卡死2"] == 1)
            {
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }
            byte count = 0;
            string lastip = "";
            foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                if (i.Character.Account.LastIP == pc.Account.LastIP && i.Character.Account.GMLevel < 20)
                    count++;
            if (count > 1)
            {
                foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    if (i.Character.Account.LastIP == lastip && i.Character.Account.GMLevel < 20)
                        i.Character.AInt["跑商次数卡死2"] = 1;
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }*/
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            if (Golem == null && pc.Account.GMLevel < 10)
            {
                SInt[pc.Name + "跑商异常"]++;
                return;
            }

            if (pc.AStr["鱼饵购买次数重置"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pc.AStr["鱼饵购买次数重置"] = DateTime.Now.ToString("yyyy-MM-dd");
                pc.AInt["鱼饵购买次数"] = 0;
            }
            Say(pc, 131, "欢迎光临，请问需要点什么吗？", "渔具店员");
            switch (Select(pc, "怎么办呢？", "", "送达货物", "购买50个鱼饵(30任务点)[每日限制：" + pc.AInt["鱼饵购买次数"] + "/3]","出售鱼类","没事"))
            {
                case 1:
                    if (pc.AInt["接受了搬运任务"] == 1 && CountItem(pc, 953200004) >= 1)
                    {
                        pc.AInt["西部渔场搬运完成"]++;
                        pc.AInt["接受了搬运任务"] = 0;
                        if (pc.AInt["西部渔场搬运完成"] < 10)
                            Say(pc, 131, "我订的货到了呀，谢谢哦。$R你有没有兴趣钓鱼呢？嘻嘻。", "渔具店员");
                        else if (pc.AInt["西部渔场搬运完成"] < 30)
                        {
                            Say(pc, 131, "哎呀，又是你呀，辛苦了。$R如你所见，这里还在发展阶段，$R你有没有兴趣来这里发展呢？", "渔具店员");
                        }
                        else
                        {
                            Say(pc, 131, "你每次送货都很快耶，虽然效率高是好事，$R但也要注意身体啊", "渔具店员");
                            Say(pc, 131, "悠闲一点也没什么不好的，嗯。", "渔具店员");
                        }
                        if (pc.AInt["西部渔场搬运完成"] >= 10 && pc.AInt["西部渔场搬运完成技能点获取"] != 1)
                        {
                            Say(pc, 131, "你获得了一个技能点。");
                            pc.AInt["西部渔场搬运完成技能点获取"] = 1;
                            pc.SkillPoint3 += 1;
                        }
                        TakeItem(pc, 953200004, 1);
                        TitleProccess(pc, 56, 1);
                        pc.Gold += Global.Random.Next(80000, 120000);
                        int rate = Global.Random.Next(80, 100);
                        uint exp = (uint)(pc.Level * pc.JobLevel3 * rate);
                        ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);
                        PlaySound(pc, 4012, false, 100, 50);
                        Wait(pc, 1000);
                        if (pc.AInt["西部渔场搬运完成"] < 30)
                            Say(pc, 131, "这里的出产的鱼都很鲜美哦，不去钓上几条么？嘻嘻", "渔具店员");
                        return;
                    }
                    break;
                case 2:
                    if(Select(pc,"确定要购买50个鱼饵吗？","","是的(50000G)[30任务点]","算了") == 1)
                    {
                        if (pc.QuestRemaining < 30)
                        {
                            Say(pc, 131, "你的任务点似乎不够？", "渔具店员");
                            return;
                        }
                        if(pc.AInt["鱼饵购买次数"] >= 3)
                        {
                            Say(pc, 131, "今天的鱼饵已经卖光啦，请明天再来吧。", "渔具店员");
                            return;
                        }
                        if(pc.Gold < 50000)
                        {
                            Say(pc, 131, "嗯？你似乎钱不够呢。", "渔具店员");
                            return;
                        }
                        pc.Gold -= 50000;
                        pc.AInt["鱼饵购买次数"]++;
                        pc.QuestRemaining -= 30;
                        pc.AInt["本周消耗任务点"] += 30;
                        GiveItem(pc, 110104900, 50);
                        MapClient.FromActorPC(pc).SendQuestPoints();
                    }
                    break;
                case 3:
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
            }
        }
        void SetBuylList(ActorGolem golem)
        {
            golem.BuyLimit = 100000000;

            if (SInt["西部渔价自动刷新"] != DateTime.Now.Hour)
            {
                SInt["西部渔价自动刷新"] = DateTime.Now.Hour;
                golem.BuyShop.Clear();
            }

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 10111400;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(1000, 3500);
                else
                    item.Price = (uint)Global.Random.Next(1000, 3000);
                item.Count = 9999;
                golem.BuyShop.Add(1, item);
            }
            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 10110300;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(2000, 6000);
                else
                    item.Price = (uint)Global.Random.Next(1500, 4000);
                item.Count = 9999;
                golem.BuyShop.Add(2, item);
            }
            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 10109900;
                if (Global.Random.Next(0, 100) <= 15)
                    item.Price = (uint)Global.Random.Next(15000, 50000);
                else
                    item.Price = (uint)Global.Random.Next(15000, 30000);
                item.Count = 9999;
                golem.BuyShop.Add(3, item);
            }
            return;
        }
    }
}

