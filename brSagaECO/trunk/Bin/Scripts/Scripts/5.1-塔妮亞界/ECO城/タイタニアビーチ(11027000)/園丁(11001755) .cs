using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001755 : Event
    {
        public S11001755()
        {
            this.EventID = 11001755;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ここが異次元だって関係ないね。$R;" +
            "$R俺は庭仕事さえ出来れば問題ないよ。$R;", "園丁");
}
}

        
    }


