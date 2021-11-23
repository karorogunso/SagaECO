
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
    public partial class MuMuGrassLandSpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public MuMuGrassLandSpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10056000);
                //map.SpawnCustomMob(10000000, map.ID, 19150000, 0, 0, 179, 69, 1, 1, 3600, 猫咪执事Info(), 猫咪执事AI(), null, 0);
                //map.SpawnCustomMob(10000000, map.ID, 18380000, 0, 0, 143, 215, 58, 1, 3600, 兔汉一Info(), 兔汉一AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10630000, 0, 0, 143, 215, 58, 10, 16, 蜥蜴人Info(), 蜥蜴人AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10651200, 0, 0, 143, 215, 58, 10, 16, 梅花猪Info(), 梅花猪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10651200, 0, 0, 84, 91, 38, 4, 16, 梅花猪Info(), 梅花猪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10651200, 0, 0, 141, 130, 32, 4, 16, 梅花猪Info(), 梅花猪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10651200, 0, 0, 207, 141, 36, 4, 16, 梅花猪Info(), 梅花猪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10660002, 0, 0, 181, 80, 20, 10, 16, 哞哞宝宝Info(), 哞哞宝宝AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10620100, 0, 0, 181, 80, 20, 10, 16, 咕咕幼崽Info(), 咕咕幼崽AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15460000, 0, 0, 225, 120, 36, 5, 60, 尼德鸟蛋Info(), 尼德鸟蛋AI(), new MobCallback(尼德鸟蛋ondie), 1);
                map.SpawnCustomMob(10000000,map.ID, 10630000, 0, 0, 225, 120, 36, 5, 16, 蜥蜴人Info(), 蜥蜴人AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 18420000, 0, 0, 225, 120, 36, 8, 16, 加农炮爬爬虫Info(), 加农炮爬爬虫AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 18420000, 0, 0, 98, 69, 54, 10, 16, 加农炮爬爬虫Info(), 加农炮爬爬虫AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15460000, 0, 0, 98, 69, 54, 5, 60, 尼德鸟蛋Info(), 尼德鸟蛋AI(), new MobCallback(尼德鸟蛋ondie), 1);
                map.SpawnCustomMob(10000000, map.ID, 10662000, 0, 0, 75, 59, 33, 15, 16, 娟姗哞哞Info(), 娟姗哞哞AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10580000, 0, 0, 135, 57, 21, 10, 16, 曼陀罗胡萝卜Info(), 曼陀罗胡萝卜AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10580000, 0, 0, 193, 15, 10, 3, 16, 曼陀罗胡萝卜Info(), 曼陀罗胡萝卜AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10010000, 0, 0, 98, 69, 54, 10, 16, 咕咕Info(), 咕咕AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10010000, 0, 0, 225, 120, 36, 8, 16, 咕咕Info(), 咕咕AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10620100, 0, 0, 98, 69, 54, 5, 16, 咕咕幼崽Info(), 咕咕幼崽AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10150000, 0, 0, 40, 138, 68, 5, 60, 独角兽Info(), 独角兽AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10150000, 0, 0, 225, 120, 36, 3, 60, 独角兽Info(), 独角兽AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10662000, 0, 0, 40, 138, 68, 10, 16, 娟姗哞哞Info(), 娟姗哞哞AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID,10580000, 0, 0, 38, 208, 31, 8, 16, 曼陀罗胡萝卜Info(), 曼陀罗胡萝卜AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15460000, 0, 0, 38, 208, 31, 3, 60, 尼德鸟蛋Info(), 尼德鸟蛋AI(),new MobCallback(尼德鸟蛋ondie),1);
                map.SpawnCustomMob(10000000, map.ID, 30290000, 0, 0, 125, 125, 125, 18, 60, 木箱子Info(), 木箱子AI(), 木箱子Ondie, 1);

                map.SpawnCustomMob(10000000, map.ID, 30211000, 0,0, 125, 125, 125, 7, 80, YugangdaoASpawn.光明之花Info(), YugangdaoASpawn.光明之花AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30210800, 0, 0, 125, 125, 125, 7, 80, YugangdaoASpawn.大地之花Info(), YugangdaoASpawn.大地之花AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30270000, 0, 0, 125, 125, 125, 5, 90, YugangdaoASpawn.宝箱Info(), YugangdaoASpawn.宝箱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30070008, 0, 0, 125, 125, 125, 8, 70, YugangdaoASpawn.蘑菇Info(), YugangdaoASpawn.蘑菇AI(), null, 0);
            }
        }

        private void 木箱子Ondie(MobEventHandler eh, ActorPC pc)
        {
            TitleProccess(pc, 11, 1);
        }
        private void 尼德鸟蛋ondie(MobEventHandler eh, ActorPC pc)
        {
            TitleProccess(pc, 1, 1);
        }
    }
}

