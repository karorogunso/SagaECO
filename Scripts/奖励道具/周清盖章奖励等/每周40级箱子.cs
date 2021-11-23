
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
    public class S910000020 : Event
    {
        public S910000020()
        {
            this.EventID = 910000020;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000020) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                for (int i = 0; i < 10; i++)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                GiveItem(pc, 950000000, 1);//发型币
                GiveItem(pc, 950000002, 1);//扭曲石头
                pc.Gold += Global.Random.Next(100000, 100000);
                GiveItem(pc, 100000000, 100);//KUJI币
                GiveItem(pc, 950000005, 10);//特质KUJI币
                GiveItem(pc, 950000003, 30);//武器碎片

                TakeItem(pc, 910000020, 1);
            }
        }
    }
}

