
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
    public class S910000006 : Event
    {
        public S910000006()
        {
            this.EventID = 910000006;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000006) >= 1 && CountItem(pc, 950000017) >= 1)
            {
                TakeItem(pc, 950000017, 1);
                TakeItem(pc, 910000006, 1);
                奖励(pc);
            }
            else
            {
                Say(pc, 0, "似乎没有钥匙可以打开它哦");
            }
        }
        void 奖励(ActorPC pc)
        {
            套装奖励(pc);
            //GiveItem(pc, 910000007, 1);
            //GiveItem(pc, 910000008, 1);
            GiveItem(pc, 950000025, 1);
            int g = Global.Random.Next(50000, 200000);
            pc.Gold += g;
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 960000003;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//高级强化石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 960000004;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//高级强化石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 960000005;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//高级强化石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }

            if (Global.Random.Next(0, 100) < 101)
            {
                uint id = 953000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//东牢装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 101)
            {
                uint id = 953000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//东牢装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 101)
            {
                uint id = 953000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//东牢装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }

            if (Global.Random.Next(0, 100) < 101)
            {
                uint id = 953000021;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//海底装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 953000021;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//海底装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 60)
            {
                uint id = 953000021;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//海底装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 960000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 3);//项链石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 960000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 3);//武器石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 960000002;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 3);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                }
            }
        }
        void 套装奖励(ActorPC pc)
        {
            uint ItemID = 261085305;
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(ItemID);
            item.PictID = 100;
            //item.Refine = 1;
            //item.Refine_Vitality = 1;
            item.Str = (short)Global.Random.Next(0, 2);//STR随机(0 ~ 3)
            item.Int = (short)Global.Random.Next(0, 2);//INT随机(0 ~ 3)
            item.Dex = (short)Global.Random.Next(0, 2);//DEX随机(0 ~ 3)
            item.Mag = (short)Global.Random.Next(0, 2);//MAG随机(0 ~ 3)
            item.Vit = (short)Global.Random.Next(0, 2);//VIT随机(0 ~ 3)
            item.Agi = (short)Global.Random.Next(0, 2);//AGI随机(0 ~ 3)
            if (SagaLib.Global.Random.Next(0, 100) <= 50)
            {
                item.Name = "黎明之光之加护";
                item.Str = (short)Global.Random.Next(0, 3);//STR随机(0 ~ 3)
                item.Int = (short)Global.Random.Next(0, 3);//INT随机(0 ~ 3)
                item.Dex = (short)Global.Random.Next(0, 3);//DEX随机(0 ~ 3)
                item.Mag = (short)Global.Random.Next(0, 3);//MAG随机(0 ~ 3)
                item.Vit = (short)Global.Random.Next(0, 3);//VIT随机(0 ~ 3)
                item.Agi = (short)Global.Random.Next(0, 3);//AGI随机(0 ~ 3)
            }
            if (SagaLib.Global.Random.Next(0, 100) <= 20)
            {
                item.Name = "纯白之光之加护";
                item.Str = (short)Global.Random.Next(0, 5);//STR随机(0 ~ 3)
                item.Int = (short)Global.Random.Next(0, 5);//INT随机(0 ~ 3)
                item.Dex = (short)Global.Random.Next(0, 5);//DEX随机(0 ~ 3)
                item.Mag = (short)Global.Random.Next(0, 5);//MAG随机(0 ~ 3)
                item.Vit = (short)Global.Random.Next(0, 5);//VIT随机(0 ~ 3)
                item.Agi = (short)Global.Random.Next(0, 5);//AGI随机(0 ~ 3)
            }
            item.WeightUp = (short)Global.Random.Next(0, 150);//加算物理攻击随机(0 ~ 20)
            item.VolumeUp = (short)Global.Random.Next(0, 150);//加算魔法攻击随机(0 ~ 20)
            if (SagaLib.Global.Random.Next(0, 100) <= 35)
            {
                item.WeightUp = (short)Global.Random.Next(0, 300);//加算物理攻击随机(0 ~ 20)
                item.VolumeUp = (short)Global.Random.Next(0, 300);//加算魔法攻击随机(0 ~ 20)
            }
            item.HP = (short)Global.Random.Next(0, 100);//HP随机(0 ~ 500)
            if (SagaLib.Global.Random.Next(0, 100) <= 50)
                item.HP = (short)Global.Random.Next(0, 200);//HP随机(0 ~ 500)
            short atk = (short)Global.Random.Next(0, 2);
            item.Atk1 = atk;//乘算物理攻击力固定提升：2
            item.Atk2 = atk;//乘算物理攻击力固定提升：2
            item.Atk3 = atk;//乘算物理攻击力固定提升：2
            item.MAtk = (short)Global.Random.Next(0, 2);//乘算魔法攻击力固定提升：2
            if (Global.Random.Next(0, 100) <= 30)
            {
                atk = (short)Global.Random.Next(0, 3);
                item.Atk1 = atk;//乘算物理攻击力固定提升：2
                item.Atk2 = atk;//乘算物理攻击力固定提升：2
                item.Atk3 = atk;//乘算物理攻击力固定提升：2
                item.MAtk = (short)Global.Random.Next(0, 3);//乘算魔法攻击力固定提升：2
            }
            if (item.Str + item.Int + item.Dex + item.Mag + item.Vit + item.Agi >= 22 && item.MAtk + item.Atk1 > 3)
            {
                item.Name = "救世之光之加护";
            }

            if (pc.Party != null)
            {
                foreach (var m in pc.Party.Members)
                    if (m.Value.Online)
                        if (item.Name == "")
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.BaseData.name);
                        else
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【阿鲁卡多战利品】中获得了 " + item.Name);
            }
            GiveItem(pc, item);
        }
    }
}

