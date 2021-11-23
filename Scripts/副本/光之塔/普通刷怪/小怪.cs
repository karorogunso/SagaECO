
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        void 主场馆2F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10380000, 0, 0, 50, 50, 150, 3, 0, S10380000Info(), S10380000AI(), null, 0); //加特林机关炮
            map.SpawnCustomMob(10000000, map.ID, 10390000, 0, 0, 50, 50, 150, 3, 0, S10390000Info(), S10390000AI(), null, 0);//破坏者特别定制型
            map.SpawnCustomMob(10000000, map.ID, 10800000, 0, 0, 50, 50, 150, 4, 0, S10800000Info(), S10800000AI(), null, 0);//杀戮机器
        }
        void 主场馆3F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10400000, 0, 0, 50, 50, 150, 3, 0, S10400000Info(), S10400000AI(), null, 0);//毁灭者
            map.SpawnCustomMob(10000000, map.ID, 10400100, 0, 0, 50, 50, 150, 3, 0, S10400100Info(), S10400100AI(), null, 0);//红色毁灭者
            map.SpawnCustomMob(10000000, map.ID, 10500000, 0, 0, 50, 50, 150, 4, 0, S10500000Info(), S10500000AI(), null, 0);//破坏MｋⅡ
        }
        void 主场馆4F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10860400, 0, 0, 50, 50, 150, 3, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（蓝)
            map.SpawnCustomMob(10000000, map.ID, 10860500, 0, 0, 50, 50, 150, 3, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（绿)
            map.SpawnCustomMob(10000000, map.ID, 10860700, 0, 0, 50, 50, 150, 4, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（黄)
        }
        void B5F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10871400, 0, 0, 25, 25, 100, 3, 0, S10871400Info(), S10871400AI(), null, 0);//银色水晶海胆
            map.SpawnCustomMob(10000000, map.ID, 10871100, 0, 0, 25, 25, 100, 3, 0, S10871100Info(), S10871100AI(), null, 0);//硬壳水晶海胆
            map.SpawnCustomMob(10000000, map.ID, 10861400, 0, 0, 25, 25, 100, 4, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（白)
        }
        void B6F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10810000, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸S5
            map.SpawnCustomMob(10000000, map.ID, 10800000, 0, 0, 25, 25, 100, 3, 0, S10800000Info(), S10800000AI(), null, 0);//杀戮机器a
            map.SpawnCustomMob(10000000, map.ID, 10810100, 0, 0, 25, 25, 100, 4, 0, S10810102Info(), S10810102AI(), null, 0);//群聚甲虫
        }
        void B7F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10810000, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸S5
            map.SpawnCustomMob(10000000, map.ID, 10810000, 0, 0, 25, 25, 100, 3, 0, S10810001Info(), S10810001AI(), null, 0);//步行钻孔机器人
            map.SpawnCustomMob(10000000, map.ID, 10810300, 0, 0, 25, 25, 100, 4, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸B3
        }
        void B8F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10850000, 0, 0, 25, 25, 100, 3, 0, S10850002Info(), S10850002AI(), null, 0);//行刑机械Σ
            map.SpawnCustomMob(10000000, map.ID, 10830300, 0, 0, 25, 25, 100, 3, 0, S10830302Info(), S10830302AI(), null, 0);//机械风暴GX0
            map.SpawnCustomMob(10000000, map.ID, 10830000, 0, 0, 25, 25, 100, 4, 0, S10830300Info(), S10830300AI(), null, 0);//巨型机械XE
        }
        void A5F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10800000, 0, 0, 25, 25, 100, 3, 0, S10800000Info(), S10800000AI(), null, 0);//杀戮机器a
            map.SpawnCustomMob(10000000, map.ID, 10810100, 0, 0, 25, 25, 100, 3, 0, S10810101Info(), S10810101AI(), null, 0);//自爆甲虫
            map.SpawnCustomMob(10000000, map.ID, 10820300, 0, 0, 25, 25, 100, 4, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬B3
        }
        void A6F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10401000, 0, 0, 25, 25, 100, 3, 0, S10401000Info(), S10401000AI(), null, 0);//空贼
            map.SpawnCustomMob(10000000, map.ID, 10811203, 0, 0, 25, 25, 100, 3, 0, S10810302Info(), S10810302AI(), null, 0);//破坏机械T01
            map.SpawnCustomMob(10000000, map.ID, 10451201, 0, 0, 25, 25, 100, 4, 0, S10451201Info(), S10451201AI(), null, 0);//恶灵拉谬罗斯
        }
        void A7F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10550000, 0, 0, 25, 25, 100, 3, 0, S10550000Info(), S10550000AI(), null, 0);//加特林机关炮
            map.SpawnCustomMob(10000000, map.ID, 10830100, 0, 0, 25, 25, 100, 3, 0, S10830300Info(), S10830300AI(), null, 0);//巨型机械R3
            map.SpawnCustomMob(10000000, map.ID, 10870100, 0, 0, 25, 25, 100, 4, 0, S10870100Info(), S10870100AI(), null, 0);//赭色水晶海胆
        }
        void A8F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10810300, 0, 0, 25, 25, 100, 3, 0, S10810302Info(), S10810302AI(), null, 0);//破坏机械TX0
            map.SpawnCustomMob(10000000, map.ID, 10810550, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸EE
            map.SpawnCustomMob(10000000, map.ID, 10820100, 0, 0, 25, 25, 100, 4, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬R1
        }
        void A9F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10860010, 0, 0, 25, 25, 100, 3, 0, S10860010Info(), S10860010AI(), null, 0);//莱利卡尔
            map.SpawnCustomMob(10000000, map.ID, 10850000, 0, 0, 25, 25, 100, 3, 0, S10850002Info(), S10850002AI(), null, 0);//行刑机械7
            map.SpawnCustomMob(10000000, map.ID, 10550010, 0, 0, 25, 25, 100, 4, 0, S10550000Info(), S10550000AI(), null, 0);//加特林机关炮A
        }
        void A10F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10860010, 0, 0, 25, 25, 100, 3, 0, S10860017Info(), S10860017AI(), null, 0);//温蒂
            map.SpawnCustomMob(10000000, map.ID, 10500050, 0, 0, 25, 25, 100, 3, 0, S10500050Info(), S10500050AI(), null, 0);//破坏诺尔
            map.SpawnCustomMob(10000000, map.ID, 10450000, 0, 0, 25, 25, 100, 4, 0, S10450002Info(), S10450002AI(), null, 0);//瓦兹·拉米亚
        }
        void A11F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10850001, 0, 0, 25, 25, 100, 3, 0, S10850002Info(), S10850002AI(), null, 0);//行刑机械Λ
            map.SpawnCustomMob(10000000, map.ID, 10820300, 0, 0, 25, 25, 100, 3, 0, S10820303Info(), S10820303AI(), null, 0);//屠杀机械HX0
            map.SpawnCustomMob(10000000, map.ID, 10811200, 0, 0, 25, 25, 100, 4, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸C1EE
        }
        void A12F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10820000, 0, 0, 25, 25, 100, 3, 0, S10820001Info(), S10820001AI(), null, 0);//步行机炮机器人
            map.SpawnCustomMob(10000000, map.ID, 10811200, 0, 0, 25, 25, 100, 3, 0, S10810302Info(), S10810302AI(), null, 0);//破坏机械T01
            map.SpawnCustomMob(10000000, map.ID, 10811200, 0, 0, 25, 25, 100, 4, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸C1
        }
        void A13F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10870000, 0, 0, 25, 25, 100, 3, 0, S10870000Info(), S10870000AI(), null, 0);//水晶海胆
            map.SpawnCustomMob(10000000, map.ID, 10870300, 0, 0, 25, 25, 100, 3, 0, S10870300Info(), S10870300AI(), null, 0);//蓝色水晶海胆
            map.SpawnCustomMob(10000000, map.ID, 10870700, 0, 0, 25, 25, 100, 4, 0, S10870700Info(), S10870700AI(), null, 0);//闪光水晶海胆
        }
        void A14F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10861000, 0, 0, 25, 25, 100, 3, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（黑）
            map.SpawnCustomMob(10000000, map.ID, 10860100, 0, 0, 25, 25, 100, 3, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（红）
            map.SpawnCustomMob(10000000, map.ID, 10860000, 0, 0, 25, 25, 100, 4, 0, S10860010Info(), S10860010AI(), null, 0);//昂布莱拉
        }
        void A15F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10550000, 0, 0, 25, 25, 100, 3, 0, S10550000Info(), S10550000AI(), null, 0);//加特林机关炮B
            map.SpawnCustomMob(10000000, map.ID, 10810100, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸R4
            map.SpawnCustomMob(10000000, map.ID, 10451000, 0, 0, 25, 25, 100, 4, 0, S10451000Info(), S10451000AI(), null, 0);//拉谬罗斯
        }
        void A16F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10830300, 0, 0, 25, 25, 100, 3, 0, S10830302Info(), S10830302AI(), null, 0);//机械风暴G00
            map.SpawnCustomMob(10000000, map.ID, 10830000, 0, 0, 25, 25, 100, 3, 0, S10830001Info(), S10830001AI(), null, 0);//步行加农炮机器人
            map.SpawnCustomMob(10000000, map.ID, 10830100, 0, 0, 25, 25, 100, 4, 0, S10830300Info(), S10830300AI(), null, 0);//巨型机械R3
        }
        void A17F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10820101, 0, 0, 25, 25, 100, 3, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬R1E
            map.SpawnCustomMob(10000000, map.ID, 10450900, 0, 0, 25, 25, 100, 3, 0, S10450900Info(), S10450900AI(), null, 0);//秀巴利斯
            map.SpawnCustomMob(10000000, map.ID, 10810300, 0, 0, 25, 25, 100, 4, 0, S10810302Info(), S10810302AI(), null, 0);//破坏机械T00
        }
        void A18F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10811200, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸C1
            map.SpawnCustomMob(10000000, map.ID, 10811200, 0, 0, 25, 25, 100, 3, 0, S10810302Info(), S10810302AI(), null, 0);//破坏机械T01
            map.SpawnCustomMob(10000000, map.ID, 10820100, 0, 0, 25, 25, 100, 4, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬R1
        }
        void A19F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10450100, 0, 0, 25, 25, 100, 3, 0, S10450100Info(), S10450100AI(), null, 0);//夏天的赤蛇
            map.SpawnCustomMob(10000000, map.ID, 10450000, 0, 0, 25, 25, 100, 3, 0, S10450000Info(), S10450000AI(), null, 0);//拉米亚
            map.SpawnCustomMob(10000000, map.ID, 10450300, 0, 0, 25, 25, 100, 4, 0, S10450300Info(), S10450300AI(), null, 0);//雷尹雷斯
        }
        void A20F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10870500, 0, 0, 25, 25, 100, 3, 0, S10870500Info(), S10870500AI(), null, 0);//绿色水晶海胆
            map.SpawnCustomMob(10000000, map.ID, 10831200, 0, 0, 25, 25, 100, 3, 0, S10830300Info(), S10830300AI(), null, 0);//巨型机械C4
            map.SpawnCustomMob(10000000, map.ID, 10820300, 0, 0, 25, 25, 100, 4, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬B3
        }
        void A21F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10810500, 0, 0, 25, 25, 100, 3, 0, S10810300Info(), S10810300AI(), null, 0);//机械狐狸G2
            map.SpawnCustomMob(10000000, map.ID, 10820100, 0, 0, 25, 25, 100, 3, 0, S10820300Info(), S10820300AI(), null, 0);//机械猎犬R1E
            map.SpawnCustomMob(10000000, map.ID, 10830100, 0, 0, 25, 25, 100, 4, 0, S10830300Info(), S10830300AI(), null, 0);//巨型机械S6
        }
        void A22F刷怪(uint mapid, ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10860100, 0, 0, 25, 25, 100, 3, 0, S10861400Info(), S10861400AI(), null, 0);//雨伞（红）
            map.SpawnCustomMob(10000000, map.ID, 10500050, 0, 0, 25, 25, 100, 3, 0, S10500050Info(), S10500050AI(), null, 0);//破坏诺尔
            map.SpawnCustomMob(10000000, map.ID, 10830300, 0, 0, 25, 25, 100, 4, 0, S10830302Info(), S10830302AI(), null, 0);//机械风暴GX0
        }
    }
}

