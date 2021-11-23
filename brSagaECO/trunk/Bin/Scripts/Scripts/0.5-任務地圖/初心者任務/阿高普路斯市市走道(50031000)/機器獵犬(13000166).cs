using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:機器獵犬(13000166) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000166 : Event
    {
        public S13000166()
        {
            this.EventID = 13000166;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
