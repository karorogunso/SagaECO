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
    public class S66000003 : Event
    {
        public S66000003()
        {
            this.EventID = 66000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "哎呀..$R这里是哪里呀，$R从来没来过的地方呢，$R好热闹呀。", "外地来的商人");
            Say(pc, 0, "但是听说这附近马上会长满蓝莓！$R$R我是做蓝莓生意的，$R所以如果你有蓝莓酱的话，$R可以卖给我哦！", "外地来的商人");
            byte count = 0;
            string lastip = "";
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                if (i.Character.Account.LastIP == pc.Account.LastIP && i.Character.Account.GMLevel < 20)
                    count++;
            if (count > 1)
            {
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            switch (Select(pc, "怎么办呢？", "", "购买活动用品（镰刀，材料）","出售货物[收购时间每小时刷新]", "兑换种植奖励", "交谈", "离开"))
            {
                case 1:
                    OpenShopByList(pc, 1100, SagaDB.Npc.ShopType.None, 160051500, 10033200, 10001800, 10045300);
                    break;
                case 2:
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 3:
                    bool 兑换了东西 = false;
                    if (pc.AInt["个人蓝莓种植"] >= 50 && pc.AInt["个人蓝莓种植50兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 953000000, 3);
                        pc.AInt["个人蓝莓种植50兑换"] = 1;
                    }
                    if (pc.AInt["个人蓝莓种植"] >= 100 && pc.AInt["个人蓝莓种植100兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 910000069, 1);
                        pc.AInt["个人蓝莓种植100兑换"] = 1;
                    }
                    if (pc.AInt["个人蓝莓种植"] >= 300 && pc.AInt["个人蓝莓种植300兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 950000027, 1);
                        pc.AInt["个人蓝莓种植300兑换"] = 1;
                    }
                    if (pc.AInt["个人蓝莓种植"] >= 500 && pc.AInt["个人蓝莓种植500兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 910000116, 1);
                        pc.AInt["个人蓝莓种植500兑换"] = 1;
                    }
                    if (pc.AInt["个人蓝莓种植"] >= 1000 && pc.AInt["个人蓝莓种植1000兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 10149500, 1);
                        pc.AInt["个人蓝莓种植1000兑换"] = 1;
                    }
                    if (pc.AInt["个人蓝莓种植"] >= 3000 && pc.AInt["个人蓝莓种植3000兑换"] != 1)
                    {
                        兑换了东西 = true;
                        GiveItem(pc, 950000028, 1);
                        pc.AInt["个人蓝莓种植3000兑换"] = 1;
                    }
                    if (兑换了东西)
                    {
                        Say(pc, 131, "哎呀，$R刚刚来了个绿头发的姑娘，$R叫我把这些东西给你。", "外地来的商人");
                        return;
                    }
                    else
                    {
                        Say(pc, 131, "嗯？兑换？那是什么？", "外地来的商人");
                        return;
                    }
                case 4:
                    Say(pc, 131, "哎呀...$R这里真是好地方呢，$R但我6月12号的凌晨就要离开这里了。$R$R不知道以后还会不会有机会再来呢", "外地来的商人");
                    break;
                case 5:
                    break;
            }
        }
        void SetBuylList(ActorGolem golem)
        {
            golem.BuyLimit = 100000000;

            if (SInt["蓝莓自动刷新"] != DateTime.Now.Hour)
            {
                SInt["蓝莓自动刷新"] = DateTime.Now.Hour;
                golem.BuyShop.Clear();
            }

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 110098900;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(3000, 5000);
                else
                    item.Price = (uint)Global.Random.Next(3000, 8000);
                item.Count = 9999;
                golem.BuyShop.Add(1, item);
            }
            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 110098901;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(5000, 13000);
                else
                    item.Price = (uint)Global.Random.Next(5000, 10000);
                item.Count = 9999;
                golem.BuyShop.Add(2, item);
            }
            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 110098902;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(10000, 20000);
                else
                    item.Price = (uint)Global.Random.Next(10000, 15000);
                item.Count = 9999;
                golem.BuyShop.Add(3, item);
            }
            if (!golem.BuyShop.ContainsKey(4))
            {
                item = new GolemShopItem();
                item.ItemID = 10033200;
                item.Price = 450;
                item.Count = 9999;
                golem.BuyShop.Add(4, item);
            }
            if (!golem.BuyShop.ContainsKey(5))
            {
                item = new GolemShopItem();
                item.ItemID = 10045300;
                item.Price = 760;
                item.Count = 9999;
                golem.BuyShop.Add(5, item);
            }
            if (!golem.BuyShop.ContainsKey(6))
            {
                item = new GolemShopItem();
                item.ItemID = 10001800;
                item.Price = 540;
                item.Count = 9999;
                golem.BuyShop.Add(6, item);
            }
        }
    }
}

