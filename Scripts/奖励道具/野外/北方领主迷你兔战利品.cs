
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
    public class S910000010 : Event
    {
        public S910000010()
        {
            this.EventID = 910000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000010) >= 1)
            {
                TakeItem(pc, 910000010, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 950000025, 1);
            GiveItem(pc, 68500152, 1);
            GiveItem(pc, 953000000, 5);
            GiveItem(pc, 953000021, 2);
            GiveItem(pc, 952000000, 500);
            GiveItem(pc, 952000001, 5); 
            GiveItem(pc, 910000106, 2);

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000116;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000116;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000116;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000116;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 910000040;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000116;//CP1000
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            int g = Global.Random.Next(20000, 50000);
            pc.Gold += g;
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 910000008;//职业装
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 910000007;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//职业装配件
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 950000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//发型币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 950000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//发型币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

