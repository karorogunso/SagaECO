using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001764 : Event
    {
        public S11001764()
        {
            this.EventID = 11001764;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "（……ひそひそ）$R;" +
            "$R（……いい加減$R;" +
            "　あっちに、行ってくれないかな。）$R;", "トップモデル");
}
}

        
    }


