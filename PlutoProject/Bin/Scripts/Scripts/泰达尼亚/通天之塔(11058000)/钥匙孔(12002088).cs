using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S12002088 : Event
    {
        public S12002088()
        {
            this.EventID = 12002088;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "返回埃米尔界？", "", "不返回", "返回") == 2)
            {
                ShowEffect(pc, 127, 153, 5306);
                PlaySound(pc, 2407, false, 100, 50);
                Wait(pc, 990);
                NPCMotion(pc, 12002087, 621);
                Wait(pc, 3960);
                Warp(pc, 10058000, 126, 157);
                //Wait(pc, 1000);
                //NPCShow(pc, 11001653);
                //NPCShow(pc, 11001652);
                //NPCShow(pc, 11001651);
                //NPCShow(pc, 11001650);
                //NPCHide(pc, 11001653);
                //NPCHide(pc, 11001652);
                //NPCHide(pc, 11001651);
                //NPCHide(pc, 11001650);
                NPCMotion(pc, 627, 135);
            }
        }
    }
}
