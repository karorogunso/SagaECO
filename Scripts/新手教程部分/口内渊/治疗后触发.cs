
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class S50200001 : Event
    {
        public S50200001()
        {
            this.EventID = 50200001;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Slave.Count > 0)
            {
                foreach (var i in pc.Slave)
                {
                    ((MobEventHandler)i.e).AI.Start();
                }
            }
        }
    }
}