using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:奇怪的染色店阿姨(13000216) X:168 Y:59
namespace SagaScript.M10024000
{
    public class S13000216 : Event
    {
        public S13000216()
        {
            this.EventID = 13000216;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
