using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S11001652 : Event
    {
        public S11001652()
        {
            this.EventID = 11001652;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "久々の帰郷が、こんな形になるとはね。$R;" +
            "複雑だわぁ。$R;", "ルルイエ");

        }
    }
}
            
        
     
    