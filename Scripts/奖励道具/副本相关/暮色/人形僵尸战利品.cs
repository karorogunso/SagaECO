
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
    public class S910000005 : Event
    {
        public S910000005()
        {
            this.EventID = 910000005;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000005) >= 1)
            {
                TakeItem(pc, 910000005, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int g = SagaLib.Global.Random.Next(50000, 220000);
            pc.Gold += g;

            GiveItem(pc, 100000000, 1);
            GiveItem(pc, 950000003, (ushort)Global.Random.Next(10, 20));//武器碎片

            if (Global.Random.Next(0, 100) < 1)
            {
                uint id = 910000008;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//职业装
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【人形僵尸战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 70)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【人形僵尸战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 40)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【人形僵尸战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 940000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【人形僵尸战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 90)
            {
                uint id = 100000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 5);//KUJI币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【人形僵尸战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

