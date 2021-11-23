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
            Say(pc, 11000702, 131, "从这里开始是我们$R;" +
                                   "「海盗泰迪」的地盘呀!$R;" +
                                   "$R发誓效忠于我们吗?$R;", "海盗泰迪");

            switch (Select(pc, "发誓效忠于我们吗?", "", "发誓忠诚", "不发誓"))
            {
                case 1:
                    Say(pc, 11000702, 131, "好的…但还是不能通过!$R;", "海盗泰迪");
                    break;

                case 2:
                    Say(pc, 11000702, 131, "不能通过，$R;" +
                                           "绝对不行!!$R;", "海盗泰迪");
                    break;
            }
        }
    }
}




