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
                Say(pc, 11000144, 131, "這裡是大家的廣場$R;" +
                    "長老也在裡面喔$R;");

            Say(pc, 11000144, 131, "最近夥伴們都去冒險了。$R;" +
                "聚集地的人口減少了$R;" +
                "$R希望您叫夥伴們，多回來看看喔$R;");
        }
    }
}