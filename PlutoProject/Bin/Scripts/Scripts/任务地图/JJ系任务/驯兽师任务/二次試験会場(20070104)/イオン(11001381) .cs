using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S11001381 : Event
    {
        public S11001381()
        {
            this.EventID = 11001381;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "やあ、今回は少しキツかったな$R;" +
            "とにかく合格できて良かった。$R;" +
            "$Rもう少しでタイムオーバーに$R;" +
            "なるところだった……$R;", "イオン");

        }
    }
}