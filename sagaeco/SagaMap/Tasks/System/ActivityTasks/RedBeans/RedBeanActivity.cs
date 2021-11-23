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
    public class RedBeanActivity : MultiRunTask
    {
        public RedBeanActivity()
        {
            period = 60000;
            dueTime = 0;
            MapIDs.Add(10017000);
            MapIDs.Add(10015000);
            MapIDs.Add(10020000);
            MapIDs.Add(10028000);
            MapIDs.Add(10030000);
            MapIDs.Add(10032000);
            MapIDs.Add(10035000);
            MapIDs.Add(10049000);
            //MapIDs.Add(10056000);
            MapIDs.Add(10064000);
/*-帕斯特街道 
-阿克罗尼亚丛林 
-杀人蜂山谷 
-摩斯格断崖 
-果物之森 
-乌特纳湖 
-军舰岛 
-北方高原 
-哞哞草原 
-铁火山 
*/
        }
        List<uint> MapIDs = new List<uint>();
        static RedBeanActivity instance;

        public static RedBeanActivity Instance
        {
            get
            {
                if (instance == null)
                    instance = new RedBeanActivity();
                return instance;
            }
        }

        public override void CallBack()
        {
            DateTime now = DateTime.Now;
            
            if (ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] > 10)
            {
                if (now.Hour == 7 || now.Hour == 11 || now.Hour == 15 || now.Hour == 18 || now.Hour == 20 || now.Hour == 22)
                {
                    if (now.Minute == 50)
                        MapClientManager.Instance.Announce("【中秋活动】在10分钟后，大家种的红豆就要生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] + "个红豆种子。");
                    if (now.Minute == 55)
                        MapClientManager.Instance.Announce("【中秋活动】在5分钟后，大家种的红豆就要生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] + "个红豆种子。");
                    if (now.Minute == 59)
                        MapClientManager.Instance.Announce("【中秋活动】在1分钟后，大家种的红豆就要生长出来啦！目前全服种植了：" + ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] + "个红豆种子。");
                }
                if (now.Hour == 8 || now.Hour == 12 || now.Hour == 16 || now.Hour == 19 || now.Hour == 21 || now.Hour == 23)
                {
                    if (now.Minute == 0)
                    {
                        MapClientManager.Instance.Announce("【中秋活动】全服种植红豆树活动正在举办中！详细请见网站公告。http://yuki.cc");
                        MapClientManager.Instance.Announce("【中秋活动】大家种下的红豆种子，已经在指定的地图中长出来了！大家快去采摘呀！");
                        
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] / 10);
                        count += 5;
                        ScriptManager.Instance.VariableHolder.AInt["红豆刷新数量"] = count;
                        ScriptManager.Instance.VariableHolder.AInt["当前小时全服种植的红豆"] = 0;
                        if (count > 45)
                            count = 45;
                        spwanRedBean(count);
                    }
                    if (now.Minute == 2)
                    {
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["红豆刷新数量"] / 2);
                        spwanRedBean(count);
                    }
                    if (now.Minute == 4)
                    {
                        ushort count = (ushort)(ScriptManager.Instance.VariableHolder.AInt["红豆刷新数量"] / 2);
                        spwanRedBean(count);
                        ScriptManager.Instance.VariableHolder.AInt["红豆刷新数量"] = 0;
                    }
                }
            }
        }
        void spwanRedBean(int count)
        {
            foreach (var id in MapIDs)
            {
                Map map = MapManager.Instance.GetMap(id);
                map.SpawnCustomMob(10000000, id, 30040000, 0, 0, 125, 125, 222, count, 0, 活动怪物.活动红豆Info(), 活动怪物.活动红豆AI(), null, 0);
            }
        }
    }
}
