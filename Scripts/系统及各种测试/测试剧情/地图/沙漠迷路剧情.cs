
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S50005001 : Event
    {
        public S50005001()
        {
            this.EventID = 50005001;
        }

        public override void OnEvent(ActorPC pc)
        {
            MovePC(pc, 93, 117, 200, 6, MoveType.WALK);
            Wait(pc, 10000);
            Select(pc, " ", "", "走..快走不动了");
            MotionPC(pc, 362, 1);
            Wait(pc, 1000);
            Fade(pc, FadeType.Out, FadeEffect.Black);
            Wait(pc, 1000);
            Select(pc, " ", "", "我..要死了吗...");
            Wait(pc, 1000);
            
        }
    }
}

