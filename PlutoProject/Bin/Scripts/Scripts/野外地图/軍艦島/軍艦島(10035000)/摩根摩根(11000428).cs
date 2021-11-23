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
                switch (Select(pc, "要给蜂蜜吗?", "", "因为太可爱了，所以给!", "不给"))
                {
                    case 1:
                        Say(pc, 361, "咕咕咕咕！$R;");
                        TakeItem(pc, 10035400, 1);
                        Say(pc, 131, "吃『蜂蜜』吃的好香啊!$R;");
                        break;
                    case 2:
                        Say(pc, 111, "呜呜呜……$R;");
                        break;
                }
                return;
            }
            Say(pc, 111, "……$R;");
        }
    }
}