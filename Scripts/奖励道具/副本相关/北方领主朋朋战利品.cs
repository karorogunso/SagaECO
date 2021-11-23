
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
    public class S910000041 : Event
    {
        public S910000041()
        {
            this.EventID = 910000041;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
        void 奖励(ActorPC pc)
        {
            int g = SagaLib.Global.Random.Next(500000, 1000000);
            pc.Gold += g;

            GiveItem(pc, 100000000, 1);
            if(Global.Random.Next(0, 100) < 45)
            {
                uint id = 910000042;//朋朋的首饰盒
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if(m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·朋朋的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if(Global.Random.Next(0, 100) < 200)
            {
                uint id = 910000040;//5000CP
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 4);
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if(m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·朋朋的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 50)
            {
                uint id = 950000101;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//卡片x5
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·朋朋的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }
            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 950000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//抽脸币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·朋朋的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) <100)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·朋朋的战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 100)
            {
                uint id = 100000000;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 150);//KUJI币
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【北方领主·迷你兔的战利品】中获得了150个 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

