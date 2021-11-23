
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S60000136 : Event
    {
        public S60000136()
        {
            this.EventID = 60000136;
        }

        public override void OnEvent(ActorPC pc)
        {
            int count = 0;
            string Sname = "";
            foreach (var item in pc.Adict["魔物画板兑换记录"])
            {
                if (item.Value == 1)
                {
                    count++;
                    Sname += item.Key + ",";
                }
            }
            Say(pc, 0, "你好呀，冒险家！$R$R如果你带来魔物画板，$R我可以帮你制作成家具哦。$R$R※制作从来没制作的画板可以获得1000CP$R※您已制作了 " + count + " 个不同的画板了。", "熊熊画师");
            switch (Select(pc, "怎么办呢?", "", "制作怪物模型(家具)[需要白的怪物模型]", "查看已做过的怪物模型", "使用『画板兑换代币』", "离开"))
            {
                case 1:
                    List<SagaDB.Item.Item> Items = new List<SagaDB.Item.Item>();
                    List<string> Names = new List<string>();
                    foreach (var i in pc.Inventory.Items[ContainerType.BODY])
                    {
                        if (i.ItemID == 10020758)
                        {
                            uint pictID = i.PictID;
                            string name = SagaDB.Mob.MobFactory.Instance.GetMobData(i.PictID).name + " 的画板";
                            if (pictID != 0)
                            {
                                Items.Add(i);
                                if (pc.Adict["魔物画板兑换记录"][SagaDB.Mob.MobFactory.Instance.GetMobData(i.PictID).name] != 1)
                                    name += "[首次可获得CP]";
                                Names.Add(name);
                            }
                        }
                    }
                    if (Items.Count < 1)
                    {
                        Say(pc, 131, "你似乎没有画板哦。", "熊熊画师");
                        return;
                    }
                    Names.Add("放弃");
                    if (Items.Count > 15)
                    {
                        Say(pc, 131, "你带了的画板太多了！", "熊熊画师");
                        return;
                    }
                    int set = Select(pc, "※请选择需要进行【制作模型】的魔物画板", "", Names.ToArray());
                    if (set == Names.Count) return;

                    uint 模型slot = 0;
                    SagaDB.Item.Item 模型 = null;
                    foreach (var i in pc.Inventory.Items[ContainerType.BODY])
                    {
                        if (i.ItemID == 10017151)
                        {
                            if (i.PictID == 0)
                            {
                                模型slot = i.Slot;
                                模型 = i;
                                break;
                            }
                        }
                    }
                    if (模型slot == 0 || 模型 == null)
                    {
                        Say(pc, 131, "你似乎没有可以再使用的模型了哦！", "熊熊画师");
                        return;
                    }
                    SagaDB.Item.Item es = Items[set - 1];
                    if (es.ItemID != 10020758) return;
                    if (es.PictID == 0) return;
                    string name2 = SagaDB.Mob.MobFactory.Instance.GetMobData(es.PictID).name;
                    if (pc.Adict["魔物画板兑换记录"][name2] != 1)
                    {
                        pc.CP += 1000;
                        pc.Adict["魔物画板兑换记录"][name2] = 1;
                        Say(pc, 131, "首次使用 " + name2 + " 的模型制作家具，获得1000CP。", "熊熊画师");
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("首次使用 " + name2 + " 的模型制作家具，获得1000CP。");
                        TitleProccess(pc, 67, 1);
                        TitleProccess(pc, 68, 1);
                        TitleProccess(pc, 69, 1);
                    }
                    模型.PictID = es.PictID;
                    TakeItemBySlot(pc, es.Slot, 1);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(模型);
                    Say(pc, 131, "制作好啦！$R欢迎再来哦~", "熊熊画师");
                    break;
                case 2:
                    if (Sname != "")
                        Say(pc, 131, Sname);
                    else
                        Say(pc, 131, "你好像没有制作过任何家具模型过哦。", "熊熊画师");
                    break;
                case 3:
                    if(CountItem(pc, 950000015) >= 1)
                    {
                        Say(pc, 131, "啊，我想要那个代币！$R你能给我吗？$R$R我用我刚刚画好的画板和你交换！$R$R※随机获得一种魔物的画板", "熊熊画师");
                        if(Select(pc,"怎么办呢？","", "给他『画板兑换代币』","离开") == 1)
                        {
                            if (CountItem(pc, 950000015) >= 1)
                            {
                                try
                                {
                                    uint mobID = 0;
                                    uint[] mobs = new uint[SagaDB.Mob.MobFactory.Instance.Mobs.Count];
                                    SagaDB.Mob.MobFactory.Instance.Mobs.Keys.CopyTo(mobs, 0);
                                    while (!SagaDB.Partner.PartnerFactory.Instance.PartnerPictList.Contains(mobID) || mobID == SagaDB.Mob.MobFactory.Instance.Mobs[mobID].pictid)
                                    {
                                        int ran = Global.Random.Next(0, mobs.Length - 1);
                                        mobID = mobs[ran];
                                    }

                                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(10020758);
                                    item.Stack = 1;
                                    item.PictID = mobID;
                                    GiveItem(pc, item);

                                    TakeItem(pc, 950000015, 1);
                                    Say(pc, 131, "这是我毕生的杰作！$R$R你一定会喜欢它的。", "熊熊画师");
                                    PlaySound(pc, 4004, false, 100, 50);
                                }
                                catch(Exception ex)
                                {
                                    SagaLib.Logger.ShowError(ex);
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}

