
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
    public class S910000018 : Event
    {
        public S910000018()
        {
            this.EventID = 910000018;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000018) > 0)
            {
                PlaySound(pc, 2040, false, 100, 50);
                List<uint> list = KujiListFactory.Instance.NotInKujiClothesList;
                if(Global.Random.Next(0,100) < 30)
                    GiveItem(pc, list[Global.Random.Next(0, list.Count)], 1);
                else
                    GiveItem(pc, list[Global.Random.Next(0, list.Count) / 3], 1);
                TakeItem(pc, 910000018, 1);
            }
        }
    }
}

