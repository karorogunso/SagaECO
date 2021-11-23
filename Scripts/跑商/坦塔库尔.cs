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
namespace SagaScript.M30210000
{
    public class S60050006 : Event
    {
        public S60050006()
        {
            this.EventID = 60050006;
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
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            if(Golem== null && pc.Account.GMLevel <10)
            {
                SInt[pc.Name + "跑商异常"]++;
                return;
            }
            if (pc.AInt["跑商坦塔库尔Rate"] == 0)
                pc.AInt["跑商坦塔库尔Rate"] = 1000;
            OpenShopByList(pc, (uint)pc.AInt["跑商坦塔库尔Rate"], SagaDB.Npc.ShopType.ECoin, 10015500, 10023400, 10023501, 10023200, 10023100);
            pc.TInt["Ec商店开启"] = 1;
            /*ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            SetSellList(Golem);
            OpenGolemBuy(pc, Golem);*/
        }
        void SetSellList(ActorGolem golem)
        {
            GolemShopItem item = new GolemShopItem();
            if (!golem.SellShop.ContainsKey(6))
            {
                item = new GolemShopItem();
                item.ItemID = 10019303;
                item.Price = 1600;
                item.Count = 9999;
                golem.SellShop.Add(6, item);
            }
            if (!golem.SellShop.ContainsKey(7))
            {
                item = new GolemShopItem();
                item.ItemID = 10020551;
                item.Price =1600;
                item.Count = 9999;
                golem.SellShop.Add(7, item);
            }
            if (!golem.SellShop.ContainsKey(8))
            {
                item = new GolemShopItem();
                item.ItemID = 10019350;
                item.Price = 16000;
                item.Count = 9999;
                golem.SellShop.Add(8, item);
            }
            if (!golem.SellShop.ContainsKey(9))
            {
                item = new GolemShopItem();
                item.ItemID = 10018209;
                item.Price = 8500;
                item.Count = 9999;
                golem.SellShop.Add(9, item);
            }
            if (!golem.SellShop.ContainsKey(10))
            {
                item = new GolemShopItem();
                item.ItemID = 10011600;
                item.Price = 3500;
                item.Count = 9999;
                golem.SellShop.Add(10, item);
            }
        }
    }
}

