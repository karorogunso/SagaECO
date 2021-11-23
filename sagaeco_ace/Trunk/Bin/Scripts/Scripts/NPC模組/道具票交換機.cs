using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 道具票交換機 : Event
    {
        public 道具票交換機()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);

            switch (Select(pc, "歡迎光臨", "", "交換道具票", "查看使用方法", "什麼都不做"))
            {
                case 1:
                    Say(pc, 0, 65535, "等一下!$R;" +
                                      "交换前先确认一下$R;" +
                                      "重量/体积/所持数量，$R;" +
                                      "以最轻便的状态交换吧。$R;" +
                                      "$R因为重量/体积/所持数量$R;" +
                                      "超过标准而无法收下的道具，$R;" +
                                      "道具票是不会复原的，$R;" +
                                      "请多留意啊!$R;", " ");

                    //尚未製作輸入視窗
                    Say(pc, 0, 65535, "目前尚未实装$R;", " ");

                    Say(pc, 0, 65535, "输入编号不正确!$R;" +
                                      "$R请再次输入编号。$R;", " ");
                    break;

                case 2:
                    Say(pc, 0, 65535, "「道具票交换机」就是用『道具票』，$R;" +
                                      "交换票上所写的道具的机器。$R;" +
                                      "$P把您有的道具票，$R;" +
                                      "往上面的红色箭头部分塞进去，$R;" +
                                      "小心不要折弯，放进去就好了。$R;" +
                                      "$P然后输入票上印有的『编号』，$R;" +
                                      "最后在机器里的玻璃箱，$R;" +
                                      "领取道具就可以了。$R;", " ");
                    break;

                case 3:
                    break;
            }
        }
    }
}
