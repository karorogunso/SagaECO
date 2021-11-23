
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
    public class S910000072 : Event
    {
        public S910000072()
        {
            this.EventID = 910000072;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000072) >= 1)
            {
                TakeItem(pc, 910000072, 1);
                奖励(pc);
            }
        }


        void 奖励(ActorPC pc)
        {
             GiveItem(pc, 950000025, 1);
            int g = Global.Random.Next(100, 8000);
            pc.Gold += g;
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【鱼缸本鱼人阿鲁玛战利品】中获得了 " + item.BaseData.name);
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【鱼缸本鱼人阿鲁玛战利品】中获得了 " + item.BaseData.name);
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【鱼缸本鱼人阿鲁玛战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

