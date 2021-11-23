using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000395 : Event
    {
        public S11000395()
        {
            this.EventID = 11000395;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000391, 131, "我們是把強烈顏色$R;" +
                "標榜突擊的軍團隊員$R;");
            Say(pc, 11000391, 140, "熱血，隊長，紅懷特$R;");
            Say(pc, 11000392, 141, "一點紅的藍布$R;");
            Say(pc, 11000393, 140, "有意思的黃耶勞$R;");
            Say(pc, 11000394, 140, "天下壯士綠杰$R;");
            Say(pc, 11000395, 140, "孤單的狼黑佰特!$R;");
            Say(pc, 11000391, 131, "5個聚在一起顔色豐富的突擊隊員!$R;");
        }
    }
}