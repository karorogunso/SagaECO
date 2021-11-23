using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023000
{
    public class S13000294 : Event
    {
        public S13000294()
        {
            this.EventID = 13000294;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "ここのケーキはおいしいねぇ。$R;" +
            "病み付きになっちゃうよ。$R;" +
            "$Pあぁ……。$R;" +
            "毎日食べていたいよ。$R;", "ケーキ好きの男");
        }
    }
}