using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50063000
{
    public class S10000294 : Event
    {
        public S10000294()
        {
            this.EventID = 10000294;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12058000, 122, 236);
            NPCHide(pc, 11001999);
        }
    }
}