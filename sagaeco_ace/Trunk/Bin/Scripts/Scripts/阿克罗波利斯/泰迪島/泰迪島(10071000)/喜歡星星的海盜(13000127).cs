using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:喜歡星星的海盜(13000127) X:33 Y:92
namespace SagaScript.M10071000
{
    public class S13000127 : Event
    {
        public S13000127()
        {
            this.EventID = 13000127;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "世界で、いっちばん大きな星を$R;" +
            "この夜空に打ち上げるぞー！$R;" +
            "$Rさあ、みんなで１等星を集めよー！$R;", "星が好きなパイレーツ");
            if (Select(pc, "どうするのー？", "", "１等星を100個渡す", "何もしない") == 1)
            {
                if (CountItem(pc, 10013107) >= 100)
                {
                    TakeItem(pc, 10013107, 100);
                    GiveItem(pc, 60082500, 1);
                }
            }

        }
    }
}