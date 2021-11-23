using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001740 : Event
    {
        public S11001740()
        {
            this.EventID = 11001740;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "ここは、アトラクションゾーンだよ！$R;", "バニーちゃん");
        }


    }
}


