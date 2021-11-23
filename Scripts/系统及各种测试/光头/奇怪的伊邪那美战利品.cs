
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000033 : Event
    {
        public S910000033()
        {
            this.EventID = 910000033;
        }

        public override void OnEvent(ActorPC pc)
        {
            TakeItem(pc, 910000033, 1);
        }
    }
}

