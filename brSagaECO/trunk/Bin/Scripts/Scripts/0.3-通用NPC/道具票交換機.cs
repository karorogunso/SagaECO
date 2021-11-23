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
                                      "交換前先確認一下$R;" +
                                      "重量/體積/所持數量，$R;" +
                                      "以最輕便的狀態交換吧。$R;" +
                                      "$R因為重量/體積/所持數量$R;" +
                                      "超過標準而無法收下的道具，$R;" +
                                      "道具票是不會復原的，$R;" +
                                      "請多留意啊!$R;", " ");

                    //尚未製作輸入視窗
                    Say(pc, 0, 65535, "目前尚未實裝$R;", " ");

                    Say(pc, 0, 65535, "輸入編號不正確!$R;" +
                                      "$R請再次輸入編號。$R;", " ");
                    break;

                case 2:
                    Say(pc, 0, 65535, "「道具票交換機」就是用『道具票』，$R;" +
                                      "交換票上所寫的道具的機器。$R;" +
                                      "$P把您有的道具票，$R;" +
                                      "往上面紅色箭頭部分塞進去，$R;" +
                                      "小心不要折彎，放進去就好了。$R;" +
                                      "$P然後輸入票上印有的『編號』，$R;" +
                                      "然後在機械裡的玻璃箱，$R;" +
                                      "領取道具就可以了。$R;", " ");
                    break;

                case 3:
                    break;
            }
        }
    }
}
