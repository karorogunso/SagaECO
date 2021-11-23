
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using System.Globalization;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;

namespace WeeklyExploration
{
    public partial class GTQuest : Event
    {
        public GTQuest()
        {
            this.EventID = 500020135;
        }
        void 创建副本(ActorPC pc)
        {

        }
        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "GM操作台", "", "创建副本", "刷怪老一", "刷怪老二", "刷怪老三", "问全服传老一", "问全服传老二", "问全服传老三", "全服BUFF", "全员回家", "离开"))
            {
                case 1:
                    pc.TInt["副本复活标记"] = 1;
                    pc.Party.Leader.TInt["复活次数"] = 16;
                    pc.Party.Leader.TInt["设定复活次数"] = 16;
                    SInt["S20140000"] = CreateMapInstance(20140000, 10054000, 21, 21, true, 0, true);//主场馆1F
                    SInt["S20163000"] = CreateMapInstance(20163000, 10054000, 21, 21, true, 0, true);//光塔上层
                    SInt["S20146000"] = CreateMapInstance(20146000, 10054000, 21, 21, true, 0, true);//光塔下层
                    break;
                case 2:
                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)SInt["S20140000"]);
                    ActorMob a = map.SpawnCustomMob(10000000, map.ID, 19010157, 0, 0, 77, 74, 0, 1, 0, 老一Info活动(), 老一AI活动(), null, 0)[0];
                    ((MobEventHandler)a.e).Defending += KitaAreaBOSS_Defending;
                    break;
                case 3:
                    SagaMap.Map map2 = SagaMap.Manager.MapManager.Instance.GetMap((uint)SInt["S20146000"]);
                    ActorMob b = map2.SpawnCustomMob(10000000, map2.ID, 18630000, 0, 0, 92, 164, 0, 1, 0, 老二Info活动(), 老二AI活动(), null, 0)[0];
                    ((MobEventHandler)b.e).Defending += KitaAreaBOSS_Defending;
                    break;
                case 4:
                    SagaMap.Map map3 = SagaMap.Manager.MapManager.Instance.GetMap((uint)SInt["S20163000"]);
                    ActorMob c = map3.SpawnCustomMob(10000000, map3.ID, 18610050, 0, 0, 149, 63, 0, 1, 0, 老三Info活动(), 老三AI活动(), null, 0)[0];
                    ((MobEventHandler)c.e).Defending += KitaAreaBOSS_Defending;
                    break;
                case 5:
                    Warp(pc, (uint)SInt["S20140000"], 38, 53);
                    pc.TInt["复活次数"] = 3;
                    pc.TInt["设定复活次数"] = 3;
                    pc.TInt["副本复活标记"] = 3;
                    pc.TInt["伤害统计"] = 0;
                    pc.TInt["受伤害统计"] = 0;
                    pc.TInt["受治疗统计"] = 0;
                    pc.TInt["治疗溢出统计"] = 0;
                    pc.TInt["死亡统计"] = 0;
                    break;
                case 6:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online && pc.MapID == item.Character.MapID)
                        {
                            Warp(item.Character, (uint)SInt["S20163000"], 144, 99);
                            item.Character.TInt["复活次数"] = 3;
                            item.Character.TInt["设定复活次数"] = 3;
                            item.Character.TInt["副本复活标记"] = 3;
                            item.Character.TInt["伤害统计"] = 0;
                            item.Character.TInt["受伤害统计"] = 0;
                            item.Character.TInt["受治疗统计"] = 0;
                            item.Character.TInt["治疗溢出统计"] = 0;
                            item.Character.TInt["死亡统计"] = 0;
                        }
                    }
                    break;
                case 7:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online && pc.MapID == item.Character.MapID)
                        {
                            Warp(item.Character, (uint)SInt["S20146000"], 124, 144);
                            item.Character.TInt["复活次数"] = 3;
                            item.Character.TInt["设定复活次数"] = 3;
                            item.Character.TInt["副本复活标记"] = 3;
                            item.Character.TInt["伤害统计"] = 0;
                            item.Character.TInt["受伤害统计"] = 0;
                            item.Character.TInt["受治疗统计"] = 0;
                            item.Character.TInt["治疗溢出统计"] = 0;
                            item.Character.TInt["死亡统计"] = 0;
                        }
                    }
                    break;
                case 8:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online)
                        {
                                item.Character.TInt["临时HP"] = 0;
                                item.Character.TInt["临时ATK"] = 0;
                                item.Character.TInt["临时MATK"] = 0;
                            //Revive(item.Character, 5);
                            pc.TInt["副本复活标记"] =0;
                            //item.Character.TInt["复活次数"] = 50;
                            //item.Character.TInt["设定复活次数"] = 50;
                            //Say(item.Character, 0, "你被原力强化了！！$R复活次数回复了！$R$CR攻击力上升1500$CD$R$CRHP上升10000$CD", "羽川柠");
                            SagaMap.Skill.Additions.Global.OtherAddition oa = new SagaMap.Skill.Additions.Global.OtherAddition(null, item.Character, "撒打算", 60000);
                            oa.OnAdditionStart += (s, e) =>
                            {
                                item.Character.TInt["副本复活标记"] = 0;
                                item.Character.TInt["临时HP"] = 00;
                                item.Character.TInt["临时ATK"] = 00;
                                item.Character.TInt["临时MATK"] = 00;
                                SagaMap.PC.StatusFactory.Instance.CalcStatus(item.Character);
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendStatus();
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendStatusExtend();
                                //SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage("你被原力强化了。");
                            };
                            oa.OnAdditionEnd += (s, e) =>
                            {
                                item.Character.TInt["临时HP"] = 0;
                                item.Character.TInt["临时ATK"] = 0;
                                item.Character.TInt["临时MATK"] = 0;
                                SagaMap.PC.StatusFactory.Instance.CalcStatus(item.Character);
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendStatus();
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendStatusExtend();
                            };
                            SagaMap.Skill.SkillHandler.ApplyAddition(item.Character, oa);
                            //SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(item.Character, 5217);
                        }
                    }
                    break;
                case 9:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online && (pc.MapID == SInt["S20140000"] || pc.MapID == SInt["S20163000"] || pc.MapID == SInt["S20146000"]))
                        {
                            Warp(item.Character, 30131001, 6, 5);
                        }
                    }
                    break;
            }
        }
        private void KitaAreaBOSS_Defending(MobEventHandler eh, ActorPC pc)
        {
            ActorMob mob = eh.mob;
            if (mob.HP < 50 && eh.mob.AttackedForEvent != 1)
            {
                SagaMap.Map map;
                map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                List<Actor> actors = map.GetActorsArea(mob, 5000, false);
                eh.mob.AttackedForEvent = 1;
                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC m = (ActorPC)item;
                        if (m.Online && m != null)
                        {
                            if (m.Buff.Dead)
                                SagaMap.Network.Client.MapClient.FromActorPC(m).RevivePC(m);
                            string s = "玩家 " + m.Name + " 在本次战斗总共造成伤害：" + m.TInt["伤害统计"].ToString() + " 受到伤害：" + m.TInt["受伤害统计"].ToString() +
                                   " 共治疗：" + m.TInt["治疗统计"].ToString() + " 共受到治疗：" + m.TInt["受治疗统计"].ToString();
                            foreach (var item2 in actors)
                            {
                                if (item2.type == ActorType.PC)
                                {
                                    ActorPC m2 = (ActorPC)item2;
                                    if (m2.Online && m2 != null)
                                        SagaMap.Network.Client.MapClient.FromActorPC(m2).SendSystemMessage(s);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

