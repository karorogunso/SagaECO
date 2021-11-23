using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001795 : Event
    {
        public S11001795()
        {
            this.EventID = 11001795;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "すやすや…$R;" +
            "$P…私が寝てると思ったでしょ？$R;" +
            "$Pそうよ、寝てるのよ。$R;" +
            "$Rすやすや…$R;", "居眠りしてる女");
        }
    }
}