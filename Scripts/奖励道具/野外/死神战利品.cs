
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
    public class S910000112 : Event
    {
        public S910000112()
        {
            this.EventID = 910000112;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000112) >= 1)
            {
                TakeItem(pc, 910000112, 1);
                奖励(pc);
            }
        }


        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 950000025, 1);
            int g = Global.Random.Next(9000, 30000);
            pc.Gold += g;
            if (Global.Random.Next(0, 100) < 70)
            {
                uint id = 960000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, (ushort)Global.Random.Next(1, 5));//项链石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 70)
            {
                uint id = 960000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, (ushort)Global.Random.Next(1, 5));//武器石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 70)
            {
                uint id = 960000002;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, (ushort)Global.Random.Next(1, 5));//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 960000012;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, (ushort)Global.Random.Next(1, 3));//祝福水
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 950000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 950000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 1)
            {
                uint id = 950000027;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【死神战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

