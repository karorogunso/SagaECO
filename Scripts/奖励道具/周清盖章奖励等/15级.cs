
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
    public class S910000124 : Event
    {
        public S910000124()
        {
            this.EventID = 910000124;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000124) > 0)
            {
                GiveItem(pc, 950000000, 1);//发型
                GiveItem(pc, 950000001, 1);//脸型
                GiveItem(pc, 950000025, 3);//KUJI
                GiveItem(pc, 951000000, 10);//雷池
                TakeItem(pc, 910000124, 1);
            }
        }
    }
}

