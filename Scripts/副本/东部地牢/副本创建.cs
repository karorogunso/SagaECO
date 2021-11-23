
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace 东部地牢副本
{
    public partial class 东部地牢 : Event
    {
        void 初始化变量(ActorPC pc)
        {
            pc.TInt["东牢幻觉"] = 0;
            pc.TInt["东牢D-1"] = 0;
            pc.TInt["东牢D-2"] = 0;
            pc.TInt["东牢D-3"] = 0;
			pc.TInt["击杀boss1"] = 0;
			pc.TInt["击杀boss2"] = 0;
			pc.TInt["击杀boss3"] = 0;
            int count = CountItem(pc, 110003500);
            if (count >= 1)
            {
                TakeItem(pc, 110003500, (ushort)count);
                GiveItem(pc, 110003600, (ushort)count);
                SagaMap.Skill.SkillHandler.SendSystemMessage(pc, "包里面有东西腐烂了");
                //Say(pc, 0, "果实发生了变化！！");
            }
        }
        void 附加单人普通BUFF(ActorPC pc)
        {
            pc.Buff.单枪匹马 = true;
        }
        void 附加多人普通BUFF(ActorPC pc)
        {
            pc.Buff.黑暗压制 = true;
        }
        bool 生成地图单人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party != null)//有队伍不生成
                return false;

            pc.TInt["S20090000"] = CreateMapInstance(20090000, 30131001, 6, 8, true, 0, true);//东方地牢
            pc.TInt["S20091000"] = CreateMapInstance(20091000, 30131001, 6, 8, true, 0, true);//毒湿沼泽
            pc.TInt["S20092000"] = CreateMapInstance(20092000, 30131001, 6, 8, true, 0, true);//原始森林

            pc.TInt["S60903000"] = CreateMapInstance(60903000, 30131001, 6, 8, true, 0, true);//隐藏BOSS房间

            return true;
        }
        bool 生成地图多人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party == null)//没有队伍不生成
                return false;
            if (pc != pc.Party.Leader)//不是队长不生成
                return false;

            pc.Party.TInt["S20090000"] = CreateMapInstance(20090000, 30131001, 6, 8, true, 0, true);//东方地牢
            pc.Party.TInt["S20091000"] = CreateMapInstance(20091000, 30131001, 6, 8, true, 0, true);//毒湿沼泽
            pc.Party.TInt["S20092000"] = CreateMapInstance(20092000, 30131001, 6, 8, true, 0, true);//原始森林

            pc.Party.TInt["S60903000"] = CreateMapInstance(60903000, 30131001, 6, 8, true, 0, true);//隐藏BOSS房间

            pc.Party.MaxMember = MaxMember;//让队伍成员上限改变
            return true;
        }
        void 生成怪物(ActorPC pc, Difficulty diff,bool single)
        {
            Mission1Mobs(pc, diff, single);
            Mission2Mobs(pc, diff, single);
            Mission3Mobs(pc, diff, single);
            Mission1Boss(pc, diff, single);
            Mission2Boss(pc, diff, single);
            Mission3Boss(pc, diff, single);
            MissionXBoss(pc, diff, single);
        }
        void 传送单人(ActorPC pc, Difficulty diff)
        {
            SetReviveCountForSingle(pc  , diff);
            if (pc.QuestRemaining < QuestPoint)
                return;
            pc.QuestRemaining -= QuestPoint;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
            Warp(pc, (uint)pc.TInt["S20090000"], 113, 252);
            SetNextMoveEvent(pc, 80001700);
            ushort count = (ushort)CountItem(pc, 110003500);
            if (count > 0)
                TakeItem(pc, 110003500, count);
        }
        void 传送多人(ActorPC pc, Difficulty diff)
        {
            foreach (var item in pc.Party.Members.Values)//遍历队伍的成员
            {
                if (item != null && item.Online)
                {
                    SetReviveCount(item, diff);
                    附加多人普通BUFF(item);
                    初始化变量(item);
                    Warp(item,(uint)pc.Party.TInt["S20090000"], 113, 252);
                    SetNextMoveEvent(item, 80001700);
                    ushort count = (ushort)CountItem(item, 110003500);
                    if (count > 0)
                        TakeItem(item, 110003500, count);
                }
            }
        }
        #region 刷怪
        uint GetMapID(ActorPC pc, string name, bool single)
        {
            uint mapid = 0;
            if (single) mapid = (uint)pc.TInt[name];
            else mapid = (uint)pc.Party.TInt[name];
            return mapid;
        }
        void Mission1Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20090000", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 14670200, 0, 10010100, 1, 188, 81, 0, 1, 0, 贪婪领主Info(diff), 贪婪领主AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => {
				SInt["贪婪领主死亡次数"]++;
				if (single) pc.TInt["击杀boss1"]=1;
				else pc.Party.TInt["击杀boss1"]=1;
				};
            mobS.TInt["playersize"] = 1500;
        }
        void Mission2Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20091000", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 15380600, 0, 10010100, 1, 135, 135, 0, 1, 0, 腐毒丧尸Info(diff), 腐毒丧尸AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => {SInt["腐毒丧尸死亡次数"]++;				if (single) pc.TInt["击杀boss2"]=1;
				else pc.Party.TInt["击杀boss2"]=1;};
            mobS.TInt["playersize"] = 5000;
        }
        void Mission3Boss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20092000", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 15510200, 0, 10010100, 1, 214, 88, 0, 1, 0, 暗黑兔偶Info(diff), 暗黑兔偶AI(diff), null, 0)[0];
            ((MobEventHandler)mobS.e).Dying += (s, e) => {SInt["暗黑兔偶死亡次数"]++;				if (single) pc.TInt["击杀boss3"]=1;
				else pc.Party.TInt["击杀boss3"]=1;};
            mobS.TInt["playersize"] = 1500;
        }
        void MissionXBoss(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S60903000", single));
            ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 16010000, 0, 10010100, 1, 47, 17, 0, 1, 0, 夺魂者Info(diff), 夺魂者AI(diff), null, 0)[0];
			if (diff == Difficulty.Single_Hard || diff == Difficulty.Hard)
				mobS.TInt["难度"] = 2;
            mobS.TInt["playersize"] = 1500;
            ((MobEventHandler)mobS.e).Dying += (s, e) =>
            {
                if (pc.Party != null)
                {
                    foreach (var item in pc.Party.Members.Values)
                    {
                        TitleProccess(item, 5, 1);
                        if (item.Online)//&& item.HP > 0
                        {
                            SagaMap.Network.Client.MapClient.FromActorPC(item).EventActivate(100000104);
                        }
                    }
                    SInt["夺魂者死亡次数"]++;
                }
                else
				{
					TitleProccess(pc, 5, 1);
					SagaMap.Network.Client.MapClient.FromActorPC(pc).EventActivate(100000104);
				}
            };
        }

        void Mission1Mobs(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20090000", single));
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 100, 220, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 117, 214, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 121, 191, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 115, 179, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 222, 28, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 213, 20, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 145, 186, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 148, 192, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 190, 154, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 170, 152, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 100, 42, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 110, 40, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 155, 66, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350000, 0, 0, 140, 55, 1, 1, 0, 骷髅弓箭手Info(diff), 骷髅弓箭手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 103, 214, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 122, 135, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 93, 143, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 99, 134, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 221, 21, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 181, 156, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 56, 117, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 49, 112, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 112, 137, 1, 1, 0, 亡灵装甲Info(diff), 亡灵装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 206, 228, 1, 1, 0, 亡灵装甲Info(diff), 亡灵装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 102, 51, 1, 1, 0, 亡灵装甲Info(diff), 亡灵装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 139, 103, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 144, 103, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 140, 68, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 147, 70, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 177, 198, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 183, 204, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 37, 174, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 46, 165, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 56, 157, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 76, 96, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 93, 81, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 148, 54, 1, 1, 0, 骷髅击剑士Info(diff), 骷髅击剑士AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 44, 128, 1, 1, 0, 骷髅击剑士Info(diff), 骷髅击剑士AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 189, 28, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 198, 39, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 178, 43, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 168, 172, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 184, 173, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 213, 198, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 221, 191, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 165, 191, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 160, 178, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 54, 195, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251100, 0, 0, 64, 180, 1, 1, 0, 死亡守卫Info(diff), 死亡守卫AI(diff), null, 0);


            //植物
            map.SpawnCustomMob(10000000, map.ID, 30150000, 0, 10010100, 2, 125, 125, 125, 7, 0, 黯淡浆果草Info(diff), 黯淡浆果草AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 30120000, 0, 10010100, 2, 125, 125, 125, 6, 0, 潮湿花生Info(diff), 潮湿花生AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 30540000, 0, 10010100, 2, 125, 125, 125, 6, 0, 发黄的蚕豆Info(diff), 发黄的蚕豆AI(diff), null, 0);
        }
        void Mission2Mobs(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20091000", single));
            map.SpawnCustomMob(10000000, map.ID, 10421000, 0, 0, 198, 193, 1, 1, 0, 亡灵装甲Info(diff), 亡灵装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10251200, 0, 0, 188, 80, 1, 1, 0, 狩魂者Info(diff), 狩魂者AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250500, 0, 0, 113, 203, 1, 1, 0, 奥迦斯Info(diff), 奥迦斯AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10420000, 0, 0, 200, 173, 1, 1, 0, 幽灵盔甲Info(diff), 幽灵盔甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10420000, 0, 0, 205, 159, 1, 1, 0, 幽灵盔甲Info(diff), 幽灵盔甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10420000, 0, 0, 232, 156, 1, 1, 0, 幽灵盔甲Info(diff), 幽灵盔甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10420000, 0, 0, 232, 176, 1, 1, 0, 幽灵盔甲Info(diff), 幽灵盔甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10420000, 0, 0, 34, 152, 1, 1, 0, 幽灵盔甲Info(diff), 幽灵盔甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421100, 0, 0, 215, 137, 1, 1, 0, 贵族装甲Info(diff), 贵族装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421100, 0, 0, 226, 130, 1, 1, 0, 贵族装甲Info(diff), 贵族装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421100, 0, 0, 215, 118, 1, 1, 0, 贵族装甲Info(diff), 贵族装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10421100, 0, 0, 41, 92, 1, 1, 0, 贵族装甲Info(diff), 贵族装甲AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 149, 107, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 137, 136, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 196, 15, 30, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 196, 15, 30, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 196, 15, 30, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, 41, 110, 1, 1, 0, 血腥死神Info(diff), 血腥死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 141, 109, 1, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 154, 112, 1, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 61, 46, 1, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 61, 57, 1, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 196, 15, 30, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 196, 15, 30, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10250000, 0, 0, 196, 15, 30, 1, 0, 死神Info(diff), 死神AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 201, 108, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 207, 102, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 163, 106, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 169, 114, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 119, 164, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 132, 172, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 88, 105, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 98, 99, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 82, 112, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 33, 123, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10350100, 0, 0, 42, 127, 1, 1, 0, 骷髅射手Info(diff), 骷髅射手AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 35, 72, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 43, 74, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 30, 202, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 45, 200, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10190000, 0, 0, 38, 205, 1, 1, 0, 骷髅士兵Info(diff), 骷髅士兵AI(diff), null, 0);

            //植物
            map.SpawnCustomMob(10000000, map.ID, 30070009, 0, 10010100, 2, 125, 125, 125, 15, 0, 黑白斑点菇Info(diff), 黑白斑点菇AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 30550700, 0, 10010100, 2, 196, 15, 30, 30, 0, 解毒草Info(diff), 解毒草AI(diff), null, 0);
        }
        void Mission3Mobs(ActorPC pc, Difficulty diff, bool single)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(GetMapID(pc, "S20092000", single));
            map.SpawnCustomMob(10000000, map.ID, 10139100, 0, 0, 229, 148, 1, 1, 0, 桃狼Info(diff), 桃狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10139100, 0, 0, 41, 241, 1, 1, 0, 桃狼Info(diff), 桃狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136011, 0, 0, 205, 226, 1, 1, 0, 黑金狼Info(diff), 黑金狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136011, 0, 0, 42, 32, 1, 1, 0, 黑金狼Info(diff), 黑金狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130700, 0, 0, 181, 223, 42, 5, 0, 狼Info(diff), 狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130700, 0, 0, 50, 87, 63, 6, 0, 狼Info(diff), 狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130400, 0, 0, 200, 177, 25, 3, 0, 冰狼Info(diff), 冰狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130400, 0, 0, 50, 87, 63, 6, 0, 冰狼Info(diff), 冰狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130400, 0, 0, 114, 136, 1, 1, 0, 冰狼Info(diff), 冰狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130400, 0, 0, 116, 141, 1, 1, 0, 冰狼Info(diff), 冰狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130100, 0, 0, 200, 177, 25, 5, 0, 红狼Info(diff), 红狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130100, 0, 0, 50, 87, 63, 6, 0, 红狼Info(diff), 红狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130100, 0, 0, 42, 214, 1, 1, 0, 红狼Info(diff), 红狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10130100, 0, 0, 48, 214, 1, 1, 0, 红狼Info(diff), 红狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 181, 223, 42, 8, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 50, 87, 63, 8, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 94, 212, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 85, 212, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 81, 183, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 67, 160, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 73, 149, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 78, 162, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 0, 70, 167, 1, 1, 0, 魔狼Info(diff), 魔狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136050, 0, 0, 181, 223, 42, 5, 0, 灰狼Info(diff), 灰狼AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10136050, 0, 0, 50, 87, 63, 6, 0, 灰狼Info(diff), 灰狼AI(diff), null, 0);

            //植物
            map.SpawnCustomMob(10000000, map.ID, 30150000, 0, 10010100, 2, 125, 125, 125, 7, 0, 黯淡浆果草Info(diff), 黯淡浆果草AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 30120000, 0, 10010100, 2, 125, 125, 125, 6, 0, 潮湿花生Info(diff), 潮湿花生AI(diff), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 30540000, 0, 10010100, 2, 125, 125, 125, 6, 0, 发黄的蚕豆Info(diff), 发黄的蚕豆AI(diff), null, 0);

        }
        #endregion
    }
}