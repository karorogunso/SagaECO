using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:埃米爾(11000512) X:127 Y:141
namespace SagaScript.M10023000
{
    public class S11000512 : Event
    {
        public S11000512()
        {
            this.EventID = 11000512;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
