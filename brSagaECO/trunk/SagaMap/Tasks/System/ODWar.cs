using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.ODWar;

using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Tasks.System
{
    public class ODWar : MultiRunTask
    {
        public ODWar()
        {
            this.period = 60000;
            this.dueTime = 0;
        }
        
        static ODWar instance;

        public static ODWar Instance
        {
            get
            {
                if (instance == null)
                    instance = new ODWar();
                return instance;
            }
        }

        public void StartODWar(uint mapID)
        {
            if (ODWarFactory.Instance.Items.ContainsKey(mapID))
            {
                MapClientManager.Instance.Announce("都市防御战开始了！");
                ODWarFactory.Instance.Items[mapID].Started = true;
                if (ODWarFactory.Instance.Items[mapID].StartTime.ContainsKey((int)DateTime.Today.DayOfWeek))
                {
                    ODWarFactory.Instance.Items[mapID].StartTime[(int)DateTime.Today.DayOfWeek] = DateTime.Now.Hour;
                }
                else
                    ODWarFactory.Instance.Items[mapID].StartTime.Add((int)DateTime.Today.DayOfWeek, DateTime.Now.Hour);
                ODWarFactory.Instance.Items[mapID].Score.Clear();
            }
        }

        public override void  CallBack()
        {
            try
            {
                DateTime now = DateTime.Now;
                foreach (SagaDB.ODWar.ODWar i in ODWarFactory.Instance.Items.Values)
                {
                    Map map = MapManager.Instance.GetMap(i.MapID);
                    if (ODWarManager.Instance.IsDefence(i.MapID))
                    {
                        if (i.StartTime.ContainsKey((int)now.DayOfWeek))
                        {
                            int time = i.StartTime[(int)now.DayOfWeek];
                            if (map != null)
                            {
                                if (ODWarManager.Instance.IsDefence(i.MapID))
                                {
                                    if ((now.Hour + 1 == time) && now.Minute >= 45 && (now.Minute % 5 == 0))
                                    {
                                        MapClientManager.Instance.Announce(string.Format(LocalManager.Instance.Strings.ODWAR_PREPARE, map.Name, (now.Minute - 30)));
                                        MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_PREPARE2);
                                    }
                                    if (now.Hour == time && now.Minute < 15)
                                    {
                                        MapClientManager.Instance.Announce(string.Format(LocalManager.Instance.Strings.ODWAR_PREPARE, map.Name, (15 - now.Minute)));
                                        MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_PREPARE2);
                                    }                                    
                                    if (now.Hour == time && now.Minute == 15)
                                    {
                                        MapClientManager.Instance.Announce(LocalManager.Instance.Strings.ODWAR_START);
                                        i.Started = true;
                                        i.Score.Clear();
                                    }
                                    if (now.Hour == time && now.Minute >= 1 && i.Started)
                                    {
                                        if (now.Minute % 10 == 0)
                                            ODWarManager.Instance.SpawnMob(i.MapID, true);
                                        else
                                            ODWarManager.Instance.SpawnMob(i.MapID, false);
                                        if (now.Minute == 58 || now.Minute == 59)
                                        {
                                            ODWarManager.Instance.SpawnMob(i.MapID, true);
                                        }
                                    }
                                    if (now.Hour == time && (now.Minute == 58) && i.Started)
                                    {
                                        ODWarManager.Instance.SpawnBoss(i.MapID);
                                    }
                                    if (now.Hour != time && i.Started)
                                    {
                                        i.Started = false;
                                        ODWarManager.Instance.EndODWar(i.MapID, true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int shouldCount = (i.WaveStrong.DEMNormal + i.WaveStrong.DEMChamp) * i.Symbols.Count;
                        if (map.CountActorType(ActorType.MOB) <= shouldCount)
                        {
                            if (now.Minute % 10 == 0)
                                ODWarManager.Instance.SpawnMob(i.MapID, true);
                            else
                                ODWarManager.Instance.SpawnMob(i.MapID, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }            
        }
    }
}
