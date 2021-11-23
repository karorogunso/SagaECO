using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30133000
{
    public class S11000746 : Event
    {
        public S11000746()
        {
            this.EventID = 11000746;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000746, 190, "女人真是多嘴啊$R;");
            Say(pc, 11000697, 190, "…真是$R;");
        }
    }
}