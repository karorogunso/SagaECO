using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000410 : Event
    {
        public S11000410()
        {
            this.EventID = 11000410;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];
            if (mogenmogen_mask.Test(mogenmogen.接受请求) && !mogenmogen_mask.Test(mogenmogen.与歷卡对话))
            {
                mogenmogen_mask.SetValue(mogenmogen.与歷卡对话, true);
                Say(pc, 11000410, 361, "哞?$R;" +
                    "哞~哞!哞~~!$R;");
                Say(pc, 0, 131, pc.Name + "在嗅氣味$R;" +
                    "$R看來是聞到了摩根摩根的味道了$R;");
                Say(pc, 11000410, 361, "哞嗚~!$R;");
                Say(pc, 0, 131, "歷卡好像在說…$R;" +
                    "$P……感謝阿$R;");
                return;
            }
            if (CountItem(pc, 10006850) >= 1)
            {
                Say(pc, 11000410, 361, "哞~哞~~!$R;");
                switch (Select(pc, "要給青菜嗎?", "", "因爲可愛才給", "不給"))
                {
                    case 1:
                        Say(pc, 11000410, 361, "哞~!$R;");
                        TakeItem(pc, 10006850, 1);
                        Say(pc, 0, 131, "『青菜』吃的好香啊?!$R;");
                        mask.SetValue(DFHJFlags.给过青菜, true);
                        break;
                    case 2:
                        Say(pc, 11000410, 111, "哞…$R;");
                        break;
                }
                return;
            }
            Say(pc, 11000410, 111, "……$R;");
        }
    }
}