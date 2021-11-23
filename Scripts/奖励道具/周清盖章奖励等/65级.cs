
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
    public class S910000129 : Event
    {
        public S910000129()
        {
            this.EventID = 910000129;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000129) > 0)
            {
                GiveItem(pc, 950000027, 1);//S突破
                GiveItem(pc, 910000104, 1);//300任务点
                GiveItem(pc, 910000040, 1);//CP5000
                GiveItem(pc, 950000025, 10);//KUJI
                GiveItem(pc, 951000000, 50);//雷池
                TakeItem(pc, 910000129, 1);
            }
        }
    }
}

