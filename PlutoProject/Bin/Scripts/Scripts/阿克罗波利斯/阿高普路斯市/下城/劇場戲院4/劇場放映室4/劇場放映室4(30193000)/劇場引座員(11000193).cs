using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場放映室4(30193000) NPC基本信息:劇場引座員(11000193) X:11 Y:23
namespace SagaScript.M30193000
{
    public class S11000193 : Event
    {
        public S11000193()
        {
            this.EventID = 11000193;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "超过了入场时间不能进入呀，$R;" +
                         "得等下一个上映时间喔$R;" +
                          "$R还要回到大厅吗？$R;");
            switch (Select(pc, "回到大厅吗？ ", "", "不回去", "回去"))
            {
                case 2:
                    Warp(pc, 30181000, 9, 2);
                    break;
            }
        }
    }
}
