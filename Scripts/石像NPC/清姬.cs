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
    public class S66000000 : Event
    {
        public S66000000()
        {
            this.EventID = 66000000;
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        public override void OnEvent(ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            Say(pc, 0, "强、强盗！！？？", "清姬");
            Say(pc, 0, "不..不是吗，那你要干什么呢QAQ", "清姬");
            switch (Select(pc, "怎么办呢？", "", "出售物品", "再见"))
            {
                case 1:
                    switch (Global.Random.Next(0, 3))
                    {
                        case 0:
                            SkillHandler.Instance.ActorSpeak(Golem, "你！你要干什么！！？");
                            break;
                        case 1:
                            SkillHandler.Instance.ActorSpeak(Golem, "！！！！！！！！！！！");
                            break;
                    }
                    SetBuylList(Golem);
                    OpenGolemBuy(pc, Golem);
                    break;
                case 3:
                    break;
            }
        }
        void SetBuylList(ActorGolem golem)
        {
            if (SInt["清姬金钱"] == 0)
                SInt["清姬金钱"] = 50000000;
            if(SInt["清姬金钱重置周"]!= GetWeekOfYear(DateTime.Now))
            {
                SInt["清姬金钱"] = 50000000;
                SInt["清姬金钱重置周"] = GetWeekOfYear(DateTime.Now);
            }
            if (golem.BuyLimit > 0)
                SInt["清姬金钱"] = (int)golem.BuyLimit;

            int val = SInt["清姬金钱"] / 500000;
            float rate = val / 100f;

            golem.BuyLimit = (uint)SInt["清姬金钱"];

            GolemShopItem item = new GolemShopItem();
            if (!golem.BuyShop.ContainsKey(1))
            {
                item = new GolemShopItem();
                item.ItemID = 10016300;
                item.Price = (uint)Global.Random.Next((int)(1200 * rate), (int)(1900 * rate));
                item.Count = 200;
                golem.BuyShop.Add(1, item);
            }

            if (!golem.BuyShop.ContainsKey(2))
            {
                item = new GolemShopItem();
                item.ItemID = 10016400;
                item.Price = item.Price = (uint)Global.Random.Next((int)(1900 * rate),(int)(2550 * rate));
                item.Count = 20;
                golem.BuyShop.Add(2, item);
            }

            if (!golem.BuyShop.ContainsKey(3))
            {
                item = new GolemShopItem();
                item.ItemID = 10016350;
                item.Price = (uint)Global.Random.Next((int)(50000 * rate),(int)(75000 * rate));
                item.Count = 1;
                golem.BuyShop.Add(3, item);
            }
            return;
        }
    }
}

