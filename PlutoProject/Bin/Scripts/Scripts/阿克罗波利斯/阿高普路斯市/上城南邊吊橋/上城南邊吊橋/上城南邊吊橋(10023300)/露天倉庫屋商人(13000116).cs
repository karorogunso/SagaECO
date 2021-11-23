using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:露天倉庫屋商人(13000116) X:122 Y:250
namespace SagaScript.M10023300
{
    public class S13000116 : Event
    {
        public S13000116()
        {
            this.EventID = 13000116;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
