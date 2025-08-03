using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:隊伍招募廣場(12002030) X:85 Y:119
namespace SagaScript.M10025000
{
    public class S12002030 : Event
    {
        public S12002030()
        {
            this.EventID = 12002030;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "这地方是你们各位战士，$R;" +
                          "召募队友的广场。$R;" +
                          "$R组成一个合作无间的……$R;" +
                          "$R　　　　　　　　　　『行会评议会』$R;", " ");
        }
    }
}
