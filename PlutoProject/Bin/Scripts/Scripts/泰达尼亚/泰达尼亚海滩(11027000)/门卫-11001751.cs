using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:泰达尼亚海滩(11027000)NPC基本信息:11001751-门卫- X:123 Y:103
namespace SagaScript.M11027000
{
    public class S11001751 : Event
    {
    public S11001751()
        {
            this.EventID = 11001751;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
