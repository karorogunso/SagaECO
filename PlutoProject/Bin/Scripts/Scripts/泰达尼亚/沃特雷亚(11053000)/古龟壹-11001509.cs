using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001509-古龟壹- X:23 Y:194
namespace SagaScript.M11053000
{
    public class S11001509 : Event
    {
    public S11001509()
        {
            this.EventID = 11001509;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "咪!!!!!!!!!!!!$R;");
            Say(pc, 0, "(哇!$R它在色眯眯的看着我的胖次...$R;");
        }
    }
}
