using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000282 : Event
    {
        public S13000282()
        {
            this.EventID = 13000282;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "私に何か用か？$R;" +
            "$P……ふん！$R;" +
            "$R用もなく話しかけるな$R;" +
            "貴様の相手をするほど暇ではない。$R;", "キュルビス");
        }
    }
}