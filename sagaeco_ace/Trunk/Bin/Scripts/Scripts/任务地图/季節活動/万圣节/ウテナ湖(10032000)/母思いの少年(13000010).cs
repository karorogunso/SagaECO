using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S13000010 : Event
    {
        public S13000010()
        {
            this.EventID = 13000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "オフクロに会いに来たんだ。$R;" +
            "$Rおれ、元気にしてるからさ$R;" +
            "オフクロも元気にしろよ……。$R;", "母思いの少年");
        }
    }
}
