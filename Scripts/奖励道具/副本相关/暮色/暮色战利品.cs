
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
    public class S910000003 : Event
    {
        public S910000003()
        {
            this.EventID = 910000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000003) >= 1)
            {
                TakeItem(pc, 910000003, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 200);
            int Coinrate = Global.Random.Next(0, 220);
            int stuff = Global.Random.Next(0, 100);

            if(Global.Random.Next(0, 100) < 20)
            GiveItem(pc, 950000003, (ushort)Global.Random.Next(1, 2));//武器碎片

            if (rate < 1)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 950000000, 1);
            }
            if (Coinrate < 1)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 100000000, 5);
            }
            else if (Coinrate < 2)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 100000000, 4);
            }
            else if (Coinrate < 3)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 100000000, 3);
            }
            else if (Coinrate < 8)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 100000000, 2);
            }
            else if (Coinrate < 43)
            {
                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 100000000, 1);
            }
            else
                PlaySound(pc, 2060, false, 100, 50);
            if (rate < 180)
            {
                int g = Global.Random.Next(5000, 13000);
                pc.Gold += g;
                return;
            }
            if (rate < 195)
            {
                int g = Global.Random.Next(13000, 25000);
                pc.Gold += g;
                return;
            }
            if (rate <= 200)
            {
                int g = Global.Random.Next(25000, 50000);
                pc.Gold += g;
                return;
            }
        }
    }
}

