using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:裁縫阿姨的家(30020000) NPC基本信息:熊熊(11000357) X:0 Y:2
namespace SagaScript.M30020000
{
    public class S11000357 : Event
    {
        public S11000357()
        {
            this.EventID = 11000357;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
