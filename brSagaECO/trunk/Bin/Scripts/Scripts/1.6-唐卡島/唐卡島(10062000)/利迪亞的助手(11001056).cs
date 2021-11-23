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
    public class S11001056 : Event
    {
        public S11001056()
        {
            this.EventID = 11001056;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 11001022, 131, "那麼，一直在這裡行嗎？$R;");
                Say(pc, 131, "就是……$R;" +
                    "利迪阿在實驗中失敗…$R變得神經質了…$R;" +
                    "$R想從這裡回避一段時間$R咇咇…$R;");
                Say(pc, 11001022, 131, "啊，原來如此。$R;" +
                    "最好避開一下吧！$R;");
                return;
            }
            Say(pc, 131, "我們是在營運這個$R;" +
                "個人工作室的工匠助手！$R;" +
                "$R這附近的工匠都用自己製作的$R;" +
                "活動木偶，作為助手唷。$R;");
        }
    }
}