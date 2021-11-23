using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000321 : Event
    {
        public S11000321()
        {
            this.EventID = 11000321;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            int selection;
            Say(pc, 131, "我们是收发部队。$R;" +
                "知道关于这个城市的所有资料。$R;");
            selection = Select(pc, "要介绍吗？", "", "武器商店", "酒馆", "鉴定所", "商店", "铁匠铺", "古董商店", "下一页", "算了");
            while (selection != 8)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "旁边那个大入口的商店$R;" +
                            "就是武器商店$R;");
                        return;
                    case 2:
                        Say(pc, 131, "现在介绍酒馆吧$R;" +
                            "$R箭头方向就是酒馆$R;");
                        Navigate(pc, 135, 145);
                        return;
                    case 3:
                        Say(pc, 131, "现在介绍鉴定所吧$R;" +
                            "箭头方向就是鉴定所$R;");
                        Navigate(pc, 139, 145);
                        return;
                    case 4:
                        Say(pc, 131, "现在介绍商店吧$R;" +
                            "箭头方向就是商店阿$R;");
                        Navigate(pc, 121, 156);
                        return;
                    case 5:
                        Say(pc, 131, "现在介绍铁匠铺吧$R;" +
                            "箭头方向就是铁匠铺$R;");
                        Navigate(pc, 135, 62);
                        return;
                    case 6:
                        Say(pc, 131, "现在介绍古董商店吧$R;" +
                            "箭头方向就是古董商店$R;");
                        Navigate(pc, 139, 62);
                        return;
                    case 7:
                        switch (Select(pc, "要介绍吗？", "", "钢铁工厂", "大工厂", "南方地牢", "动力控制中心", "返回"))
                        {
                            case 1:
                                Say(pc, 131, "现在介绍钢铁工厂$R;" +
                                    "有两个入口，两边都可以进去$R;" +
                                    "$R箭头方向就是钢铁工厂啊$R;");
                                Navigate(pc, 155, 59);
                                return;
                            case 2:
                                Say(pc, 131, "现在介绍大工厂$R;" +
                                    "有两个入口，两边都可以进去$R;" +
                                    "箭头方向就是大工厂啊$R;");
                                Navigate(pc, 156, 148);
                                return;
                            case 3:
                                Say(pc, 131, "现在介绍南部地牢$R;" +
                                    "有两个入口，两边都可以进去$R;" +
                                    "箭头方向就是南部地牢喔$R;");
                                Navigate(pc, 200, 81);
                                return;
                            case 4:
                                Say(pc, 131, "对不起$R;" +
                                    "动力控制中心$R;" +
                                    "一般人不可以进去啊$R;");
                                return;
                        }
                        selection = Select(pc, "要介绍吗？", "", "武器商店", "酒馆", "鉴定所", "商店", "铁匠铺", "古董商店", "下一页", "算了");
                        break;
                }
            }
        }
    }
}