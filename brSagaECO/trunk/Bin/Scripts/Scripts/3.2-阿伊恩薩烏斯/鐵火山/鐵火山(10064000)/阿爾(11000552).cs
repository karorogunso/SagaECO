using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000552 : Event
    {
        public S11000552()
        {
            this.EventID = 11000552;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊，$R;" +
                "真是危險啊$R;" +
                "從天而降的火山碳$R真是可怕啊。$R;" +
                "$P差點就被打到了。$R;" +
                "真的降得很快$R;" +
                "如果認為危險的話$R;" +
                "最好是躲到避難所。$R;");
        }
    }
}