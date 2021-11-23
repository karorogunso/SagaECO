using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S12002005 : Event
    {
        public S12002005()
        {
            this.EventID = 12002005;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "往後面出吧！$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『肉』和『馬鈴薯』$R放在『水』裡煮$R;" +
                        "$R再放『香辛科』再煮一會兒$R;" +
                        "$R材料變軟就完成了$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
