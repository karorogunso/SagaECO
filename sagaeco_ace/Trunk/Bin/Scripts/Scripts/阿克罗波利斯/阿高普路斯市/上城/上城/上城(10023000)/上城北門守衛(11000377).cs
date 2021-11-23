using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:上城北門守衛(11000377) X:126 Y:33
namespace SagaScript.M10023000
{
    public class S11000377 : Event
    {
        public S11000377()
        {
            this.EventID = 11000377;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
