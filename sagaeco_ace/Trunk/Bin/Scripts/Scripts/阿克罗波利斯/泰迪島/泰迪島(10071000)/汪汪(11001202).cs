using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:汪汪(11001202) X:186 Y:163
namespace SagaScript.M10071000
{
    public class S11001202 : Event
    {
        public S11001202()
        {
            this.EventID = 11001202;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000722, 131, "这小家伙身上穿的，$R;" +
                                   "是从「泰迪」那裡拿的衣服唷!$R;", "小鬼嘉布利");

            ShowEffect(pc, 10005, 4516);
        }
    }
}
