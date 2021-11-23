using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:隊伍招募廣場(12002031) X:85 Y:136
namespace SagaScript.M10025000
{
    public class S12002031 : Event
    {
        public S12002031()
        {
            this.EventID = 12002031;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "這地方是你們各位戰士，$R;" +
                          "召募隊友的廣場。$R;" +
                          "$R組成一個合作無間的……$R;" +
                          "$R　　　　　　　　　　『行會評議會』$R;", " ");
        }
    }
}
