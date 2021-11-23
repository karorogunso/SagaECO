
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
using SagaMap.ActorEventHandlers;
using SagaMap.Mob;
using SagaDB.Mob;
namespace SagaScript.M30210000
{
    public class S2018 : Event
    {
        public S2018()
        {
            this.EventID = 2018;
        }

        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            switch (Select(pc, "！！！！", "", "改变地图玩家的PK模式 ", "刷花", "卧槽我点错了！", "S", "全体回复"))
            {
                case 1:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004)
                        {

                            if (item.Character == pc)
                            {
                                item.Character.Mode = PlayerMode.KNIGHT_NORTH;

                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, item.Character, true);
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item.Character, true);

                                item.SendCharInfoUpdate();
                            }
                            else
                            {
                                item.Character.Mode = PlayerMode.KNIGHT_EAST;

                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, item.Character, true);
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item.Character, true);

                                item.SendCharInfoUpdate();
                            }
                        }
                    }
                    break;
                case 2:
                    List<ActorMob> mobs = map.SpawnCustomMob(10000000, 10071004, 30210000, 0, 0, 125, 125, 125, 20, 0, 花(), 花AI(), null, 0);
                    for (int i = 0; i < mobs.Count; i++)
                    {
                        ((MobEventHandler)mobs[i].e).Dying += (k, y) =>
                        {
                            if (y.Mode == PlayerMode.NORMAL)
                            {
                                if (SagaLib.Global.Random.Next(0, 100) < 20)
                                {
                                    y.Mode = PlayerMode.KNIGHT_EAST;

                                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, y, true);
                                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, y, true);
                                }
                            }
                            else if (y.Mode == PlayerMode.KNIGHT_EAST)
                            {
                                int d = (int)(y.MaxHP * 0.1f);
                                y.HP += (uint)d;
                                if (y.HP > y.MaxHP)
                                    y.HP = y.MaxHP;
                                SagaMap.Skill.SkillHandler.Instance.ShowVessel(y, -d, 0, 0, SagaMap.Skill.SkillHandler.AttackResult.Hit);
                                if (SagaLib.Global.Random.Next(0, 100) < 100)
                                {
                                    List<Actor> actors = map.GetActorsArea(y, 2000, false);
                                    foreach (var item in actors)
                                    {
                                        if (item.type == ActorType.PC && item != y)
                                        {
                                            SagaMap.Skill.Additions.Global.Freeze f = new SagaMap.Skill.Additions.Global.Freeze(null, item, 5000, 0);
                                            SagaMap.Skill.SkillHandler.ApplyAddition(item, f);
                                        }
                                    }
                                }
                            }
                        };
                    }
                    break;
                case 4:
                    if (Select(pc, "阿萨德", "", "敌方", "己方") == 1)
                    {
                        pc.Mode = PlayerMode.KNIGHT_WEST;

                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, pc, true);
                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                    }
                    else
                    {
                        pc.Mode = PlayerMode.KNIGHT_EAST;

                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, pc, true);
                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                    }
                    break;
                case 5:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004)
                        {

                            item.Character.Mode = PlayerMode.NORMAL;

                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, null, item.Character, true);
                            map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item.Character, true);

                            item.SendCharInfoUpdate();
                        }
                    }
                    break;
            }
        }

        ActorMob.MobInfo 花()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "花";
            info.maxhp = 500;
            info.speed = 336;
            info.atk_min = 6;
            info.atk_max = 63;
            info.matk_min = 590;
            info.matk_max = 690;
            info.def = 1;
            info.def_add = 1;
            info.mdef = 5;
            info.mdef_add = 50;
            info.hit_critical = 14;
            info.hit_magic = 50;
            info.hit_melee = 78;
            info.hit_ranged = 79;
            info.avoid_critical = 14;
            info.avoid_magic = 24;
            info.avoid_melee = 60;
            info.avoid_ranged = 58;
            info.Aspd = 550;
            info.Cspd = 440;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;
            info.baseExp = info.maxhp / 15;
            info.jobExp = info.maxhp / 15;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }

        AIMode 花AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            return ai;
        }
    }
}