using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（下层）(11024000)NPC基本信息:11002291-铁匠铺店员- X:134 Y:200
namespace SagaScript.M11024000
{
    public class S11002291 : Event
    {
    public S11002291()
        {
            this.EventID = 11002291;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
