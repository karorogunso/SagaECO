
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
    public class S910000032 : Event
    {
        public S910000032()
        {
            this.EventID = 910000032;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 4276);
            Warp(pc, 31301000, 33, 23);
        }
    }
}

