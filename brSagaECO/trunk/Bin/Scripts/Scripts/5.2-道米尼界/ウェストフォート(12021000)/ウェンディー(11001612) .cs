using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001612 : Event
    {
        public S11001612()
        {
            this.EventID = 11001612;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……？$R;" +
            "$R你想幹什麽。$R;" +
            "難道說你想加入我們$R;" +
            "的團隊？$R;", "溫迪");
        }
    }
}
            
            
        
     
    