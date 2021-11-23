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
                Say(pc, 131, "啊？這麼快就到了替換工作時間了？$R;" +
                    "$P哎呀…$R原來是過路的活動木偶呀，…$R;" +
                    "$R唉……$R不知道什麼時候替換工作呀。$R;" +
                    "我想回家看書啊…$R;");
                return;
            }
            Say(pc, 131, "這裡是活動木偶保管倉庫，$R;" +
                "$R相關人員以外禁止出入，請回吧。$R;");
        }
    }
}