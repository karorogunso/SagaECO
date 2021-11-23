using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000803 : Event
    {
        public S11000803()
        {
            this.EventID = 11000803;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            /*
            if (_2b32)
            {
                OpenShopBuy(pc, 221);
                return;
            }
            if (_xb31)
            {
                Say(pc, 135, "呵呵呵…$R;" +
                    "$R您對飛行帆有興趣嗎?$R;");
                switch (Select(pc, "對飛行帆有興趣嗎?", "", "有!有!", "沒有!"))
                {
                    case 1:
                        _2b32 = true;
                        Say(pc, 135, "那樣的話來的好!$R;" +
                            "$R要不要看我們的商品?$R當然是有品質保證的正牌貨!$R;" +
                            "$P雖然價格有點高!$R呵呵呵…$R;");
                        OpenShopBuy(pc, 221);
                        break;
                    case 2:
                        Say(pc, 135, "呵呵呵…$R;");
                        break;
                }
                return;
            }
            */
            if (mask.Test(AYEFlags.闇黑商人商店開放))//_6A14)
            {
                OpenShopBuy(pc, 159);
                return;
            }
            if (!fgarden.Test(FGarden.第一次和飛空庭匠人說話))
            {
                Say(pc, 135, "…$R;");
                return;
            }
            Say(pc, 135, "…$R;" +
                "$R对飞空庭有兴趣?来!$R;");
            switch (Select(pc, "对飞空庭有兴趣吗?", "", "有!", "没有!"))
            {
                case 1:
                    mask.SetValue(AYEFlags.闇黑商人商店開放, true);
                    //_6A14 = true;
                    Say(pc, 135, "请来看看!$R;" +
                        "$R良心价!童叟无欺$R;", "闇黑商人");
                       Say(pc, 0,0, "这人看起来被揍得很惨$R;");
                    OpenShopBuy(pc, 159);
                    break;
                case 2:
                    Say(pc, 135, "慢走!慢走!$R;");
                    break;
            }
        }
    }
}