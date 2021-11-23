using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:隊伍招募廣場(12002032) X:85 Y:136
namespace SagaScript.M10022000
{
    public class S12002032 : Event
    {
        public S12002032()
        {
            this.EventID = 12002032;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "这地方是各位战士，$R;" +
                          "招募队友的广场。$R;" +
                          "$R组成一个合作无间的……$R;" +
                          "$R　　　　　　　　　　『行会评议会』$R;", " ");
        }
    }
}
