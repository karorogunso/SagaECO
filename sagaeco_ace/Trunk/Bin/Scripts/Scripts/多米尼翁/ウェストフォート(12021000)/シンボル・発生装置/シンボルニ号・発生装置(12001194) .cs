using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S12001194 : Event
    {
        public S12001194()
        {
            this.EventID = 12001194;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);

            Say(pc, 0, 131, "（……ピッ）$R;" +
            "$R西部要塞シンボル$R;" +
            "……展開準備中。$R;", "システム");
        }

    }

}
            
            
        
     
    