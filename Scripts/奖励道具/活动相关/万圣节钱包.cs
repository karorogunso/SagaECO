
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
    public class S910000058 : Event
    {
        public S910000058()
        {
            this.EventID = 910000058;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000058) >= 1)
            {
                TakeItem(pc, 910000058, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 200);
            int Coinrate = Global.Random.Next(0, 100);
            int Srate = Global.Random.Next(0, 800);
            GiveItem(pc, 950000010, 1);

                PlaySound(pc, 2060, false, 100, 50);
            if (rate <130)
            {
                int g = Global.Random.Next(1000, 6000);
                pc.Gold += g;
                return;
            }
            if(rate < 190)
            {
                int g = Global.Random.Next(6000, 15000);
                pc.Gold += g;
                return;
            }
            if (rate <= 200)
            {
                int g = Global.Random.Next(15000,20000);
                pc.Gold += g;
                return;
            }

        }
    }
}

