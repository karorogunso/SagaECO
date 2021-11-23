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
    public class BlueBerryActivity : MultiRunTask
    {
        public BlueBerryActivity()
        {
            this.period = 60000;
            this.dueTime = 0;
        }

        static BlueBerryActivity instance;

        public static BlueBerryActivity Instance
        {
            get
            {
                if (instance == null)
                    instance = new BlueBerryActivity();
                return instance;
            }
        }

        public override void CallBack()
        {
            DateTime now = DateTime.Now;
            Map map = MapManager.Instance.GetMap(10056000);
            if (ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] > 10)
            {
                if (now.Hour == 8 || now.Hour == 12 || now.Hour == 16 || now.Hour == 20 || now.Hour == 22)
                {
                    if (now.Minute == 50)
                        MapClientManager.Instance.Announce("【蓝莓活动】在10分钟后，大家种的蓝莓在哞哞草原生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] + "个蓝莓种子。");
                    if (now.Minute == 55)
                        MapClientManager.Instance.Announce("【蓝莓活动】在5分钟后，大家种的蓝莓在哞哞草原生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] + "个蓝莓种子。");
                    if (now.Minute == 59)
                        MapClientManager.Instance.Announce("【蓝莓活动】在1分钟后，大家种的蓝莓在哞哞草原生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] + "个蓝莓种子。");
                }
                if (now.Hour == 9 || now.Hour == 13 || now.Hour == 17 || now.Hour == 21 || now.Hour == 23)
                {
                    if (now.Minute == 0)
                    {
                        MapClientManager.Instance.Announce("【蓝莓活动】全服种植蓝莓活动正在举办中！详细请见网站公告。http://yuki.cc");
                        MapClientManager.Instance.Announce("【蓝莓活动】大家种下的蓝莓种子，已经在哞哞草原长出来了！大家快去采摘呀！");
                        ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] = 0;
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的蓝莓"] / 3);
                        count += 50;
                        ScriptManager.Instance.VariableHolder.AInt["蓝莓刷新数量"] = count;
                        if (count > 500)
                            count = 500;
                        map.SpawnCustomMob(10000000, 10056000, 30550000, 0, 0, 157, 147, 222, count, 0, 活动怪物.活动蓝莓Info(), 活动怪物.活动蓝莓AI(), null, 0);
                    }
                    if (now.Minute == 2)
                    {
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["蓝莓刷新数量"] / 2);
                        map.SpawnCustomMob(10000000, 10056000, 30550000, 0, 0, 157, 147, 222, count, 0, 活动怪物.活动蓝莓Info(), 活动怪物.活动蓝莓AI(), null, 0);
                    }
                    if (now.Minute == 4)
                    {
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["蓝莓刷新数量"] / 2);
                        map.SpawnCustomMob(10000000, 10056000, 30550000, 0, 0, 157, 147, 222, count, 0, 活动怪物.活动蓝莓Info(), 活动怪物.活动蓝莓AI(), null, 0);
                        ScriptManager.Instance.VariableHolder.AInt["蓝莓刷新数量"] = 0;
                    }
                }
            }
        }
    }
}
