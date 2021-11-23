using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001594 : Event
    {
        public S11001594()
        {
            this.EventID = 11001594;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……。$R;" +
            "$R……糟糕、不能在夜間運行。$R;", "飛空庭係員");

            Say(pc, 131, "……稍後再來吧。$R;", "飛空庭係員");
            //
            /*
            Say(pc, 131, "……。$R;" +
            "$R……悪いが、夜間運行はしていない。$R;", "飛空庭係員");

            Say(pc, 131, "……出直しな。$R;", "飛空庭係員");
            */

        }
    }
}


        
   


