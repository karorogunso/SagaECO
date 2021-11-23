
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
    public class S910000024 : Event
    {
        public S910000024()
        {
            this.EventID = 910000024;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000024) >= 1)
            {
                TakeItem(pc, 910000024, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 200);
            int Coinrate = Global.Random.Next(0, 210);
            int stuff = Global.Random.Next(0, 100);

            if (rate < 180)
            {
                int g = Global.Random.Next(500, 1300);
                pc.Gold += g;
                return;
            }
            if (rate < 195)
            {
                int g = Global.Random.Next(1300, 2500);
                pc.Gold += g;
                return;
            }
            if (rate <= 200)
            {
                int g = Global.Random.Next(2500, 5000);
                pc.Gold += g;
                return;
            }
        }
    }
}

