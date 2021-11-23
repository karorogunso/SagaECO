using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S12002085 : Event
    {
        public S12002085()
        {
            this.EventID = 12002085;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 9571);
            Wait(pc, 990);

            Say(pc, 0, 0, "……？？$R;" +
            "何か見えない壁がある…$R;" +
            "$R…通れないようだ。$R;", "");

        }
    }
}