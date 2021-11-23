using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001944-泰达尼亚冒险家- X:47 Y:4
namespace SagaScript.M31303000
{
    public class S11001944 : Event
    {
    public S11001944()
        {
            this.EventID = 11001944;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
