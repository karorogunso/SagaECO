
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
    public class S953000009 : Event
    {
        public S953000009()
        {
            this.EventID = 953000009;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 953000009) >= 1)
            {
                TakeItem(pc, 953000009, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 950000025, 1);
            int g = Global.Random.Next(20000, 45000);
            pc.Gold += g;

            if (Global.Random.Next(0, 100) < 10)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 8)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 8)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 8)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 8)
            {
                uint id = 910000116;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//CP1000
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 953000021;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//副本材料箱子
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }

            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 960000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//项链石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 960000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//武器石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 960000002;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }

            int rate2 = Global.Random.Next(0, 200);
            if (rate2 == 1)
            {
                uint id = 65000200;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//攻撃の護符＋３
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if (rate2 == 2)
            {
                uint id = 65000520;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//魔法攻撃の護符＋３
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if (rate2 > 10 && rate2 < 15)
            {
                uint id = 65000100;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//攻撃の護符＋２
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
            else if (rate2 > 15 && rate2 < 20)
            {
                uint id = 65000100;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//魔法攻撃の護符＋２
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【泰达米尔龙战利品】中获得了 " + item.BaseData.name);
                }
            }
        }
    }
}

