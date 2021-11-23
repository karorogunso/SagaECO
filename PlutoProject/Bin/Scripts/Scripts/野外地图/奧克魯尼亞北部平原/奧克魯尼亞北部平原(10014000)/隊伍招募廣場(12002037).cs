using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:隊伍招募廣場(12002037) X:85 Y:119
namespace SagaScript.M10014000
{
    public class S12002037 : Event
    {
        public S12002037()
        {
            this.EventID = 12002037;
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
