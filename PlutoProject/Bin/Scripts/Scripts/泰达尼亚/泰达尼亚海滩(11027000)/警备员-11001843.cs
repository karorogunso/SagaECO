using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:泰达尼亚海滩(11027000)NPC基本信息:11001843-警备员- X:182 Y:48
namespace SagaScript.M11027000
{
    public class S11001843 : Event
    {
    public S11001843()
        {
            this.EventID = 11001843;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
