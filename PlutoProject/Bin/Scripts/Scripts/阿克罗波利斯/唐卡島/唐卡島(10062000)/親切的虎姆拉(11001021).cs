using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001021 : Event
    {
        public S11001021()
        {
            this.EventID = 11001021;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10000305) >= 1)
            {
                Say(pc, 131, "啊，那个是不是$R;" +
                    "『健康营养饮料』呀？$R;" +
                    "$R不好意思，能不能给我一个呢？$R;");
                switch (Select(pc, "怎么办呢？", "", "不给", "好阿"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10018301, 1))
                        {
                            TakeItem(pc, 10000305, 1);
                            GiveItem(pc, 10018301, 1);
                            Say(pc, 131, "谢谢$R;" +
                                "这是表示感谢的礼物，$R;" +
                                "请您收下$R;");
                            Say(pc, 0, 131, "嚯嚯！$R;");
                            Say(pc, 131, "您不知道吗？$R;" +
                                "我的翅膀能变成金。$R;" +
                                "$R如果不会使用可以问别人喔。$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『炎之翼』$R;");
                            return;
                        }
                        Say(pc, 131, "谢谢$R;" +
                            "想向您道谢，$R;" +
                            "不过行李看起来太多了。$R;" +
                            "不好意思，能不能减少一点行李后$R;" +
                            "再来找我呢？$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "这个身体曾经带着我环游世界，$R;" +
                    "可是时光飞逝啊，$R;" +
                    "$R能不能赏我『健康营养饮料』呢？$R;");
                return;
            }
            Say(pc, 131, "现在还剩一点，$R;" +
                "加油啊$R;");
        }
    }
}