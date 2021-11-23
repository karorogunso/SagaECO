using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001768 : Event
    {
        public S11001768()
        {
            this.EventID = 11001768;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "スイカいかがっすか～？$R;", "トップモデル");
}
}

        
    }


