
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
    public class S910000047 : Event
    {
        public S910000047()
        {
            this.EventID = 910000047;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000047) >= 1)
            {
                TakeItem(pc, 910000047, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 200);
            int Coinrate = Global.Random.Next(0, 100);
            int Srate = Global.Random.Next(0, 100);
            GiveItem(pc, 100000000, 20);

            if (Coinrate < 5)
            {
                int rate1 = Global.Random.Next(1, 3);
                int rate2 = Global.Random.Next(1, 1000);

                uint id = 0;
                switch (rate1)
                {
                    case 1:
                        id = 250011110;
                        break;
                    case 2:
                        id = 250060200;
                        break;
                    case 3:
                        id = 210057600;
                        break;
                }
                if (rate2 <= 3)
                    id += 4;
                else if (rate2 < 15)
                    id += 3;
                else if (rate2 < 70)
                    id += 2;
                else if (rate2 < 300)
                    id += 1;
                GiveItem(pc, id, 1);
            }

            PlaySound(pc, 2040, false, 100, 50);
            if (rate < 130)
            {
                int g = Global.Random.Next(5000, 10000);
                pc.Gold += g;
                return;
            }
            if (rate < 180)
            {
                int g = Global.Random.Next(10000, 25000);
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

