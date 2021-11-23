using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000428 : Event
    {
        public S11000428()
        {
            this.EventID = 11000428;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10035400) >= 1)
            {
                Say(pc, 361, "咕~咕~！$R;");
                switch (Select(pc, "要給蜂蜜嗎?", "", "因爲太可愛了，所以給!", "不給"))
                {
                    case 1:
                        Say(pc, 361, "咕咕咕咕！$R;");
                        TakeItem(pc, 10035400, 1);
                        Say(pc, 131, "吃『蜂蜜』吃的好香啊!$R;");
                        break;
                    case 2:
                        Say(pc, 111, "呱嗚嗚……$R;");
                        break;
                }
                return;
            }
            Say(pc, 111, "……$R;");
        }
    }
}