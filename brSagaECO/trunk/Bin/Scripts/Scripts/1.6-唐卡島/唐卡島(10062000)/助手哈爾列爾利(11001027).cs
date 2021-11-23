using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001027 : Event
    {
        public S11001027()
        {
            this.EventID = 11001027;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "這個城市的人，對我這樣的$R;" +
                    "活動木偶，也很親切唷。$R;");
                return;
            }
            Say(pc, 131, "這裡是飛空庭大工廠。$R;" +
                "有什麼事情，$R就去問站在那座建築物前面的$R漂亮女士吧。$R;");
        }
    }
}