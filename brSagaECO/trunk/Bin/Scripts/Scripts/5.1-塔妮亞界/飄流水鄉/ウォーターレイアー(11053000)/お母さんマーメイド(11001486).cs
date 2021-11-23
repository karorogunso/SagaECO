	using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001486 : Event
    {
        public S11001486()
        {
            this.EventID = 11001486;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 331, "這裡不可以通過！$R;" +
            "即使堵上生命也不行!$R;", "お母さんマーメイド");
        }


    }
}


