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
                switch (Select(pc, "给卷心菜？", "", "给", "不给"))
                {
                    case 1:
                        TakeItem(pc, 10051600, 1);
                        Say(pc, 11000637, 361, "咕噜咕噜$R;");
                        Say(pc, 0, 131, "吃掉了『卷心菜』$R;");
                        if (mogenmogen_mask.Test(mogenmogen.接受请求) && !mogenmogen_mask.Test(mogenmogen.与莫波对话))
                        {
                            mogenmogen_mask.SetValue(mogenmogen.与莫波对话, true);
                            Say(pc, 11000637, 361, "咕噜咕噜?$R;" +
                                "咕噜?咕噜…$R;");
                            Say(pc, 0, 131, pc.Name + "在嗅气味$R;" +
                                "$R在您这里好像闻到摩戈摩戈的味道$R;");
                            Say(pc, 11000637, 361, "克噜噜噜噜$R;");
                            Say(pc, 0, 131, "吃得很开心的小白好像在致谢呢$R;");
                            return;
                        }
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 0, 131, "肚子饿了吧$R;" +
                "看都不想看这一边啊$R;");
            Say(pc, 11000638, 131, "那家伙喜欢『卷心菜』$R;" +
                "$R没有卷心菜的话，谁都叫不动$R;" +
                "真是令人头痛的爬爬虫$R;");
        }
    }
}