using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001858 : Event
    {
        public S11001858()
        {
            this.EventID = 11001858;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "あ～、もうこのままここで$R;" +
            "寝ちゃおうかなぁ。$R;", "湯上りの女");
        }


    }
}


