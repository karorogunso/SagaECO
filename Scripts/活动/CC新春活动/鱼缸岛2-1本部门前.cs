
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S4000001 : Event
    {
        public S4000001()
        {
            this.EventID = 4000001;
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 3)//此段代码为进入鱼缸岛2-1本部触发用 ID4000001
                {
                    Say(pc, 0, "你推了推门，门丝纹不动");
                    while (Select(pc, "怎么办？", "", "撞门", "奥义·铁山靠！") != 2)
                    {
                        Say(pc, 0, "门还是丝纹不动……");
                        Say(pc, 0, "再试试？");
                    }
                    Say(pc, 0, "你似乎听到了从里面传来什么声音");
                    Say(pc, 0, "啊，好像能开了！", "番茄茄");
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 0, "你们一窝蜂涌进了迷之团本部");
                    Say(pc, 0, "！？");
                    Say(pc, 0, "一进门，一股刺鼻的气味扑面而来。");
                    Say(pc, 0, "我看见了……");
                    Warp(pc, 30131000, 6, 10);//鱼缸岛本部2-1
                    pc.CInt["CC新春活动"] = 4;
                    return;
                }
            }
            Warp(pc, 30131000, 6, 10);//鱼缸岛本部2-1
        }
    }
}