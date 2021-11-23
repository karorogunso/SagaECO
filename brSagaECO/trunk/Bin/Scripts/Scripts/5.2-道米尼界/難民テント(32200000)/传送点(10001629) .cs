using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200000
{
    public class S10001629 : Event
    {
        public S10001629()
        {
            this.EventID = 10001629;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))
            {
                Warp(pc, 12035001, 46, 176);
            }
            else
            {
                Warp(pc, 12035000, 46, 176);
            }
        }
    }
}
        
   


