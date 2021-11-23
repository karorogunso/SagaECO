using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001053 : Event
    {
        public S11001053()
        {
            this.EventID = 11001053;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "市長先生，來玩吧~！$R;");
            Say(pc, 11001054, 131, "好，一起玩吧，$R;" +
                "$R正好天氣也好，$R;" +
                "等一會兒，去公園嗎?$R;");
            Say(pc, 131, "哇！！市長萬嵗！！$R;");
        }
    }
}