using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:初階冒險者(11000932) X:61 Y:57
namespace SagaScript.M10018001
{
    public class S11000932 : Event
    {
        public S11000932()
        {
            this.EventID = 11000932;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000932, 0, "跟前輩學了很多東西。$R;" +
                                 "$R您要一起學嗎?$R;", "初階冒險者");
        }
    }
}
