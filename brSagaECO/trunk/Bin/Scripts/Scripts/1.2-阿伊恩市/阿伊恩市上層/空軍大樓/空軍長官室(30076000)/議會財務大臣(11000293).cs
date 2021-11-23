using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30076000
{
    public class S11000293 : Event
    {
        public S11000293()
        {
            this.EventID = 11000293;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "從咖啡館裡來的這個請求書$R;" +
                "到底是什麼？$R;" +
                "$R這個是不容許的阿$R;");
            Say(pc, 11000292, 131, "現在不能飛，也沒什麼可以做$R;" +
                "$R幫幫忙吧，這樣也不行嗎?$R;");
        }
    }
}