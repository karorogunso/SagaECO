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
            Say(pc, 131, "我們在約會呢，不要妨礙我們呀$R;");

        }
        void 情人节(ActorPC pc)
        {
            Say(pc, 131, "是親自做的巧克力蛋糕！$R;");
            Say(pc, 11000210, 131, "哇~真是太感謝您了$R;");
            Say(pc, 131, "（到了白色情人節，$R一定要收到好幾倍的禮物）$R;");
        }
        void 白色情人节(ActorPC pc)
        {
            Say(pc, 11000210, 131, "給您白色情人節的禮物吧$R;");
            Say(pc, 131, "哇…太謝謝了$R;" +
                "（以後得查一查價錢）$R;");
        }
    }
}