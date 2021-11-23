using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:露天倉庫屋商人(13000115) X:6 Y:122
namespace SagaScript.M10023200
{
    public class S13000115 : Event
    {
        public S13000115()
        {
            this.EventID = 13000115;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
