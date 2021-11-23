using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場放映室2(30191000) NPC基本信息:劇場引座員(11000192) X:11 Y:23
namespace SagaScript.M30191000
{
    public class S11000192 : Event
    {
        public S11000192()
        {
            this.EventID = 11000192;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "超過了入場時間不能進入呀，$R;" +
                         "得等下一個上映時間喔$R;" +
                          "$R還要回到大廳嗎？$R;");
            switch (Select(pc, "回到大廳嗎？ ", "", "不回去", "回去"))
            {
                case 2:
                    Warp(pc, 30180000, 9, 2);
                    break;
            }
        }
    }
}
