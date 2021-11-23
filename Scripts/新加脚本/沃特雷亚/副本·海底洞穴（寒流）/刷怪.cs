
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaMap.Skill;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
using SagaMap.Skill.Additions.Global;
namespace 海底副本
{
    public partial class 海底副本 : Event
    {
        void 生成怪物(ActorPC pc, Difficulty diff, bool single)
        {
            Mission1Boss(pc, diff, single);
            Mission2Boss(pc, diff, single);
            Mission3Boss(pc, diff, single);
            Mission1Mobs(pc, diff, single);
        }
        void Mission1Mobs(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S21180001", single));
            map.SpawnCustomMob(10000000, map.ID, 14150800, 0, 0, 125, 125, 250, 8, 0, 螺贝Info(diff), 螺贝AI(diff), 血量加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 14130000, 0, 0, 125, 125, 250, 9, 0, 海马Info(diff), 海马AI(diff), 攻击加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 125, 125, 250, 10, 0, 海龙Info(diff), 海龙AI(diff), 攻击加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 14160000, 0, 0, 125, 125, 250, 9, 0, 气泡水母Info(diff), 气泡水母AI(diff), 血量加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10060600, 0, 0, 125, 125, 250, 12, 0, 天使飞鱼Info(diff), 天使飞鱼AI(diff), 攻击加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 10870900, 0, 0, 125, 125, 250, 10, 0, 黑色水晶海胆Info(diff), 黑色水晶海胆AI(diff), 血量加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 14120000, 0, 0, 125, 125, 250, 8, 0, 水晶龟Info(diff), 水晶龟AI(diff), 血量加成ondie, 1);
            map.SpawnCustomMob(10000000, map.ID, 15181100, 0, 0, 125, 125, 250, 9, 0, 深色鱼人Info(diff), 深色鱼人AI(diff), 攻击加成ondie, 1);
        }
        private void 血量加成ondie(MobEventHandler eh, ActorPC pc)
        {
            List<ActorPC> pcs = new List<ActorPC>();
            if (pc.Party == null)
                pcs.Add(pc);
            else
                foreach (var item in pc.Party.Members.Values)
                    if (item.MapID == pc.MapID)
                        pcs.Add(item);
            foreach (var item in pcs)
            {
                item.TInt["临时HP"] += 50;
                SagaMap.PC.StatusFactory.Instance.CalcStatus(item);
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC(item);
                client.SendStatus();
                client.SendStatusExtend();
                client.SendSystemMessage("你获得了海神的祝福！血量上限临时提升：" + item.TInt["临时HP"]);
                SkillHandler.Instance.ShowEffectOnActor(item, 5360);
            }
        }
        private void 攻击加成ondie(MobEventHandler eh, ActorPC pc)
        {
            List<ActorPC> pcs = new List<ActorPC>();
            if (pc.Party == null)
                pcs.Add(pc);
            else
                foreach (var item in pc.Party.Members.Values)
                    if (item.MapID == pc.MapID)
                        pcs.Add(item);
            foreach (var item in pcs)
            {
                item.TInt["临时攻击上升"] += 1;
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC(item);
                client.SendSystemMessage("你获得了海神的祝福！造成的伤害临时提升：" + item.TInt["临时攻击上升"] + "％");
                SkillHandler.Instance.ShowEffectOnActor(item, 5359);
            }
        }
        void Mission1Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S21180001", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 17160000, 0, 10010100, 1, 221, 29, 0, 1, 0, 鱼人长老Info(diff), 鱼人长老AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["鱼人长老死亡次数"]++;
            int count = 2;
            if (diff == Difficulty.Normal)
                count = 7;
            for (int i = 0; i < count; i++)
            {
                ActorMob Slave = map.SpawnCustomMob(10000000, map.ID, 26100001, 0, 10010100, 1, 221, 29, 0, 1, 0, 鱼人干部Info(diff), 鱼人干部AI(diff), null, 0)[0];
                ((MobEventHandler)Slave.e).AI.Master = mobS;
                mobS.SettledSlave.Add(Slave);
            }
        }
        void Mission2Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S21180001", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 18500050, 0, 10010100, 1, 92, 116, 0, 1, 0, 利维坦Info(diff), 利维坦AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["利维坦死亡次数"]++;
mobS.TInt["playersize"] = 1500;
            int count = 2;
            if (diff == Difficulty.Normal)
                count = 7;
            for (int i = 0; i < count; i++)
            {
                ActorMob Slave = map.SpawnCustomMob(10000000, map.ID, 18450200, 0, 10010100, 1, 92, 116, 0, 1, 0, 鲨鱼Info(diff), 鲨鱼AI(diff), null, 0)[0];
                ((MobEventHandler)Slave.e).AI.Master = mobS;
                mobS.SettledSlave.Add(Slave);
            }
        }
        void Mission3Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S21180001", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 15580200, 0, 10010100, 1, 209, 235, 0, 1, 0, 常暗Info(diff), 常暗AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["常暗死亡次数"]++;
mobS.TInt["playersize"] = 700;
            ((Actor)mobS).TInt["海神的诅咒"] = 1;
            int count = 2;
            if (diff == Difficulty.Normal)
                count = 7;
            for (int i = 0; i < count; i++)
            {
                ActorMob Slave = map.SpawnCustomMob(10000000, map.ID, 26390000, 0, 10010100, 1, 209, 235, 0, 1, 0, 常闇的従者Info(diff), 常闇的従者AI(diff), null, 0)[0];
                ((MobEventHandler)Slave.e).AI.Master = mobS;
                mobS.SettledSlave.Add(Slave);
            }
        }
    }
}