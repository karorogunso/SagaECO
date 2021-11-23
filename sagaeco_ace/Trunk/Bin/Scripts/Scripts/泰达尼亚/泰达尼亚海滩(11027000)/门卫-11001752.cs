using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:泰达尼亚海滩(11027000)NPC基本信息:11001752-门卫- X:123 Y:111
namespace SagaScript.M11027000
{
    public class S11001752 : Event
    {
    public S11001752()
        {
            this.EventID = 11001752;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
