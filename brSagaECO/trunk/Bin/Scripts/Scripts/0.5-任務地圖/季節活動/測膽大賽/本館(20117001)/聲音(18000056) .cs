using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M30154002
{
    public class S18000056 : Event
    {
        public S18000056()
        {
            this.EventID = 18000056;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                PlaySound(pc, 2446, false, 70, 80);
                Wait(pc, 990);
                PlaySound(pc, 2446, false, 70, 30);
            }
         }
     }
}
