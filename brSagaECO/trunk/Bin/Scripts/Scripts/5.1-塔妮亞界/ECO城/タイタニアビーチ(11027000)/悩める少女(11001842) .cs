using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001842 : Event
    {
        public S11001842()
        {
            this.EventID = 11001842;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……う～ん。$R;" +
            "海風は気持ちいわね～！$R;" +
            "$R温泉に入ったあとは格別よっ♪$R;", "悩める少女");
}
}

        
    }


