using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000143 : Event
    {
        public S11000143()
        {
            this.EventID = 11000143;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
                Say(pc, 11000143, 131, "飛魚!$R;" +
                    "是我們的偶像$R;" +
                    "$R真的很漂亮呢!$R;");

            Say(pc, 11000143, 131, "這裡的飛魚非常文靜$R;" +
                "$R不能打擾牠們唷$R;");
        }
    }
}