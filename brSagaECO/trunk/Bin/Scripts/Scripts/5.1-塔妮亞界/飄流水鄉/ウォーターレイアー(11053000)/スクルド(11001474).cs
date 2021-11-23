using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001474 : Event
    {
        public S11001474()
        {
            this.EventID = 11001474;
        }

        public override void OnEvent(ActorPC pc)
        {


            Say(pc, 131, "爲什麽我們$R;" +
            "非要做這樣的警告呢……。$R;" +
            "$R現在還不能說。$R;", "スクルド");
        }


    }
}


