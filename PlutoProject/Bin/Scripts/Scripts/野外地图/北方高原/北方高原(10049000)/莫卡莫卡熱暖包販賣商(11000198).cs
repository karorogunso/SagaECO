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
                    "哎呀！来客人了！买不买诺森特产$R『暖石』呢？$R;" +
                    "$P只要有『暖石』$R在这个寒冷的诺森$R也可以过的很温暖哦。$R;" +
                    "$R我也是一直用它，要不然会冻僵的喔$R;");
                mask.SetValue(BFGYFlags.莫卡莫卡熱暖包販賣商第一次對話, true);
                return;
            }
            switch (Select(pc, "欢迎光临…冷死了", "", "买东西", "卖东西", "什么也不做"))
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
