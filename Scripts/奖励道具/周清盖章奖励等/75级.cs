
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
    public class S910000130 : Event
    {
        public S910000130()
        {
            this.EventID = 910000130;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000130) > 0)
            {
                GiveItem(pc, 950000037, 1);//S转生
                GiveItem(pc, 910000104, 1);//300任务点
                GiveItem(pc, 910000040, 2);//CP5000
                GiveItem(pc, 950000025, 10);//KUJI
                GiveItem(pc, 951000000, 50);//雷池
                TakeItem(pc, 910000130, 1);
            }
        }
    }
}

