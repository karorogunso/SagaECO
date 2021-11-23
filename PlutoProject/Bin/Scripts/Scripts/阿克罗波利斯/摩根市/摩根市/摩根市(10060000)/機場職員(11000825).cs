using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000825 : Event
    {
        public S11000825()
        {
            this.EventID = 11000825;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];

            NavigateCancel(pc);

            Say(pc, 131, "这里是商人机场$R;" +
                "$P从这里乘坐飞空庭可以到光之塔吧$R;" +
                "想搭乘飞空庭吗？$R;");

            switch (Select(pc, "搭乘飞空庭吗？", "", "搭乘", "不要"))
            {
                case 1:
                    if (TravelFGarden_mask.Test(TravelFGarden.已经买票))
                    {
                        Say(pc, 131, "那么就直接搭乘吧$R;" +
                            "到时间就出发了$R;");
                        return;
                    }

                    Say(pc, 131, "那么，$R在商人行会总部那里买机票吧$R;" +
                        "$R总部就在旁边，紫色屋顶的建筑物喔$R;");
                    Navigate(pc, 80, 152);
                    break;
                case 2:
                    break;
            }
        }
    }
}