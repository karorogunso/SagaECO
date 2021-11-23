using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001869 : Event
    {
        public S11001869()
        {
            this.EventID = 11001869;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "頭髮和身體。$R;" +
            "$R 到底要先洗哪邊啊！$R;", "困惑中的少女");
            /*
            Say(pc, 0, "髪と体。$R;" +
            "$Rどちらを先に洗えばいいのっ！$R;", "困っている少女");
            */
        }


    }
}


