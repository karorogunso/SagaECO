using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001812 : Event
    {
        public S11001812()
        {
            this.EventID = 11001812;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "この店のメニューはどれも正解なんだ。$R;" +
            "$R一日の楽しみの一つさ！$R;" +
            "$Rさーて、今日は何を頼もうかな？$R;", "メニュー選びの男");
        }
    }
}