
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
    public partial class YugangdaoASpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public YugangdaoASpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10054001);
                map.SpawnCustomMob(10000000, map.ID, 10151200, 0, 0, 138, 230, 50, 5, 50, 棕马Info(), 棕马AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10151200, 0, 0, 38, 208, 31, 5, 50, 棕马Info(), 棕马AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10620000, 0, 0, 138, 230, 50, 7, 25, 咕咕鸡Info(), 咕咕鸡AI(), 小鸡鸡ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 15150000, 0, 0, 138, 230, 50, 7, 25, 小猪Info(), 小猪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15660000, 0, 0, 138, 230, 50, 9, 25, 小红花Info(), 小红花AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15660000, 0, 0, 38, 208, 31, 7, 25, 小红花Info(), 小红花AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14290000, 0, 0, 138, 230, 50, 6, 25, 小苹果Info(), 小苹果AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14290000, 0, 0, 38, 208, 31, 6, 25, 小苹果Info(), 小苹果AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15140000, 0, 0, 138, 230, 50, 5, 25, 莫兰Info(), 莫兰AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15140000, 0, 0, 38, 208, 31, 6, 25, 莫兰Info(), 莫兰AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10780000, 0, 0, 138, 230, 50, 2, 70, 粉色吱吱Info(), 粉色吱吱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10780000, 0, 0, 38, 208, 31, 2, 70, 粉色吱吱Info(), 粉色吱吱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10780015, 0, 0, 138, 230, 50, 2, 70, 橙色吱吱Info(), 橙色吱吱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10780015, 0, 0, 38, 208, 31, 2, 70, 橙色吱吱Info(), 橙色吱吱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10730008, 0, 0, 138, 230, 50, 6, 60, 粉色天马Info(), 粉色天马AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10730008, 0, 0, 38, 208, 31, 5, 60, 粉色天马Info(), 粉色天马AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10660000, 0, 0, 38, 208, 31, 6, 25, 哞哞Info(), 哞哞AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10660000, 0, 0, 138, 230, 50, 6, 25, 哞哞Info(), 哞哞AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 18380001, 0, 0, 38, 208, 31, 1, 3600, 贪玩的兔子Info(), 贪玩的兔子AI(), Ondie, 1);

                map.SpawnCustomMob(10000000, map.ID, 30290000, 0, 0, 138, 230, 50, 3, 70, 木箱子Info(), 木箱子AI(), 木箱子Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 30290000, 0, 0, 38, 208, 31, 3, 70, 木箱子Info(), 木箱子AI(), 木箱子Ondie, 1);

                map.SpawnCustomMob(10000000, map.ID, 30270000, 0, 0, 138, 230, 50, 1, 90, 宝箱Info(), 宝箱AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30270000, 0, 0, 38, 208, 31, 1, 90, 宝箱Info(), 宝箱AI(), null, 0);

                map.SpawnCustomMob(10000000, map.ID, 30211000, 0, 0, 138, 230, 50, 1, 80, 光明之花Info(), 光明之花AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30210800, 0, 0, 38, 208, 31, 1, 80, 大地之花Info(), 大地之花AI(), null, 0);

                map.SpawnCustomMob(10000000, map.ID, 30070008, 0, 0, 138, 230, 50, 1, 70, 蘑菇Info(), 蘑菇AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30070008, 0, 0, 38, 208, 31, 1, 70, 蘑菇Info(), 蘑菇AI(), null, 0);
            }
        }

        private void 木箱子Ondie(MobEventHandler eh, ActorPC pc)
        {
            TitleProccess(pc, 11, 1);
        }

        private void 小鸡鸡ondie(MobEventHandler eh, ActorPC pc)
        {
            TitleProccess(pc, 12, 1);
        }

        void Ondie(MobEventHandler e, ActorPC pc)
        {
            TitleProccess(pc, 2, 1);
            string day = DateTime.Now.ToString("d");
            SInt[day + "我兔汉三死亡次数"]++;
            if (Global.Random.Next(0, 100) < 50)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10054001);
                ActorMob mob = map.SpawnCustomMob(10000000, map.ID, 18380002, 0, 0, 38, 208, 31, 1, 0, 我兔汉二Info(), 我兔汉二AI(), null, 0)[0];
                SagaMap.Skill.SkillHandler.Instance.ActorSpeak(mob, "泥萌居然欺负窝的噢多多！看窝不来打屎泥萌！！");
                ((MobEventHandler)mob.e).Dying += (k, y) =>
                {
                    string day2 = DateTime.Now.ToString("d");
                    SInt[day2 + "我兔汉二死亡次数"]++;
                    if (Global.Random.Next(0, 100) < 50)
                    {
                        map = SagaMap.Manager.MapManager.Instance.GetMap(10056000);
                        mob = map.SpawnCustomMob(10000000, map.ID, 18380000, 0, 0, 143, 215, 58, 1, 0, 兔汉一Info(), 兔汉一AI(), null, 0)[0];
                        Announce("我兔汉一：看来你们根本不懂原力的力量！来牛牛草原见我！我教你们做人！");
                        ((MobEventHandler)mob.e).Dying += (s, a) =>
                        {
                            foreach (var item in ((MobEventHandler)mob.e).AI.DamageTable)
                            {
                                Actor ac = map.GetActor(item.Key);
                                if (ac == null) continue;
                                if (ac.type == SagaDB.Actor.ActorType.PC)
                                {
                                    ActorPC apc = (ActorPC)ac;
                                    /*if (apc.AInt["弱肉强食领取"] != 2)
                                    {
                                        apc.AInt["弱肉强食领取"] = 2;
                                        GiveItem(apc, 130000025, 1);
                                        SagaMap.Network.Client.MapClient.FromActorPC(apc).SendSystemMessage("参与消灭我兔汗一 ，获得【弱肉强食】称号！");
                                    }*/
                                }
                            }
                        };
                    }
                };
            }
        }
    }
}

