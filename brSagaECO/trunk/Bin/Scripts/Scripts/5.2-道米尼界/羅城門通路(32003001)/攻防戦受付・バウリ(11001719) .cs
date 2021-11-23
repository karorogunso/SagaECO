using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001719 : Event
    {
        public S11001719()
        {
            this.EventID = 11001719;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "敵、是敵人的攻擊！？$R;" +
            "究竟是由哪邊來......$R;", "攻防戦受付・バウリ");
        }

    }

}

/*
Say(pc, 131, "て、敵襲だと！？$R;" +
"一体どこから……。$R;", "攻防戦受付・バウリ");
*/            
        
     
    
