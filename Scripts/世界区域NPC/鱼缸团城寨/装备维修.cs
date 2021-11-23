
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
    public class S80000006 : Event
    {
        public S80000006()
        {
            this.EventID = 80000006;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗯哼，$R小姑娘,$R你的衣服破洞洞了呢$R$R想不想让我来帮你修补修补呢？","修理师");
            switch(Select(pc,"你找这个变态要干什么","","我要修理我的装备","拒绝"))
            {
                case 1:
                    SagaDB.Item.Item Equip = new SagaDB.Item.Item();
                    List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
                    List<string> names = new List<string>();
                    string name = "";
                    int basecost = 0;//价格基数，损失3以下 10000一点，3以上 20000一点。
                    foreach (var item in pc.Inventory.Equipments)
                    {
                        if (!Equips.Contains(item.Value))
                        {
                            if (item.Value.Durability < item.Value.maxDurability)
                            {
                                name = "【" + item.Value.BaseData.name + "】持久度：" + item.Value.Durability.ToString() + "/" + item.Value.maxDurability.ToString();
                                Equips.Add(item.Value);
                                names.Add(item.Value.BaseData.name);
                            }
                        }
                    }
                    foreach (var item in pc.Inventory.Items[ContainerType.BODY])
                    {
                        if (!Equips.Contains(item))
                        {
                            if (item.Durability < item.maxDurability)
                            {
                                name = "【" + item.BaseData.name + "】持久度：" + item.Durability.ToString() + "/" + item.maxDurability.ToString();
                                Equips.Add(item);
                                names.Add(item.BaseData.name);
                            }
                        }
                    }
                    if (names.Count < 1)
                    {
                        Say(pc, 0, "不好意思...$R$R您似乎身上没有坏的装备呢。", "修理师");
                        return;
                    }
                    int set = Select(pc, "怎么办呢", "", "我要单独修理", "修理身上全部已损坏的物品", "离开");
                    if (set == 1)
                    {
                        Equip = Equips[Select(pc, "请选择要修理的部位", "", names.ToArray()) - 1];
                        int dif = Equip.maxDurability - Equip.Durability;
                        int cost = basecost;//价格计算
                        if (dif < 3)
                            cost = (Equip.maxDurability - Equip.Durability) * basecost;
                        else
                            cost = (Equip.maxDurability - Equip.Durability) * basecost * 2;
                        switch (Select(pc, "是否修理【" + name + "】?", "", "进行修理 (当前费用:" + cost.ToString() + ")", "离开"))
                        {
                            case 1:
                                if (pc.Gold >= cost)
                                {
                                    pc.Gold -= cost;
                                }
                                else
                                {
                                    Say(pc, 131, "唔。。你似乎没带够钱呢", "强化店");
                                    return;
                                }
                                PlaySound(pc, 2863, false, 100, 50);
                                Equip.Durability = Equip.maxDurability;
                                Say(pc, 131, "【" + Equip.BaseData.name + "】$R$R" + "修理完成了！");
                                return;
                        }
                    }
                    else if (set == 2)
                    {
                        foreach (var item in Equips)
                        {
                            PlaySound(pc, 2863, false, 100, 50);
                            item.Durability = item.maxDurability;
                            Say(pc, 131, "【" + item.BaseData.name + "】$R$R" + "修理完成了！");
                        }
                    }
                    break;
                case 2:
                    break;
            }
        }
    }
}

