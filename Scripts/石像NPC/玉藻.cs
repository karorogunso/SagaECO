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
    public class S66000001 : Event
    {
        public S66000001()
        {
            this.EventID = 66000001;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            Say(pc, 0, "那个...$R$R你能卖我点花吗？", "玉藻");
            Say(pc, 0, "我想送给清姬姐姐...", "清姬");
            switch (Select(pc, "怎么办呢？", "", "出售物品", "再见"))
            {
                case 1:
                    switch (Global.Random.Next(0, 3))
                    {
                        case 0:
                            SkillHandler.Instance.ActorSpeak(Golem, "谢、谢谢你...");
                            break;
                        case 1:
                            SkillHandler.Instance.ActorSpeak(Golem, "不可以卖我奇怪的东西哦");
                            break;
                    }
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 3:
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
            if (SInt["玉藻金钱"] == 0)
                SInt["玉藻金钱"] = 50000000;
            if(SInt["玉藻金钱重置周"]!= GetWeekOfYear(DateTime.Now))
            {
                SInt["玉藻金钱"] = 50000000;
                SInt["玉藻金钱重置周"] = GetWeekOfYear(DateTime.Now);
            }
            if (golem.BuyLimit > 0)
                SInt["玉藻金钱"] = (int)golem.BuyLimit;

            int val = SInt["玉藻金钱"] / 500000;
            float rate = val / 100f;

            golem.BuyLimit = (uint)SInt["玉藻金钱"];

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 10005390;
                item.Price = (uint)Global.Random.Next((int)(2000 * rate), (int)(2500 * rate));
                item.Count = 60;
                golem.BuyShop.Add(1, item);
            }

            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 10005300;
                item.Price = (uint)Global.Random.Next((int)(800 * rate), (int)(1000 * rate));
                item.Count = 200;
                golem.BuyShop.Add(2, item);
            }

            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 10005200;
                item.Price = (uint)Global.Random.Next((int)(16000 * rate), (int)(19500 * rate));
                item.Count = 5;
                golem.BuyShop.Add(3, item);
            }

            if (!golem.BuyShop.ContainsKey(4))
            {
                item = new GolemShopItem();
                item.ItemID = 10025056;
                item.Price = (uint)Global.Random.Next((int)(50000 * rate), (int)(65000 * rate));
                item.Count = 1;
                golem.BuyShop.Add(4, item);
            }

            if (!golem.BuyShop.ContainsKey(5))
            {
                item = new GolemShopItem();
                item.ItemID = 10043204;
                item.Price = (uint)Global.Random.Next((int)(900 * rate), (int)(1300 * rate));
                item.Count = 200;
                golem.BuyShop.Add(5, item);
            }

            if (!golem.BuyShop.ContainsKey(6))
            {
                item = new GolemShopItem();
                item.ItemID = 10031159;
                item.Price = (uint)Global.Random.Next((int)(1000 * rate), (int)(1500 * rate));
                item.Count = 200;
                golem.BuyShop.Add(6, item);
            }

            if (!golem.BuyShop.ContainsKey(7))
            {
                item = new GolemShopItem();
                item.ItemID = 10006900;
                item.Price = (uint)Global.Random.Next((int)(650 * rate), (int)(1200 * rate));
                item.Count = 200;
                golem.BuyShop.Add(7, item);
            }

            if (!golem.BuyShop.ContainsKey(8))
            {
                item = new GolemShopItem();
                item.ItemID = 10007001;
                item.Price = (uint)Global.Random.Next((int)(31000 * rate), (int)(36000 * rate));
                item.Count = 2;
                golem.BuyShop.Add(8, item);
            }
            return;
        }
    }
}

