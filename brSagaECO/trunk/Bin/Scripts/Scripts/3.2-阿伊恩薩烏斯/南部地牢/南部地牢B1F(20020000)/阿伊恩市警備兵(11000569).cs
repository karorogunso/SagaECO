using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000569 : Event
    {
        public S11000569()
        {
            this.EventID = 11000569;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "從這裡開始$R;" +
                "會很危險的，請小心！！$R;");
        }
    }
}