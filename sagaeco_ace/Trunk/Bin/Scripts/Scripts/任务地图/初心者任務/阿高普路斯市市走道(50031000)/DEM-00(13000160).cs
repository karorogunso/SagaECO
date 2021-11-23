using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:DEM-00(13000160) X:10 Y:15
namespace SagaScript.M50031000
{
    public class S13000160 : Event
    {
        public S13000160()
        {
            this.EventID = 13000160;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
