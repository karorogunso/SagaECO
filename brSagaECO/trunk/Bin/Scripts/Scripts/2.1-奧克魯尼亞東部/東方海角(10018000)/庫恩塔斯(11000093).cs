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
                case 1: Say(pc, 131, "很快就要到達帕斯特了$R;" +
                     "雖然外表看起來很和平$R;" +
                     "但不小心的話，也是會出意外的$R;");
                    break;
                case 2:
                    Say(pc, 131, "阿高普路斯最近怎麽樣?$R;" +
                        "如果繼續待在這裡的話$R;" +
                        "我都快覺得自己變成孤兒了$R;");
                    break;
            }
        }
    }
}
