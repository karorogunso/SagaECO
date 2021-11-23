using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000902 : Event
    {
        public S11000902()
        {
            this.EventID = 11000902;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (pc.Fame < 10)
            {
                Say(pc, 131, "為了調查最近發現的遺跡，$R;" +
                    "諾頓正在召集調查隊$R;" +
                    "$R對您來說，這件事情有點困難$R;" +
                    "$P想參加調查隊，$R;" +
                    "先增強力量後再來吧。$R;");
                return;
            }
            if (mask.Test(NDFlags.协助) && mask.Test(NDFlags.遗迹))
            {
                Say(pc, 131, "調查需要很長時間$R;" +
                    "在那期間這裡會開放的，$R;" +
                    "請協助我們調查吧。$R;");
                return;
            }
            Say(pc, 131, "諾頓正在召集調查隊，$R;" +
                "調查最近發現的遺跡$R;" +
                "$R不怕危險的話，$R;" +
                "請參加吧。$R;");
            switch (Select(pc, "參加嗎？", "", "不參加", "遺跡？"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(NDFlags.遗迹, true);
                    Say(pc, 131, "這是一個非常龐大的遺跡$R;" +
                        "各國競爭調查遺跡，$R;" +
                        "現在已經有好幾個調查隊$R;" +
                        "探測遺跡內部阿$R;" +
                        "$P雖然我們不願意，$R;" +
                        "但被派入調查隊跟男士們一起調查$R;" +
                        "有興趣就參加我們調查隊協助我們吧$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "協助", "拒絕"))
                    {
                        case 1:
                            mask.SetValue(NDFlags.协助, true);
                            Say(pc, 131, "準備完後，到這個房間吧$R;" +
                                "我用移動魔法$R;" +
                                "$R把您送到遺跡入口喔。$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}
