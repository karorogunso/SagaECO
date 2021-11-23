
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
    public class S910000123 : Event
    {
        public S910000123()
        {
            this.EventID = 910000123;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000123) > 0)
            {
                List<uint> kujis = KujiListFactory.Instance.EventKujiList;
                PlaySound(pc, 2040, false, 100, 50);
                uint itemid = kujis[Global.Random.Next(0, kujis.Count - 1)];
                GiveItem(pc, itemid, 1);
                TakeItem(pc, 910000123, 1);
            }
        }
    }
}

