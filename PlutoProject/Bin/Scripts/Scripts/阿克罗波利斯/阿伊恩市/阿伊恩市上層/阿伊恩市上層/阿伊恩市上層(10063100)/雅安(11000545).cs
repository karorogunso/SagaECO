using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000545 : Event
    {
        public S11000545()
        {
            this.EventID = 11000545;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            Say(pc, 131, "欢迎来到矿产都市$R;" +
                "『艾恩萨乌斯』。$R;" +
                "$R这个都市分成$R;" +
                "上层部和下层部两部份$R;");
            switch (Select(pc, "希望我给您介绍哪个部分呢？", "", "下层", "商业区", "合同大厦", "不用了"))
            {
                case 1:
                    Say(pc, 131, "往前走一点，$R;" +
                        "从阶梯下去，就是下层部了。$R;");
                    break;
                case 2:
                    Say(pc, 131, "商业区就在眼前所见，$R;" +
                        "高楼林立的地方$R;" +
                        "有很多裁缝和宝石商开的店呢。$R;" +
                        "下层部也有商店，$R;" +
                        "也得去看看啊。$R;");
                    break;
                case 3:
                    Say(pc, 131, "合同大厦就是前面$R;" +
                        "很大的那座建筑。$R;" +
                        "$R跟着箭头走吧。$R;");
                    Navigate(pc, 127, 103);
                    break;
                case 4:
                    Say(pc, 131, "这个都市很大，$R;" +
                        "小心不要迷路啊。$R;");
                    break;
            }
        }
    }
}