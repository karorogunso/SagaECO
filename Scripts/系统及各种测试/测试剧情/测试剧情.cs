
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S50000002 : Event
    {
        public S50000002()
        {
            this.EventID = 50000002;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch(Select(pc,"请选择本次活动的选项","","将本队队友传送至活动地图","开起本队的PK状态","离开"))
            {
                case 1:
                    switch (Select(pc, "请选择阵营", "", "头头的队", "an的队","123", "离开"))
                    {
                        case 1:
                            foreach (var item in pc.Party.Members.Values)
                            {
                                item.TInt["复活次数"] = 999;
                                item.TInt["副本复活标记"] = 2;
                                item.TInt["副本savemapid"] = 10100018;
                                item.TInt["副本savemapX"] = 133;
                                item.TInt["副本savemapY"] = 111;
                                Warp(item, 10100018, 133, 111);
                            }
                            break;
                        case 2:
                            foreach (var item in pc.Party.Members.Values)
                            {
                                item.TInt["复活次数"] = 20;
                                item.TInt["副本复活标记"] = 2;
                                item.TInt["副本savemapid"] = 10100018;
                                item.TInt["副本savemapX"] = 67;
                                item.TInt["副本savemapY"] = 85;
                                Warp(item, 10100018, 67, 85);
                            }
                            break;
                        case 3:
                            pc.Buff.Dead = false;
                            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, null,pc, true);
                            foreach (var item in map.Actors)
                            {
                                if(item.Value.type == ActorType.PC && item.Value != pc)
                                Warp((ActorPC)item.Value, 30201002, 5, 9);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (Select(pc, "请选择PK阵营", "", "头头选北国", "an选南国", "离开"))
                    {
                        case 1:
                            foreach (var item in pc.Party.Members.Values)
                            {
                                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                                //ChangeBGM(item, 1161, true, 100, 50);
                                item.Mode = PlayerMode.KNIGHT_NORTH;
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item, true);
                            }
                            break;
                        case 2:
                            foreach (var item in pc.Party.Members.Values)
                            {
                                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                                //ChangeBGM(item, 1161, true, 100, 50);
                                item.Mode = PlayerMode.KNIGHT_SOUTH;
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item, true);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}

