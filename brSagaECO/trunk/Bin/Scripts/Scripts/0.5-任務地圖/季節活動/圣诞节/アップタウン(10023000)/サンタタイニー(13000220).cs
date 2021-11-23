using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023000
{
    public class S13000220 : Event
    {
        public S13000220()
        {
            this.EventID = 13000220;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ペロペロ、ぱくぱくごっくん！$R;" +
            "甘くて、おいし～な～！$R;", "サンタタイニー");
        }
    }
}