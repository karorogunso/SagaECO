using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000314 : Event
    {
        public S11000314()
        {
            this.EventID = 11000314;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "来，可爱的孩子们$R;" +
                "想听什么故事啊？$R;");
            Say(pc, 11000318, 131, "有公主的故事？$R;");
            Say(pc, 11000315, 131, "那个没意思呢$R;" +
                "给我们讲击退魔物的那种$R;" +
                "刺激的故事吧$R;");
            Say(pc, 131, "想听什么故事呢$R;");
            switch (Select(pc, "听什么故事呢？", "", "火焰凤凰的传说", "不听"))
            {
                case 1:
                    Say(pc, 11000314, 131, "有种样子虽然像火焰凤凰$R但不是火焰凤凰的人。$R;" +
                        "只要得到他翅膀的人就可以敞开$R;" +
                        "$R火焰凤凰走的路。$R;" +
                        "$P这就是从古代开始传下来$R;" +
                        "关于火焰凤凰的传说啦$R;");
                    break;
            }
        }
    }
}