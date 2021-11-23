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
    public class S66000004 : Event
    {
        public S66000004()
        {
            this.EventID = 66000004;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "嗯？$R你说我们前几天才见过面？$R是错觉啦！", "外地来的商人");
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            switch (Select(pc, "怎么办呢？", "", "购买活动用品（镰刀，材料）","出售货物[收购时间每小时刷新]", "离开"))
            {
                case 1:
                    pc.TInt["ShopType"] = 0;
                    OpenShopByList(pc, 1100, SagaDB.Npc.ShopType.None, 160051500);
                    break;
                case 2:
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 3:
                    break;
            }
        }
        void SetBuylList(ActorGolem golem)
        {
            golem.BuyLimit = 100000000;

            if (SInt["草莓自动刷新"] != DateTime.Now.Hour)
            {
                SInt["草莓自动刷新"] = DateTime.Now.Hour;
                golem.BuyShop.Clear();
            }

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 110096400;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(300, 900);
                else
                    item.Price = (uint)Global.Random.Next(300, 1300);
                item.Count = 9999;
                golem.BuyShop.Add(1, item);
            }
            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 110096401;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(500, 1200);
                else
                    item.Price = (uint)Global.Random.Next(500, 1500);
                item.Count = 9999;
                golem.BuyShop.Add(2, item);
            }
            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 110096402;
                if (Global.Random.Next(0, 100) <= 30)
                    item.Price = (uint)Global.Random.Next(900, 2500);
                else
                    item.Price = (uint)Global.Random.Next(900, 2000);
                item.Count = 9999;
                golem.BuyShop.Add(3, item);
            }
        }
    }
}

