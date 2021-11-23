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
                "$R這個石像非常神奇呀？$R;" +
                "$R這是『皮諾』的石像$R;" +
                "給您說說關於皮諾的故事嗎？$R;");
            switch (Select(pc, "聽故事嗎？", "", "不聽", "我想聽故事"))
            {
                case 1:
                    break;
                case 2:
                    Say(pc, 131, "很久很久以前，這裡有一棵樹喔$R;" +
                        "$P這棵樹很神奇的，還會說話呢，$R;" +
                        "$P唐卡人非常珍惜這棵樹，$R;" +
                        "這棵樹好像也明白似的，$R一直守護著唐卡。$R;" +
                        "$P不過有一天，$R一個國家的國王說『這太珍貴了』$R;" +
                        "就把樹給砍走了$R;" +
                        "$P悲傷的唐卡人，用那棵樹的樹枝$R做成了人形木偶，$R;" +
                        "$P木偶完成後，$R從天上飛來有著翅膀的女神說：$R;" +
                        "$P『答應我，好好珍惜這個木偶，$R我就賦予這個孩子生命吧。』$R;" +
                        "$P唐卡人非常高興地答應了，$R;" +
                        "$P他們給那賦予生命的木偶$R;" +
                        "起名叫『皮諾』$R;" +
                        "意思就是永遠幸福生活的意思，$R故事就是這樣，$R;" +
                        "$P最後怎麼樣了？$R;" +
                        "故事已經講完了嗎?$R;" +
                        "$R不，這才是皮諾冒險歷程的開始，$R;" +
                        "說來話長，$R;" +
                        "$R以後給您講吧。$R;");
                    break;
            }
        }
    }
}