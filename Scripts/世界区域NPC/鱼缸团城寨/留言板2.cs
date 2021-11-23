
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
    public class S60000023: Event
    {
        public S60000023()
        {
            this.EventID = 60000023;
        }

        public override void OnEvent(ActorPC pc)
        {
   OpenBBS(pc,9,0);
        }
    }
}