using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001839 : Event
    {
        public S11001839()
        {
            this.EventID = 11001839;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "ようこそ、ECOタウンへ♪$R;" +
            "$Rお金の使いすぎには$R;" +
            "気をつけてね♪$R;", "日に焼けた少女");
}
}

        
    }


