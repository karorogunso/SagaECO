using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:飛空庭的庭院(30201000) NPC基本信息:舵輪(11000975) X:7 Y:13
namespace SagaScript.M30201000
{
    public class S11000975 : Event
    {
        public S11000975()
        {
            this.EventID = 11000975;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000938, 131, "那个是飞空庭的「舵轮」呀!$R;" +
                                   "$R从飞空庭下来，$R;" +
                                   "或改造飞空庭时使用的。$R;" +
                                   "$R放心吧，$R;" +
                                   "除了本人以外，不会让别人操作的。$R;", "玛莎");
        }
    }
}
