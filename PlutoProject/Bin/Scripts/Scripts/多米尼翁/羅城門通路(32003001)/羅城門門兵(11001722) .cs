using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001722 : Event
    {
        public S11001722()
        {
            this.EventID = 11001722;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "くっ……敵だと！？$R;" +
            "$R我々の眼を$R;" +
            "潜り抜けてきたとでもいうのか！？$R;", "羅城門門兵");
        }

    }
}
                
            
            
        
     
    