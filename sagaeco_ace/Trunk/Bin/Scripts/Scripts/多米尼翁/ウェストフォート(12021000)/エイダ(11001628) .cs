using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001628 : Event
    {
        public S11001628()
        {
            this.EventID = 11001628;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 332, "……找人家不知道有什么事？$R;" +
            "雖然我現在在忙于訓練？$R;" +
            "$R有什么事的話就請簡短地說吧。$R;", "艾達");
        }

    }

}
            
            
        
     
    