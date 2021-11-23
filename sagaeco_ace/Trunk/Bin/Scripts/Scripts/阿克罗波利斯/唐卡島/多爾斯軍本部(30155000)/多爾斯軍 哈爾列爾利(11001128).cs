using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30155000
{
    public class S11001128 : Event
    {
        public S11001128()
        {
            this.EventID = 11001128;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "啊？这么快就到了替换工作时间了？$R;" +
                    "$P哎呀…$R原来是过路的活动木偶呀，…$R;" +
                    "$R唉……$R不知道什么时候替换工作呀。$R;" +
                    "我想回家看书啊…$R;");
                return;
            }
            Say(pc, 131, "这里是活动木偶保管仓库，$R;" +
                "$R相关人员以外禁止出入，请回吧。$R;");
        }
    }
}