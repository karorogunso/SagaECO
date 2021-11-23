using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001610 : Event
    {
        public S11001610()
        {
            this.EventID = 11001610;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "正在談要如何抵抗$R;" +
            "DEM。$R;", "艾瑪");
        }

    }

}
            
            
        
     
    