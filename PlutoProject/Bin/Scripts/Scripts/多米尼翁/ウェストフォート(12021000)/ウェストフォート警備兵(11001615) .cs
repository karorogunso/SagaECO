using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001615 : Event
    {
        public S11001615()
        {
            this.EventID = 11001615;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……我。$R;" +
            "如果這個戰爭結束了的話$R;" +
            "要和那個人結婚……。$R;", "西部要塞警備兵");
        }

    }

}
            
            
        
     
    