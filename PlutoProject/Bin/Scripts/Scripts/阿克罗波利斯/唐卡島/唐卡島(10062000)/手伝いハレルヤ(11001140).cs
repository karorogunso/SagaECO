using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001140 : Event
    {
        public S11001140()
        {
            this.EventID = 11001140;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "管理者用", "", "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"))
                {
                    case 1:
                        OpenShopBuy(pc, 211);
                        break;
                    case 2:
                        OpenShopBuy(pc, 212);
                        break;
                    case 3:
                        OpenShopBuy(pc, 213);
                        break;
                    case 4:
                        OpenShopBuy(pc, 214);
                        break;
                    case 5:
                        OpenShopBuy(pc, 215);
                        break;
                    case 6:
                        OpenShopBuy(pc, 216);
                        break;
                    case 7:
                        OpenShopBuy(pc, 217);
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "嘻嘻$R;" +
                    "我们喜欢工作喔$R;");
                return;
            }
            Say(pc, 131, "有想买的东西就说一声吧。$R;" +
                "给您算便宜点！$R;");
            int wday = int.Parse(DateTime.Now.DayOfWeek.ToString("d"));
            switch (wday)
            {
                case 0:
                    OpenShopBuy(pc, 211);
                    break;
                case 1:
                    OpenShopBuy(pc, 212);
                    break;
                case 2:
                    OpenShopBuy(pc, 213);
                    break;
                case 3:
                    OpenShopBuy(pc, 214);
                    break;
                case 4:
                    OpenShopBuy(pc, 215);
                    break;
                case 5:
                    OpenShopBuy(pc, 216);
                    break;
                case 6:
                    OpenShopBuy(pc, 217);
                    break;
            }
            Say(pc, 131, "商品每天都会更换！！$R;");
        }
    }
}