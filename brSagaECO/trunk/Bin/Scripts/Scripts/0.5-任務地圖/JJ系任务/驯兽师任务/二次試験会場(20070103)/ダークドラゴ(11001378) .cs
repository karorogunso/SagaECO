using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070103
{
    public class S11001378 : Event
    {
        public S11001378()
        {
            this.EventID = 11001378;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ガァ$R;", "ダークドラゴ");


        }
    }
}