
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
    public class S910000000 : Event
    {
        public S910000000()
        {
            this.EventID = 910000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc,910000000) >= 1)
            {
                TakeItem(pc, 910000000, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 200);
            int Coinrate = Global.Random.Next(0, 100);
            int Srate = Global.Random.Next(0, 800);
                PlaySound(pc, 2060, false, 100, 50);
            if (rate <130)
            {
                int g = Global.Random.Next(100, 300);
                pc.Gold += g;
                return;
            }
            if(rate < 180)
            {
                int g = Global.Random.Next(300, 1000);
                pc.Gold += g;
                return;
            }
            if (rate <= 200)
            {
                int g = Global.Random.Next(1000, 1800);
                pc.Gold += g;
                return;
            }
        }
    }
}

