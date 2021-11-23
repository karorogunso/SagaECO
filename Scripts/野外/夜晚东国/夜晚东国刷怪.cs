
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
namespace WeeklyExploration
{
    public partial class DongguoSpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public DongguoSpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10057002);
                map.SpawnCustomMob(10000000, map.ID, 10680100, 0, 0, 60, 90, 50, 3, 60, 深红鬼魂Info(), 深红鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680100, 0, 0, 85, 190, 50, 3, 60, 深红鬼魂Info(), 深红鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680100, 0, 0, 190, 195, 50, 3, 60, 深红鬼魂Info(), 深红鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680100, 0, 0, 190, 90, 50, 3, 60, 深红鬼魂Info(), 深红鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 60, 90, 50, 3, 60, 蓝色鬼魂Info(), 蓝色鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 85, 190, 50, 3, 60, 蓝色鬼魂Info(), 蓝色鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 190, 195, 50, 3, 60, 蓝色鬼魂Info(), 蓝色鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680300, 0, 0, 190, 90, 50, 3, 60, 蓝色鬼魂Info(), 蓝色鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680800, 0, 0, 60, 90, 50, 3, 60, 小鬼魂Info(), 小鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680800, 0, 0, 85, 190, 50, 3, 60, 小鬼魂Info(), 小鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680800, 0, 0, 190, 195, 50, 3, 60, 小鬼魂Info(), 小鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10680800, 0, 0, 190, 90, 50, 3, 60, 小鬼魂Info(), 小鬼魂AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10220500, 0, 0, 60, 90, 50, 3, 60, 光子鬼火Info(), 光子鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10220500, 0, 0, 85, 190, 50, 3, 60, 光子鬼火Info(), 光子鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10220500, 0, 0, 190, 195, 50, 3, 60, 光子鬼火Info(), 光子鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10220500, 0, 0, 190, 90, 50, 3, 60, 光子鬼火Info(), 光子鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10221500, 0, 0, 60, 90, 50, 3, 60, 粉色鬼火Info(), 粉色鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10221500, 0, 0, 85, 190, 50, 3, 60, 粉色鬼火Info(), 粉色鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10221500, 0, 0, 190, 195, 50, 3, 60, 粉色鬼火Info(), 粉色鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10221500, 0, 0, 190, 90, 50, 3, 60, 粉色鬼火Info(), 粉色鬼火AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 60, 90, 50, 3, 60, 灾厄木乃伊Info(), 灾厄木乃伊AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 85, 190, 50, 3, 60, 灾厄木乃伊Info(), 灾厄木乃伊AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 190, 195, 50, 3, 60, 灾厄木乃伊Info(), 灾厄木乃伊AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200200, 0, 0, 190, 90, 50, 3, 60, 灾厄木乃伊Info(), 灾厄木乃伊AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200100, 0, 0, 60, 90, 50, 3, 60, 古代的死者Info(), 古代的死者AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200100, 0, 0, 85, 190, 50, 3, 60, 古代的死者Info(), 古代的死者AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200100, 0, 0, 190, 195, 50, 3, 60, 古代的死者Info(), 古代的死者AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10200100, 0, 0, 190, 90, 50, 3, 60, 古代的死者Info(), 古代的死者AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 60, 90, 50, 3, 60, 骷髅射手Info(), 骷髅射手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 85, 190, 50, 3, 60, 骷髅射手Info(), 骷髅射手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 190, 195, 50, 3, 60, 骷髅射手Info(), 骷髅射手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 190, 90, 50, 3, 60, 骷髅射手Info(), 骷髅射手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 60, 90, 50, 3, 60, 骷髅击剑士Info(), 骷髅击剑士AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 85, 190, 50, 3, 60, 骷髅击剑士Info(), 骷髅击剑士AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 190, 195, 50, 3, 60, 骷髅击剑士Info(), 骷髅击剑士AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 190, 90, 50, 3, 60, 骷髅击剑士Info(), 骷髅击剑士AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 60, 90, 50, 3, 60, 亡灵装甲Info(), 亡灵装甲AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 85, 190, 50, 3, 60, 亡灵装甲Info(), 亡灵装甲AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 190, 195, 50, 3, 60, 亡灵装甲Info(), 亡灵装甲AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 190, 90, 50, 3, 60, 亡灵装甲Info(), 亡灵装甲AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10250500, 0, 0, 60, 90, 50, 3, 60, 奥迦斯Info(), 奥迦斯AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10250500, 0, 0, 85, 190, 50, 3, 60, 奥迦斯Info(), 奥迦斯AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10250500, 0, 0, 190, 195, 50, 3, 60, 奥迦斯Info(), 奥迦斯AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10250500, 0, 0, 190, 90, 50, 3, 60, 奥迦斯Info(), 奥迦斯AI(), null, 0);
            }
        }
    }
}

