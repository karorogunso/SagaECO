using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001767 : Event
    {
        public S11001767()
        {
            this.EventID = 11001767;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "あはっ！$R;" +
            "楽しまなくっちゃ損よ！$R;", "トップモデル");
}
}

        
    }


