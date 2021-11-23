using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S11001651 : Event
    {
        public S11001651()
        {
            this.EventID = 11001651;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "こんな場所から本当に行けるのかしら？$R;" +
            "不思議だわ。$R;", "マーシャ");
        }
    }
}
            
            
        
     
    