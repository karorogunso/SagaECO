using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001806 : Event
    {
        public S11001806()
        {
            this.EventID = 11001806;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "あ～…$R;" +
            "冬はやっぱりコタツだよねぇ～$R;" +
            "$Pもうここから出たくないな～$R;" +
            "動くのも面倒臭いな～$R;", "幸せそうな女");
        }
    }
}