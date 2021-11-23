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

            Say(pc, 131, "這裡是商人機場$R;" +
                "$P從這裡乘坐飛空庭可以到光之塔吧$R;" +
                "想搭乘飛空庭嗎？$R;");

            switch (Select(pc, "搭乘飛空庭嗎？", "", "搭乘", "不要"))
            {
                case 1:
                    if (TravelFGarden_mask.Test(TravelFGarden.已经买票))
                    {
                        Say(pc, 131, "那麼就直接搭乘吧$R;" +
                            "到時間就出發了$R;");
                        return;
                    }
                    
                    Say(pc, 131, "那麼，$R在商人行會總部那裡買機票吧$R;" +
                        "$R總部就在旁邊，紫色屋頂的建築物喔$R;");
                    Navigate(pc, 80, 152);
                    break;
                case 2:
                    break;
            }
        }
    }
}