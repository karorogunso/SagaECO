using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001766 : Event
    {
        public S11001766()
        {
            this.EventID = 11001766;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "ハァ～イ！$R;" +
            "楽しんでる～？$R;", "トップモデル");
}
}

        
    }


