using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10018000
{
    public class S11000093 : Event
    {
        public S11000093()
        {
            this.EventID = 11000093;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1: Say(pc, 131, "很快就要到达法伊斯特了$R;" +
                     "虽然外表看起来很和平$R;" +
                     "但不小心的话，也是会出意外的$R;");
                    break;
                case 2:
                    Say(pc, 131, "阿克罗波利斯最近怎么样?$R;" +
                        "如果继续待在这里的话$R;" +
                        "我都快觉得自己变成孤儿了$R;");
                    break;
            }
        }
    }
}
