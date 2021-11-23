
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000010 : Event
    {
        public S80000010()
        {
            this.EventID = 80000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "天灵灵地灵灵，$R欧气聚我手心，$R非洲黑雾退散也……$R$R请问你要什么服务呢？", "卡片大师");
            int set = Select(pc, "请选择服务", "", "为装备扩展插卡槽", "解锁已经锁定卡片的装备", "卡片融合", "制作虹石", "离开");
            if (set == 1)
                确定卡槽装备(pc);
            if (set == 2)
                解锁卡槽(pc);
            if (set == 3)
                卡片融合(pc);
            if (set == 4)
                Synthese(pc, 2009, 5);
        }
        void 卡片融合(ActorPC pc)
        {
            SagaDB.Item.Item Card = new SagaDB.Item.Item();
            string name = "无";
            
            List<SagaDB.Item.Item> Cards = new List<SagaDB.Item.Item>();
            SagaDB.Iris.IrisCard card = new SagaDB.Iris.IrisCard();
            List<string> names = new List<string>();
            foreach (var item in pc.Inventory.Items[ContainerType.BODY])
            {
                if (item.BaseData.itemType == ItemType.IRIS_CARD && item.Stack != 0)
                {
                    if (SagaDB.Iris.IrisCardFactory.Instance.Items.ContainsKey(item.BaseData.id))
                    {
                        card = SagaDB.Iris.IrisCardFactory.Instance.Items[item.BaseData.id];
                        if (card.NextCard != 0 && CountItem(pc, item.ItemID) >= 2)
                        {
                            Cards.Add(item);
                            names.Add(item.BaseData.name);
                        }
                    }
                }
            }
            if (names.Count < 1)
            {
                Say(pc, 0, "不好意思...$R$R您似乎没有可以合成的卡片", "卡片大师");
                return;
            }
            names.Add("离开");
            int set = Select(pc, "请选择要合成卡片的部分", "", names.ToArray());
            if (set == names.Count) return;
            Card = Cards[set - 1];
            card = SagaDB.Iris.IrisCardFactory.Instance.Items[Card.BaseData.id];
            Dictionary<uint, byte> stuffs = 获取合成材料(card);
            uint cost = 获取合成费用(card);
            stuffs.Add(Card.ItemID, 2);
            string s = "";
            foreach (var item in stuffs)
            {
                SagaDB.Item.Item i = ItemFactory.Instance.GetItem(item.Key);
                s += i.BaseData.name + " " + item.Value.ToString() + "个，$R";
            }
            Say(pc, 131, "合成这个卡槽需要：$R$R" + s + "工费："+ cost + "G", "卡片大师");
            int set2 = Select(pc, "确定合成 " + Card.BaseData.name + " ?", "", "是的，我要合成它", "还是算了");
            if (set2 == 1)
            {
                if (pc.Gold < cost)
                {
                    Say(pc, 131, "你似乎..没带够钱呢", "卡片大师");
                    return;
                }
                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "你似乎..材料没带够呢", "卡片大师");
                        return;
                    }
                }
                foreach (var item in stuffs)
                    TakeItem(pc, item.Key, item.Value);
                pc.Gold -= cost;
                GiveItem(pc, card.NextCard, 1);
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC(pc);
                client.SendSystemMessage("卡片合成成功了！");
                ShowEffect(pc, 5143);
            }
            else return;
            return;
        }
        void 解锁卡槽(ActorPC pc)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    if (item.Locked)
                    {
                        string ss = item.BaseData.name;
                        if (item.BaseData.name.Length > 9)
                            ss = item.BaseData.name.Substring(0, 10);
                        name = "【" + ss + "】卡槽数：" + item.CurrentSlot.ToString();
                        Equips.Add(item);
                        names.Add(name);
                    }
                }
            }
            if (names.Count < 1)
            {
                Say(pc, 0, "不好意思...$R$R您似乎没有装备已锁定的装备", "卡片大师");
                return;
            }
            names.Add("离开");
            int set = Select(pc, "请选择要解锁卡槽的部位", "", names.ToArray());
            if (set == names.Count) return;
            Equip = Equips[set - 1];
            Say(pc, 131, "解锁卡槽需要：$R$R[卡片解锁钥匙] 1把，$R100,000G", "卡片大师");
            if (Select(pc, "确定要解锁 " + Equip.BaseData.name + "吗", "", "是的，解锁", "还是算了") == 1)
            {
                if (pc.Gold < 100000)
                {
                    Say(pc, 131, "你似乎..没带够钱呢", "卡片大师");
                    return;
                }
                if (CountItem(pc, 950000054) < 1)
                {
                    Say(pc, 131, "你似乎..没有钥匙呢", "卡片大师");
                    return;
                }
                pc.Gold -= 100000;
                TakeItem(pc, 950000054, 1);
                ShowEffect(pc, 5153);
                Equip.Locked = false;
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC(pc);
                client.SendItemInfo(Equip);
                client.SendSystemMessage(Equip.BaseData.name + " 的锁定解除了");
            }
        }
        void 确定卡槽装备(ActorPC pc)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 9)
                        ss = item.BaseData.name.Substring(0, 10);
                    name = "【" + ss + "】卡槽数：" + item.CurrentSlot.ToString();
                    Equips.Add(item);
                    names.Add(name);
                }
            }
            if (names.Count < 1)
            {
                Say(pc, 0, "不好意思...$R$R您似乎没有在插卡部分穿戴装备呢。", "卡片大师");
                return;
            }
            names.Add("离开");
            int set2 = Select(pc, "请选择要扩展卡槽的部位", "", names.ToArray());
            if (set2 == names.Count) return;
            Equip = Equips[set2 - 1];
            if (Equip.CurrentSlot >= 5)
            {
                Say(pc, 131, "非常抱歉，目前只能为你开到5洞", "卡片大师");
                return;
            }
            Dictionary<uint, byte> stuffs = 获取开洞材料(Equip);
            string s = "";
            foreach (var item in stuffs)
            {
                SagaDB.Item.Item i = ItemFactory.Instance.GetItem(item.Key);
                s += i.BaseData.name + " " + item.Value.ToString() + "个，$R";
            }
            uint rate = 获取开洞几率(Equip);
            uint gr = 获取开洞费用(Equip);
            Say(pc, 131, "扩展这个装备的卡槽需要：$R$R" + s + "工费："+gr+"G，$R$R成功率：" + (rate / 100).ToString() + "％", "卡片大师");
            int set = Select(pc, "确定是否要扩展呢？", "", "是的，我要扩展", "还是算了");
            if (set == 1)
            {
                if (pc.Gold < gr)
                {
                    Say(pc, 131, "你似乎..没带够钱呢", "卡片大师");
                    return;
                }
                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "你似乎..材料没带够呢", "卡片大师");
                        return;
                    }
                }
                foreach (var item in stuffs)
                    TakeItem(pc, item.Key, item.Value);
                pc.Gold -= gr;
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC(pc);
                int r = Global.Random.Next(0, 10000);
                if (r < rate)
                {
                    Equip.CurrentSlot++;
                    client.SendSystemMessage("扩展卡槽成功了！当前卡槽数：" + Equip.CurrentSlot.ToString());
                    ShowEffect(pc, 5145);
                    client.SendItemInfo(Equip);
                }
                else
                {
                    ShowEffect(pc, 5146);
                    client.SendSystemMessage("失败了！！！当前卡槽数：" + Equip.CurrentSlot.ToString());
                }
            }
            else return;
            return;
        }
        uint 获取合成费用(SagaDB.Iris.IrisCard card)
        {
            uint g = 60000;

            switch (card.Rarity)
            {
                case SagaDB.Iris.Rarity.Common:
                case SagaDB.Iris.Rarity.Uncommon://装备0洞时
                    switch (card.Rank)
                    {
                        case 0:
                            g = 20000;
                            break;
                        case 1:
                            g = 50000;
                            break;
                        case 2:
                            g = 100000;
                            break;
                        case 3:
                            g = 150000;
                            break;
                    }
                    break;
                case SagaDB.Iris.Rarity.Rare:
                    switch (card.Rank)
                    {
                        case 0:
                            g = 50000;
                            break;
                        case 1:
                            g = 120000;
                            break;
                        case 2:
                            g = 200000;
                            break;
                        case 3:
                            g = 350000;
                            break;
                    }
                    break;
                case SagaDB.Iris.Rarity.SuperRare:
                    switch (card.Rank)
                    {
                        case 0:
                            g = 200000;
                            break;
                        case 1:
                            g = 400000;
                            break;
                        case 2:
                            g = 800000;
                            break;
                        case 3:
                            g = 1200000;
                            break;
                    }
                    break;

            }
            return g;
        }
        Dictionary<uint, byte> 获取合成材料(SagaDB.Iris.IrisCard card)
        {
            Dictionary<uint, byte> stuffs = new Dictionary<uint, byte>();
            switch (card.Rarity)
            {
                case SagaDB.Iris.Rarity.Common:
                case SagaDB.Iris.Rarity.Uncommon://装备0洞时
                    switch (card.Rank)
                    {
                        case 0:
                            stuffs.Add(10071200, 1);//该道具需求1个
                            break;
                        case 1:
                            stuffs.Add(10071200, 3);//该道具需求1个
                            break;
                        case 2:
                            stuffs.Add(10071200, 5);//该道具需求1个
                            break;
                        case 3:
                            stuffs.Add(10071200, 7);//该道具需求1个
                            break;
                    }
                    break;
                case SagaDB.Iris.Rarity.Rare:
                    switch (card.Rank)
                    {
                        case 0:
                            stuffs.Add(10071300, 1);//该道具需求1个
                            break;
                        case 1:
                            stuffs.Add(10071300, 3);//该道具需求1个
                            break;
                        case 2:
                            stuffs.Add(10071300, 5);//该道具需求1个
                            break;
                        case 3:
                            stuffs.Add(10071300, 7);//该道具需求1个
                            break;
                    }
                    break;
                case SagaDB.Iris.Rarity.SuperRare:
                    switch (card.Rank)
                    {
                        case 0:
                            stuffs.Add(10071400, 1);//该道具需求1个
                            break;
                        case 1:
                            stuffs.Add(10071400, 3);//该道具需求1个
                            break;
                        case 2:
                            stuffs.Add(10071400, 5);//该道具需求1个
                            break;
                        case 3:
                            stuffs.Add(10071400, 7);//该道具需求1个
                            break;
                    }
                    break;

            }
            return stuffs;
        }
        Dictionary<uint, byte> 获取开洞材料(SagaDB.Item.Item equip)
        {
            Dictionary<uint, byte> stuffs = new Dictionary<uint, byte>();
            switch (equip.CurrentSlot)
            {
                case 0://装备0洞时
                    stuffs.Add(950000050, 1);//该道具需求1个
                    stuffs.Add(950000051, 1);//该道具需求1个
                    break;
                case 1:
                    stuffs.Add(950000050, 1);
                    stuffs.Add(950000051, 1);
                    break;
                case 2:
                    stuffs.Add(950000050, 2);
                    stuffs.Add(950000051, 2);
                    break;
                case 3:
                    stuffs.Add(950000050, 3);
                    stuffs.Add(950000052, 1);
                    break;
                case 4:
                    stuffs.Add(950000050, 4);
                    stuffs.Add(950000052, 2);
                    break;
                case 5:
                    stuffs.Add(950000050, 5);
                    stuffs.Add(950000052, 3);
                    break;
                case 6:
                    stuffs.Add(950000050, 6);
                    stuffs.Add(950000052, 4);
                    break;
                case 7:
                    stuffs.Add(950000050, 7);
                    stuffs.Add(950000052, 5);
                    break;
                case 8:
                    stuffs.Add(950000050, 8);
                    stuffs.Add(950000053, 1);
                    break;
                case 9:
                    stuffs.Add(950000050, 9);
                    stuffs.Add(950000053, 2);
                    break;
            }
            return stuffs;
        }
        protected uint 获取开洞费用(SagaDB.Item.Item equip)
        {
            uint[] rates = new uint[10] { 250000, 750000, 1500000, 5000000, 10000000,
                                         20000000, 30000000, 40000000, 50000000, 60000000};
            return rates[equip.CurrentSlot];
        }
        protected uint 获取开洞几率(SagaDB.Item.Item equip)
        {
            uint[] rates = new uint[10] { 10000, 10000, 10000, 10000, 10000,
                                         10000, 10000, 10000, 10000, 10000};
            return rates[equip.CurrentSlot];
        }
    }
}

