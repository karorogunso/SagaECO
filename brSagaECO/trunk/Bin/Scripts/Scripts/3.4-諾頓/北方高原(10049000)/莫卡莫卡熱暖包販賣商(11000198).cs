using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10049000
{
    public class S11000198 : Event
    {
        public S11000198()
        {
            this.EventID = 11000198;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BFGYFlags> mask = new BitMask<BFGYFlags>(pc.CMask["BFGY"]);
            //EVT11000198
            if (!mask.Test(BFGYFlags.莫卡莫卡熱暖包販賣商第一次對話))
            {
                Say(pc, 131, "嘿啾…哎呀…冷死了$R;" +
                    "哎呀！來客人了！買不買諾頓特產$R『暖呼呼的紅色暖包』呢？$R;" +
                    "$P只要有『暖呼呼的紅色暖包』$R在這個寒冷的諾頓$R也可以過的很溫暖唷。$R;" +
                    "$R我也是一直用它，要不然會凍僵的喔$R;");
                mask.SetValue(BFGYFlags.莫卡莫卡熱暖包販賣商第一次對話, true);
                return;
            }
            switch (Select(pc, "歡迎光臨…冷死了", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 79);
                    break;
                case 2:
                    OpenShopSell(pc, 79);
                    break;
                case 3:
                    break;
            }
        }
    }
}
