using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊1(30180000) NPC基本信息:劇場小姐(11000160) X:1 Y:10
namespace SagaScript.M30180000
{
    public class S11000160 : Event
    {
        public S11000160()
        {
            this.EventID = 11000160;
            AddGoods(10047700);
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎光临$R;");
            switch(Select(pc,"要怎么做呢？","","查看影院播放列表","购买电影票"))
            {
                case 1:
                    ShowTheaterSchedule(pc, 30190000);
                    break;
                case 2:
                    OpenShopBuy(pc);
                    break;
            }
        }
    }
}
