using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018003) NPC基本信息:初階冒險者(11001430) X:62 Y:59
namespace SagaScript.M10018003
{
    public class S11001430 : Event
    {
        public S11001430()
        {
            this.EventID = 11001430;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001430, 0, "不懂的东西太多了，$R;" +
                                 "一定认真学习哦!$R;" +
                                 "$R您要一起听吗?$R;", "初阶冒险者");
        }
    }
}
