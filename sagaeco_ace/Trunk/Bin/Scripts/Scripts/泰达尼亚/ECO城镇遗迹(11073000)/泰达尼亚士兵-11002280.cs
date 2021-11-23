using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO城镇遗迹(11073000)NPC基本信息:11002280-泰达尼亚士兵- X:183 Y:36
namespace SagaScript.M11073000
{
    public class S11002280 : Event
    {
    public S11002280()
        {
            this.EventID = 11002280;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
