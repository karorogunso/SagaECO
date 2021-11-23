using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001872 : Event
    {
        public S11001872()
        {
            this.EventID = 11001872;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "嗚～、感覺很差～。$R;" +
           "$R……著了涼耶。$R;", "著了涼的少女");
            /*
            Say(pc, 0, "う～、きもちわるい～。$R;" +
            "$R……のぼせちゃったかなぁ。$R;", "のぼせた少女");
            */
        }


    }
}


