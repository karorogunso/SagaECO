using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12020001
{
    public class S12001184 : Event
    {
        public S12001184()
        {
            this.EventID = 12001184;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "당신도 받았군요$R;" +
            "어둠의 힘을…$R;" +
            "$R어둠의 힘은 대단해요……。$R;" +
            "후훗 이제 곧 나도 어둠과 하나야…$R;", "어둠의 정령");
            if (Select(pc, "어떻게 할까？", "", "돌아간다", "돌아가지 않는다") == 1)
            {
                ShowEffect(pc, 5066);
                ShowEffect(pc, 5180);
                Wait(pc, 990);
                Warp(pc, 12035001, 204, 73);
            }
        }
    }
}


        
   


