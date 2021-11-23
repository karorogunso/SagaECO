using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000144 : Event
    {
        public S11000144()
        {
            this.EventID = 11000144;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
                Say(pc, 11000144, 131, "这里是大家的广场$R;" +
                    "长老也在里面喔$R;");

            Say(pc, 11000144, 131, "最近伙伴们都去冒险了。$R;" +
                "聚集地的人口减少了$R;" +
                "$R希望您叫伙伴们，多回来看看喔$R;");
        }
    }
}