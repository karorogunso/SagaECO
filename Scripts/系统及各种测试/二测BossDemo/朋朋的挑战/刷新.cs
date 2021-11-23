
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class MOBTEST : Event
    {
        ActorMob mob;

        void 刷怪(uint mapid,Actor sactor)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            mob = map.SpawnCustomMob(10000000, sactor.MapID, 16290000, 0, 0, Global.PosX16to8(sactor.X,map.Width), Global.PosY16to8(sactor.Y,map.Height), 0, 1, 0, 外塔Info(), 外塔AI(), null, 0)[0];
        }
    }
}

