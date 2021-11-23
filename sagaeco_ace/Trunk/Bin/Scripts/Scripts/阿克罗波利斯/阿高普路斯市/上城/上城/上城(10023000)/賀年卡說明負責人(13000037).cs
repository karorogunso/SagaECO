using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:賀年卡說明負責人(13000037) X:128 Y:139
namespace SagaScript.M10023000
{
    public class S13000037 : Event
    {
        public S13000037()
        {
            this.EventID = 13000037;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
