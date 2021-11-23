using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001878 : Event
    {
        public S11001878()
        {
            this.EventID = 11001878;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "可愛いよな～。$R;" +
            "あの娘、でじこちゃんって言うんだぜ？$R;" +
            "$Rはぁ～。$R;" +
            "最近はあの娘を眺める日々だよ……。$R;", "デジコを見つめる男");
}
}

        
    }


