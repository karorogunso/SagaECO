
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public class S11002000 : Event
    {
        public S11002000()
        {
            this.EventID = 11002000;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "听说这儿的矿石很值钱，$R冒险者，你卖我点矿石吗？", "行脚商人");
            switch(Select(pc,"请选择","","买东西","卖东西","打开仓库","离开"))
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:
                    OpenWareHouse(pc, WarehousePlace.Acropolis);
                    break;
            }
        }
    }
}
