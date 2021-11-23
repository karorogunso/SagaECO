using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:露天倉庫屋商人(13000114) X:249 Y:133
namespace SagaScript.M10023100
{
    public class S13000114 : Event
    {
        public S13000114()
        {
            this.EventID = 13000114;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
