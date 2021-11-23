
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
    public class S910000028 : Event
    {
        public S910000028()
        {
            this.EventID = 910000028;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000028) >= 1)
            {
                TakeItem(pc, 910000028, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 152);
            uint id = 10162400;
            if (rate <= 50)
                id = 10162400;
            else if (rate <= 75)
                id = 10162401;
            else if (rate <= 92)
                id = 10162407;
            else if (rate <= 110)
                id = 10162403;
            else if (rate <= 125)
                id = 10162404;
            else if (rate <= 140)
                id = 10162405;
            else if (rate <= 150)
                id = 10162406;
            else
                id = 10162402;
            GiveItem(pc, id, 1);
        }
    }
}

