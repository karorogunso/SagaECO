
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
    public class S910000042 : Event
    {
        public S910000042()
        {
            this.EventID = 910000042;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000042) >= 1)
            {
                TakeItem(pc, 910000042, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate1 = Global.Random.Next(1, 4);
            int rate2 = Global.Random.Next(1, 1000);
            uint id = 250105400;
            switch(rate1)
            {
                case 1:
                    id = 250105400;
                    break;
                case 2:
                    id = 250122500;
                    break;
                case 3:
                    id = 250120500;
                    break;
                case 4:
                    id = 250122700;
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
    }
}

