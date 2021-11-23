using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（下层）(11024000)NPC基本信息:12001251-道具票交换机- X:182 Y:60
namespace SagaScript.M11024000
{
    public class S12001251 : Event
    {
    public S12001251()
        {
            this.EventID = 12001251;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
