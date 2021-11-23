
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using System.Globalization;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;

namespace WeeklyExploration
{
    public partial class GTSQuest : Event
    {
        public GTSQuest()
        {
            this.EventID = 50123123;
        }
        void 创建副本(ActorPC pc)
        {

        }
        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            switch (Select(pc, "哭叽叽GM操作台", "", "开启玩家手机传送功能", "刷怪【暮色柠妹】", "刷怪【光塔老三】", "刷怪【北国领主】", "刷怪【吴克/疾风/花音】","刷怪【暖流埃雷拉】", "当前地图随机刷50个【端午节礼物盒】", "当前地图全员回家", "离开"))
            {
                case 1:
                    SInt["活动地图MAPID"] = 20074000;
                    SInt["活动地图X"] = 240;
                    SInt["活动地图Y"] = 184;
                    break;
                case 2:
                    ActorMob a = map.SpawnCustomMob(10000000, pc.MapID, 16420000, 0, 0, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, BOSS1Info(), BOSS1AI(), null, 0)[0];
                    a.TInt["playersize"] = 1800;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, a, false);
                    break;
                case 3:
                    ActorMob b = map.SpawnCustomMob(10000000, pc.MapID, 18610050, 0, 0, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 老三Info困难(), 老三AI困难(), null, 0)[0];
                    b.TInt["playersize"] = 1600;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, b, false);
                    break;
                case 4:
                    ActorMob C = map.SpawnCustomMob(10000000, pc.MapID, 16290000, 0, 0, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 外塔Info(), 外塔AI(), null, 0)[0];
                    C.TInt["playersize"] = 1600;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, C, false);
                    break;
                case 5:
                    ActorMob D = map.SpawnCustomMob(10000000, pc.MapID, 70000003, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 花音Info(), 花音AI(), null, 0)[0];
                    D.TInt["playersize"] = 1600;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, D, false);
                    ActorMob hayate = map.SpawnCustomMob(10000000, pc.MapID, 70000005, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 疾风Info(), 疾风AI(), null, 0)[0];
                    hayate.TInt["playersize"] = 1800;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, hayate, false);
                    ActorMob wuke = null;
                    ((MobEventHandler)D.e).AI.Master = hayate;
                    ((MobEventHandler)hayate.e).Defending += (s, e) =>
                    {
                        if (hayate.HP < hayate.MaxHP * 0.5 && wuke == null && hayate.AttackedForEvent != 1)
                        {
                            hayate.AttackedForEvent = 1;
                            hayate.RideID = 0;
                            hayate.HP = hayate.MaxHP;
                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, hayate, false);
                            wuke = map.SpawnCustomMob(10000000, pc.MapID, 70000004, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 吴克Info(), 吴克AI(), null, 0)[0];
                            wuke.TInt["光头疾风ID"] = (int)hayate.ActorID;
                            wuke.TInt["playersize"] = 1800;
                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, hayate, false);
                            ((MobEventHandler)wuke.e).AI.Master = hayate;
                            hayate.Slave.Add(wuke);
                            ((MobEventHandler)wuke.e).Defending += (w, k) =>
                            {
                                if (wuke.HP < wuke.MaxHP * 0.05 && hayate != null)
                                {
                                    if (hayate.HP > hayate.MaxHP * 0.12)
                                    {
                                        hayate.HP = hayate.MaxHP;
                                        wuke.HP = wuke.MaxHP;
                                    }
                                }
                            };
                        }
                        if (hayate.HP < hayate.MaxHP * 0.05 && wuke != null)
                        {
                            if (wuke.HP > wuke.MaxHP * 0.12)
                            {
                                hayate.HP = hayate.MaxHP;
                                wuke.HP = wuke.MaxHP;
                            }
                        }
                    };
                    break;
                case 6:
                    ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 16925100, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 10, 1, 0, 萝蕾拉Info(), 萝蕾拉AI(), null, 0)[0];
                    mobS.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS, false);
                    break;
                case 7:
                    map.SpawnCustomMob(10000000, pc.MapID, 30560000, 0, 0, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 250, 50, 0, 礼物INFO(), 礼物AI(), null, 0);
                    break;
            }
        }
    }
}

