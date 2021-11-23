using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001877 : Event
    {
        public S11001877()
        {
            this.EventID = 11001877;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "くそっ、くそっ！$R;" +
            "あそこでジョーカーなんて$R;" +
            "汚いだろうッ！$R;", "全てを失った男");
}
}

        
    }


