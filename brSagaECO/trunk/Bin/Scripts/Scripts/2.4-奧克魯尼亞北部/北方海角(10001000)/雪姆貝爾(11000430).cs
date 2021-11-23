using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000430 : Event
    {
        public S11000430()
        {
            this.EventID = 11000430;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10001804) >= 1)
            {
                Say(pc, 361, "咕咕~!$R;");
                switch (Select(pc, "要給能量水嗎?", "", "因爲太可愛了，所以給!", "不給"))
                {
                    case 1:
                        Say(pc, 361, "咕~~!$R;");
                        TakeItem(pc, 10001804, 1);
                        Say(pc, 131, "吃『能量水』吃的好香啊!$R;");
                        break;
                    case 2:
                        Say(pc, 111, "咕……$R;");
                        break;
                }
                return;
            }
            Say(pc, 111, "……$R;");
        }
    }
}
