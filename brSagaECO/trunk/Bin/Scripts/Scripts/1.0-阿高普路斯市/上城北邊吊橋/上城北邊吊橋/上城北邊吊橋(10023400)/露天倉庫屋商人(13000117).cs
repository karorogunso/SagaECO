using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:露天倉庫屋商人(13000117) X:133 Y:6
namespace SagaScript.M10023400
{
    public class S13000117 : Event
    {
        public S13000117()
        {
            this.EventID = 13000117;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
