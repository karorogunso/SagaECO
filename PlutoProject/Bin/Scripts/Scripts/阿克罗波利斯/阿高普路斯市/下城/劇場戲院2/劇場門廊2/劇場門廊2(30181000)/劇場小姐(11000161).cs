using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊2(30181000) NPC基本信息:劇場小姐(11000161) X:1 Y:10
namespace SagaScript.M30181000
{
    public class S11000161 : Event
    {
        public S11000161()
        {
            this.EventID = 11000161;
            AddGoods(10047700);
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎光临$R;");
            switch (Select(pc, "要怎么做呢？", "", "查看影院播放列表", "购买电影票"))
            {
                case 1:
                    ShowTheaterSchedule(pc, 30191000);
                    break;
                case 2:
                    OpenShopBuy(pc);
                    break;
            }
        }
    }
}
