
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
    public class S910000015 : Event
    {
        public S910000015()
        {
            this.EventID = 910000015;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000015) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                for (int i = 0; i < 10; i++)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                GiveItem(pc, 950000001, 1);
GiveItem(pc, 950000025, 5);
GiveItem(pc, 951000000, 5);
                GiveItem(pc, 950000000, 1);
                
                TakeItem(pc, 910000015, 1);
            }
        }
    }
}

