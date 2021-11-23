
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
    public partial class MOBTEST : Event
    {
        public MOBTEST()
        {
            this.EventID = 90000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            刷怪(pc.MapID, pc);
        }
    }
}

