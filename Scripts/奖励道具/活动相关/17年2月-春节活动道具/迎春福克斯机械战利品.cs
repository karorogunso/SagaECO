
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
    public class S910000026 : Event
    {
        public S910000026()
        {
            this.EventID = 910000026;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000026) >= 1)
            {
                TakeItem(pc, 910000026, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 950000030, (ushort)SagaLib.Global.Random.Next(10, 30));
            if (Global.Random.Next(0, 100) < 9)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 910000101;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//5任务点
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 20)
            {
                uint id = 910000102;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//10任务点
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 2)
            {
                uint id = 910000103;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//50任务点
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 3)
            {
                uint id = 950000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//脸币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 960000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//项链石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 960000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//武器石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 15)
            {
                uint id = 960000002;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//衣服石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 200) < 3)
            {
                uint id = 950000027;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//S
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【迎春福克斯机械战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

