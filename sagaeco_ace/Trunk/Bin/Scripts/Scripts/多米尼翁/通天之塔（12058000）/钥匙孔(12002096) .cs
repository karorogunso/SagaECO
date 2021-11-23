using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12002096
{
    public class S12002096 : Event
    {
        public S12002096()
        {
            this.EventID = 12002096;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Quest != null)
                Say(pc, 131, "要交掉任务以后才能通过.");
            else if (Select(pc, "回埃米尔界吗？", "", "不回去", "回去") == 2)
            {
                if (pc.PossesionedActors.Count == 0)
                {
                    if (pc.Quest != null)
                    {
                        Say(pc, 131, "要交掉任务以后才能通过.");
                        return;
                    }
                    ShowEffect(pc, 127, 153, 5306);
                    PlaySound(pc, 2407, false, 100, 50);
                    Wait(pc, 990);
                    Wait(pc, 3960);
                    Warp(pc, 10058000, 126, 157);
                    SetHomePoint(pc, 10058000, 127, 155);
                }
                else
                {
                    Say(pc, 131, "解除凭依之后才能通过哦.");
                }
            }
        }
    }
}
