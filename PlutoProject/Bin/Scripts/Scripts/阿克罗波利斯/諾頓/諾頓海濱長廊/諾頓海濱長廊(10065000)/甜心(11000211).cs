using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000211 : Event
    {
        public S11000211()
        {
            this.EventID = 11000211;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我们在约会呢，不要妨碍我们呀$R;");

        }
        void 情人节(ActorPC pc)
        {
            Say(pc, 131, "是亲自做的巧克力蛋糕！$R;");
            Say(pc, 11000210, 131, "哇~真是太感谢您了$R;");
            Say(pc, 131, "（到了白色情人节，$R一定要收到好几倍的礼物）$R;");
        }
        void 白色情人节(ActorPC pc)
        {
            Say(pc, 11000210, 131, "给您白色情人节的礼物吧$R;");
            Say(pc, 131, "哇…太谢谢了$R;" +
                "（以后得查一查价钱）$R;");
        }
    }
}