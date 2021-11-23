using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:隊伍招募廣場(12002034) X:136 Y:88
namespace SagaScript.M10031000
{
    public class S12002034 : Event
    {
        public S12002034()
        {
            this.EventID = 12002034;
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
