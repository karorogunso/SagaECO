using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000637 : Event
    {
        public S11000637()
        {
            this.EventID = 11000637;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];
            if (CountItem(pc, 10051600) >= 1)
            {
                switch (Select(pc, "給洋白菜？", "", "給", "不給"))
                {
                    case 1:
                        TakeItem(pc, 10051600, 1);
                        Say(pc, 11000637, 361, "咯嚕咯嚕$R;");
                        Say(pc, 0, 131, "吃掉了『洋白菜』$R;");
                        if (mogenmogen_mask.Test(mogenmogen.接受请求) && !mogenmogen_mask.Test(mogenmogen.与莫波对话))
                        {
                            mogenmogen_mask.SetValue(mogenmogen.与莫波对话, true);
                            Say(pc, 11000637, 361, "咯嚕咯咯?$R;" +
                                "咯嚕?咯嚕…$R;");
                            Say(pc, 0, 131, pc.Name + "在嗅氣味$R;" +
                                "$R在您這裡好像聞到摩根摩根的味道$R;");
                            Say(pc, 11000637, 361, "克嚕嚕嚕嚕$R;");
                            Say(pc, 0, 131, "吃得很開心的小白好像在致謝呢$R;");
                            return;
                        }
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 0, 131, "肚子餓了吧$R;" +
                "瞧也不想瞧瞧這一邊阿$R;");
            Say(pc, 11000638, 131, "那傢伙喜歡『洋白菜』$R;" +
                "$R没有洋白菜的話，誰都叫不動$R;" +
                "真是令人頭痛的爬爬蟲$R;");
        }
    }
}