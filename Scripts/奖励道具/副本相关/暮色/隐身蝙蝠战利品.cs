
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
    public class S910000004 : Event
    {
        public S910000004()
        {
            this.EventID = 910000004;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000004) >= 1)
            {
                TakeItem(pc, 910000004, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int g = SagaLib.Global.Random.Next(10000, 150000);
            pc.Gold += g;

            GiveItem(pc, 100000000, 1);

            GiveItem(pc, 950000003, (ushort)Global.Random.Next(1, 10));//武器碎片

            if (Global.Random.Next(0, 100) < 30)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【隐身蝙蝠战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 10)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【隐身蝙蝠战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 25)
            {
                uint id = 940000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//+9~+12强化石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【隐身蝙蝠战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 100000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 2);//KUJI币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【隐身蝙蝠战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

