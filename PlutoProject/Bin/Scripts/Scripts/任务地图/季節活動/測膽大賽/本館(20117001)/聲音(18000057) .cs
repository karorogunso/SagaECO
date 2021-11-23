using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M20117001
{
    public class S18000057 : Event
    {
        public S18000057()
        {
            this.EventID = 18000057;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                return;
            }
            PlaySound(pc, 2410, false, 70, 80);
            Wait(pc, 990);
            PlaySound(pc, 2410, false, 80, 20);
            PlaySound(pc, 2411, false, 60, 50);
         }
     }
}
