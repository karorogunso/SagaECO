using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:唐卡快線職員(11001335) X:160 Y:192
namespace SagaScript.M10023000
{
    public class S11001335 : Event
    {
        public S11001335()
        {
            this.EventID = 11001335;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
