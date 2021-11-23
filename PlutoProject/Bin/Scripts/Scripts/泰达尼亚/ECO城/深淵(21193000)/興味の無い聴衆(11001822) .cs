using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001822 : Event
    {
        public S11001822()
        {
            this.EventID = 11001822;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "仮にあの人の言う通りだったとしても、$R;" +
            "俺らはあの人に利用されるだけだろ？$R;" +
            "$P結局は自分の満足のために$R;" +
            "でかい声出して叫んでるだけなのさ。$R;" +
            "$Rくだらないな。$R;", "興味の無い聴衆");
        }
    }
}