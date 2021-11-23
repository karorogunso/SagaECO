using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000263 : Event
    {
        public S13000263()
        {
            this.EventID = 13000263;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "しっ、静かにして！$R;", "二年生");
        }
    }
}