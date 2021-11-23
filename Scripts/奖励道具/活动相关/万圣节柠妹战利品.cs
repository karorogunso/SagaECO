
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
    public class S910000060 : Event
    {
        public S910000060()
        {
            this.EventID = 910000060;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000060) >= 1)
            {
                TakeItem(pc, 910000060, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int g = SagaLib.Global.Random.Next(300000, 800000);
            pc.Gold += g;



            ushort c = (ushort)Global.Random.Next(70, 120);
            if(Global.Random.Next(0,100) < 15)
                c = (ushort)Global.Random.Next(100, 200);
            GiveItem(pc, 950000010, c);
            if (pc.Party != null)
                foreach (var m in pc.Party.Members)
                    if (m.Value.Online)
                        SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【万圣节·柠妹战利品】中获得了 " + c.ToString() +"个 糖果");
            GiveItem(pc, 950000100, 1);
            GiveItem(pc, 910000040, 1);
            GiveItem(pc, 950000005, 50);
            if (Global.Random.Next(0, 100) < 100)
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【万圣节·柠妹战利品】中获得了 " + item.BaseData.name);
                    }
                }
            }

            if (Global.Random.Next(0, 100) < 80)
            {
                uint id = 940000001;
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, id, 1);//+12~+15强化石
                if (pc.Party != null)
                {
                    SagaDB.Item.Item item = ItemFactory.Instance.GetItem(id);
                    foreach (var m in pc.Party.Members)
                    {
                        if (m.Value.Online)
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【万圣节·柠妹战利品】中获得了 " + item.BaseData.name);
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
                            SagaMap.Network.Client.MapClient.FromActorPC(m.Value).SendSystemMessage(pc.Name + " 从【万圣节·柠妹战利品】中获得了150个 " + item.BaseData.name);
                    }
                }
            }
        }
    }
}

