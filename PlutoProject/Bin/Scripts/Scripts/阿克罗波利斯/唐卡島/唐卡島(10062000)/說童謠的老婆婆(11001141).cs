using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001141 : Event
    {
        public S11001141()
        {
            this.EventID = 11001141;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "呵呵呵……$R;" +
                "$R这个石像非常神奇呀？$R;" +
                "$R这是『皮诺』的石像$R;" +
                "给您说说关于皮诺的故事吗？$R;");
            switch (Select(pc, "听故事吗？", "", "不听", "我想听故事"))
            {
                case 1:
                    break;
                case 2:
                    Say(pc, 131, "很久很久以前，这里有一棵树喔$R;" +
                        "$P这棵树很神奇的，还会说话呢，$R;" +
                        "$P唐卡人非常珍惜这棵树，$R;" +
                        "这棵树好像也明白似的，$R一直守护著唐卡。$R;" +
                        "$P不过有一天，$R一个国家的国王说『这太珍贵了』$R;" +
                        "就把树给砍走了$R;" +
                        "$P悲伤的唐卡人，用那棵树的树枝$R做成了人形木偶，$R;" +
                        "$P木偶完成后，$R从天上飞来有著翅膀的女神说：$R;" +
                        "$P『答应我，好好珍惜这个木偶，$R我就赋予这个孩子生命吧。』$R;" +
                        "$P唐卡人非常高兴地答应了，$R;" +
                        "$P他们给那赋予生命的木偶$R;" +
                        "起名叫『皮诺』$R;" +
                        "意思就是永远幸福生活的意思，$R故事就是这样，$R;" +
                        "$P最后怎么样了？$R;" +
                        "故事已经讲完了吗?$R;" +
                        "$R不，这才是皮诺冒险历程的开始，$R;" +
                        "说来话长，$R;" +
                        "$R以后给您讲吧。$R;");
                    break;
            }
        }
    }
}