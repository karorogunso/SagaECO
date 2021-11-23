using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:機器獵犬(13000161) X:4 Y:17
namespace SagaScript.M50031000
{
    public class S13000161 : Event
    {
        public S13000161()
        {
            this.EventID = 13000161;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
