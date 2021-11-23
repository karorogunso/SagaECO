using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
using SagaMap.Manager;
namespace SagaMap.Tasks.System
{
    public class AutoDeleteInstanceMap : MultiRunTask
    {
        public AutoDeleteInstanceMap()
        {
            this.period = 60000;
        }
        static AutoDeleteInstanceMap instance;

        public static AutoDeleteInstanceMap Instance
        {
            get
            {
                if (instance == null)
                    instance = new AutoDeleteInstanceMap();
                return instance;
            }
        }
        public override void CallBack()
        {
            IConcurrentDictionary<uint, Map> maps = MapManager.Instance.Maps;
            DateTime now = DateTime.Now;
            uint count = 0;
            foreach (var item in maps)
            {
                uint mapid = item.Key;
                if (maps.ContainsKey(mapid))
                {
                    if (maps[mapid].AutoDispose && maps[mapid].IsMapInstance)
                    {
                        Map map = maps[mapid];
                        int resetMinutes = (int)(map.DeleteTime - DateTime.Now).TotalMinutes;
                        if (resetMinutes <= 0)
                            MapManager.Instance.DeleteMapInstance(mapid);
                        else if (resetMinutes <= 10)
                            map.Announce("[副本坍塌]本地图将在" + resetMinutes + "分钟后坍塌。");
                        count++;
                    }
                }
            }
            Logger.ShowInfo("当前服务器副本地图数量：" + count);
        }
    }
}
