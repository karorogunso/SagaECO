using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30156000
{
    public class S11001066 : Event
    {
        public S11001066()
        {
            this.EventID = 11001066;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "$R这间房间凉快点吧。$R;" +
                    "我跟前面那位火焰蜥蜴先生$R正在调温度。$R;");
                return;
            }
            Say(pc, 255, "……$R;");
        }
    }
}