using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（上层）(11023000)NPC基本信息:11002420-艾尔·夏尔士兵- X:244 Y:143
namespace SagaScript.M11023000
{
    public class S11002420 : Event
    {
    public S11002420()
        {
            this.EventID = 11002420;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
