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
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S66000002 : Event
    {
        public S66000002()
        {
            this.EventID = 66000002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            Say(pc, 0, "呐...你有这附近产出的石头吗？", "海胆");
            switch (Select(pc, "怎么办呢？", "", "出售物品","任务控制台", "再见"))
            {
                case 1:
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 2:
                    HandleQuest(pc, 7);
                    break;
            }
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        void SetBuylList(ActorGolem golem)
        {
            if (SInt["海胆金钱"] == 0)
                SInt["海胆金钱"] = 50000000;
            if(SInt["海胆金钱重置周"]!= GetWeekOfYear(DateTime.Now))
            {
                SInt["海胆金钱"] = 50000000;
                SInt["海胆金钱重置周"] = GetWeekOfYear(DateTime.Now);
            }
            if (golem.BuyLimit > 0)
                SInt["海胆金钱"] = (int)golem.BuyLimit;

            int val = SInt["海胆金钱"] / 500000;
            float rate = val / 100f;

            golem.BuyLimit = (uint)SInt["海胆金钱"];

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 10015600;
                item.Price = (uint)Global.Random.Next((int)(1500 * rate), (int)(2000 * rate));
                item.Count = 50;
                golem.BuyShop.Add(1, item);
            }

            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 10014600;
                item.Price = (uint)Global.Random.Next((int)(500 * rate), (int)(800 * rate));
                item.Count = 200;
                golem.BuyShop.Add(2, item);
            }

            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 10015000;
                item.Price = (uint)Global.Random.Next((int)(2000 * rate), (int)(3000 * rate));
                item.Count = 50;
                golem.BuyShop.Add(3, item);
            }

            if (!golem.BuyShop.ContainsKey(4))
            {
                item = new GolemShopItem();
                item.ItemID = 10015701;
                item.Price = (uint)Global.Random.Next((int)(2500 * rate), (int)(4000 * rate));
                item.Count = 50;
                golem.BuyShop.Add(4, item);
            }
            if (!golem.BuyShop.ContainsKey(5))
            {
                item = new GolemShopItem();
                item.ItemID = 10015300;
                item.Price = (uint)Global.Random.Next((int)(10000 * rate), (int)(15000 * rate));
                item.Count = 10;
                golem.BuyShop.Add(5, item);
            }
            if (!golem.BuyShop.ContainsKey(6))
            {
                item = new GolemShopItem();
                item.ItemID = 10015706;
                item.Price = (uint)Global.Random.Next((int)(30000 * rate), (int)(35000 * rate));
                item.Count = 2;
                golem.BuyShop.Add(6, item);
            }
            if (!golem.BuyShop.ContainsKey(7))
            {
                item = new GolemShopItem();
                item.ItemID = 10015200;
                item.Price = (uint)Global.Random.Next((int)(45000 * rate), (int)(60000 * rate));
                item.Count = 1;
                golem.BuyShop.Add(7, item);
            }
            if (!golem.BuyShop.ContainsKey(8))
            {
                item = new GolemShopItem();
                item.ItemID = 10017300;
                item.Price = (uint)Global.Random.Next((int)(2800 * rate), (int)(5000 * rate));
                item.Count = 50;
                golem.BuyShop.Add(8, item);
            }
            if (!golem.BuyShop.ContainsKey(9))
            {
                item = new GolemShopItem();
                item.ItemID = 10005800;
                item.Price = (uint)Global.Random.Next((int)(300 * rate), (int)(500 * rate));
                item.Count = 50;
                golem.BuyShop.Add(9, item);
            }
            if (!golem.BuyShop.ContainsKey(10))
            {
                item = new GolemShopItem();
                item.ItemID = 10004350;
                item.Price = (uint)Global.Random.Next((int)(3000 * rate), (int)(5000 * rate));
                item.Count = 50;
                golem.BuyShop.Add(10, item);
            }
            return;
        }
    }
}

