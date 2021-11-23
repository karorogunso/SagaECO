
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000019: Event
    {
        public S60000019()
        {
            this.EventID = 60000019;
        }

        public override void OnEvent(ActorPC pc)
        {
   OpenBBS(pc,8,0);
        }
    }
}