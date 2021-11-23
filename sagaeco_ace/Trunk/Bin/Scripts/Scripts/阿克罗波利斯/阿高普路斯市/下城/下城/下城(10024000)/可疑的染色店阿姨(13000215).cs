using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:可疑的染色店阿姨(13000215) X:169 Y:61
namespace SagaScript.M10024000
{
    public class S13000215 : Event
    {
        public S13000215()
        {
            this.EventID = 13000215;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
