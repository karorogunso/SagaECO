using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10023000
{
    public class S13000219 : Event
    {
        public S13000219()
        {
            this.EventID = 13000219;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "そばにいらっしゃい。$R;" +
            "暖かいわよ。$R;", "サラマンドラ");
        }
    }
}