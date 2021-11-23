using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:祭師前輩(18000042) X:168 Y:141
namespace SagaScript.M10024000
{
    public class S18000042 : Event
    {
        public S18000042()
        {
            this.EventID = 18000042;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
