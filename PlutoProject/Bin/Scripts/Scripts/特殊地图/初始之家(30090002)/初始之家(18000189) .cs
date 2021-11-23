using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30090002
{
    public class S18000189 : Event
    {
        public S18000189()
        {
            this.EventID = 18000189;
        }

        public override void OnEvent(ActorPC pc)
        {
                    Say(pc, 0, 0, "コンコン！$R;" + 
"$R（誰かが来たみたい……）$R;", "");
Warp(pc, 10018104, 204, 65);
         }
     }
}
