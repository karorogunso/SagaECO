using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:飛空庭的庭院(30201001) NPC基本信息:舵輪(11001436) X:7 Y:13
namespace SagaScript.M30201001
{
    public class S11001436 : Event
    {
        public S11001436()
        {
            this.EventID = 11001436;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001435, 131, "那個是飛空庭的「舵輪」呀!$R;" +
                                   "$R從飛空庭下來，$R;" +
                                   "或改造飛空庭時使用的。$R;" +
                                   "$R放心吧，$R;" +
                                   "除了本人以外，不會讓別人操作的。$R;", "瑪莎");
        }
    }
}
