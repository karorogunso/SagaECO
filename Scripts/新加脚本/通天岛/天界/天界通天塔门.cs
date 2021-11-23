
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
    public class S12002088 : Event
    {
        public S12002088()
        {
            this.EventID = 12002088;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch(Select(pc,"要进入通天塔里面吗？","","进去","还是算了"))
            {
                case 1:
                    ShowEffect(pc, 127, 154, 5267);
                    NPCMotion(pc, 12002087, 622, false,1);
                    Wait(pc, 200);
                    NPCMotion(pc, 12002087, 621, false, 1);
                    Wait(pc, 200);
                    NPCMotion(pc, 12002087, 622, false, 1);
                    Wait(pc, 2000);
                    Warp(pc, 20170000, 31, 60);
                    break;
            }
        }
    }
}