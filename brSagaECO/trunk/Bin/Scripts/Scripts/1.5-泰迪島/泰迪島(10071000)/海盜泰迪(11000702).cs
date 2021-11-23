using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000702) X:128 Y:90
namespace SagaScript.M10071000
{
    public class S11000702 : Event
    {
        public S11000702()
        {
            this.EventID = 11000702;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000702, 131, "從這裡開始是我們$R;" +
                                   "「海盜泰迪」的地盤呀!$R;" + 
                                   "$R發誓效忠於我們嗎?$R;", "海盜泰迪");

            switch (Select(pc, "發誓效忠於我們嗎?", "", "發誓忠誠", "不發誓"))
            {
                case 1:
                    Say(pc, 11000702, 131, "好的…但還是不能通過!$R;", "海盜泰迪");
                    break;

                case 2:
                    Say(pc, 11000702, 131, "不能通過，$R;" +
                                           "絕對不行!!$R;", "海盜泰迪");
                    break;
            }
        }
    }
}




