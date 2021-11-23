using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S11001204 : Event
    {
        public S11001204()
        {
            this.EventID = 11001204;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……♪$R;");
            Say(pc, 11000536, 131, "這孩子是曼陀蘿胡蘿蔔!$R;" +
                "$R是用安全又安心的無農藥飼料栽培出來的$R是非常非常健康的孩子♪$R;");
        }
    }
}
