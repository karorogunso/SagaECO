
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
    public class S910000121 : Event
    {
        public S910000121()
        {
            this.EventID = 910000121;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000121) >= 1)
            {
                TakeItem(pc, 910000121, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 950000025, 1);
            int g = Global.Random.Next(5000, 15000);
            pc.Gold += g;

            if (Global.Random.Next(0, 100) < 101)
            {
                uint id = 953000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 2);//装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 953000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//装备箱
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }

            if (Global.Random.Next(0, 100) < 60)
            {
                uint id = 960000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//项链石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 60)
            {
                uint id = 960000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//武器石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 60)
            {
                uint id = 960000002;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }

            int rate = Global.Random.Next(0, 300);
            if(rate < 5)
            {
                uint id = 950000105;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//封印的凶暴的魔物们篇卡片x10
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if(rate < 35)
            {
                uint id = 950000104;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//封印的凶暴的魔物们篇卡片x5
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            else
            {
                uint id = 950000103;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//封印的凶暴的魔物们篇卡片x1
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }

            int rate2 = Global.Random.Next(0, 150);
            if(rate == 1)
            {
                uint id = 65000200;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//攻撃の護符＋３
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if(rate == 2)
            {
                uint id = 65000520;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//魔法攻撃の護符＋３
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if (rate > 10 && rate < 15)
            {
                uint id = 65000100;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//攻撃の護符＋２
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if (rate > 15 && rate < 20)
            {
                uint id = 65000100;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//魔法攻撃の護符＋２
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【夺魂者战利品】中获得了 " + item.BaseData.name);
                }
            }
        }
    }
}

