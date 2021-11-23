using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001769 : Event
    {
        public S11001769()
        {
            this.EventID = 11001769;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "む～、このカレー……。$R;" +
            "ちょっと辛すぎやん！$R;", "トップモデル");

}
}

        
    }


