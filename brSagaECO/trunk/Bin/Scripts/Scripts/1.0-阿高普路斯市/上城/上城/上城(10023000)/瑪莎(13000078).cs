using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:瑪莎(13000078) X:168 Y:186
namespace SagaScript.M10023000
{
    public class S13000078 : Event
    {
        public S13000078()
        {
            this.EventID = 13000078;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
