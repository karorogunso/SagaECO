using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001752 : Event
    {
        public S11001752()
        {
            this.EventID = 11001752;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ここは、閉ざされた街$R;" +
            "ＥＣＯタウンだよ。$R;" +
            "$Rキミも、くじらに飲まれたの？$R;", "ドアマン");
}
}

        
    }


