using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:薄荷(13000067) X:52 Y:86
namespace SagaScript.M10024000
{
    public class S13000067 : Event
    {
        public S13000067()
        {
            this.EventID = 13000067;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
