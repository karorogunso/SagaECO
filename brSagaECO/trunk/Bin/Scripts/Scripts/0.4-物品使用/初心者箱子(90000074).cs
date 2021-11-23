using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M初心者箱子
{
    public class S90000074 : Event
    {
        public S90000074()
        {
            this.EventID = 90000074;
        }

        public override void OnEvent(ActorPC pc)
        {
            GiveItem(pc, 50020100, 1);
            GiveItem(pc, 50020200, 1);
            GiveItem(pc, 60108900, 1);
            GiveItem(pc, 50073500, 1);
            GiveItem(pc, 10042801, 1);
            TakeItem(pc, 10020114, 1);

        }
    }
}