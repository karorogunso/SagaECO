using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001857 : Event
    {
        public S11001857()
        {
            this.EventID = 11001857;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "えっ？ここで何をしているかですって？$R;" +
            "$R……人がいると恥ずかしくて$R;" +
            "着替えられないじゃないっ。$R;" +
            "$Rだから私はここで脱衣所から$R;" +
            "人がいなくなるのを待っているの。$R;", "様子を見る女");
        }


    }
}


