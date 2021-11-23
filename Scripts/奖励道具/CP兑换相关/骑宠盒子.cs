
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
    public class S910000035 : Event
    {
        public S910000035()
        {
            this.EventID = 910000035;
        }

        public override void OnEvent(ActorPC pc)
        {
return;
            if (CountItem(pc, 910000035) >= 1)
            {
                TakeItem(pc, 910000035, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);
            List<uint> list = KujiListFactory.Instance.RidePartnerList;
            if (Global.Random.Next(0, 100) < 80)
                GiveItem(pc, list[Global.Random.Next(0, list.Count / 2)], 1);
            else
                GiveItem(pc, list[Global.Random.Next(0, list.Count-1)], 1);
        }
    }
}

