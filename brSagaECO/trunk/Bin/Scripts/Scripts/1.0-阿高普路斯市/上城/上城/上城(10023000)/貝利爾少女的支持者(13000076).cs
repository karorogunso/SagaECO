using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:貝利爾少女的支持者(13000076) X:127 Y:116
namespace SagaScript.M10023000
{
    public class S13000076 : Event
    {
        public S13000076()
        {
            this.EventID = 13000076;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
