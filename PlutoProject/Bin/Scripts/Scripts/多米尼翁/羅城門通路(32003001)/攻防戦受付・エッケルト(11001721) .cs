using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001721 : Event
    {
        public S11001721()
        {
            this.EventID = 11001721;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ここは危ない！$R;" +
            "早く避難しろ！！$R;", "攻防戦受付・エッケルト");
        }

    }

}
            
            
        
     
    