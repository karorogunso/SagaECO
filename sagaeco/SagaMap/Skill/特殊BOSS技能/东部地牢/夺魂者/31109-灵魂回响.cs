using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31109 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "迷失在幻象中的灵魂，来加入这合唱吧！");

            /*-------------------获取灵魂漩涡-----------------*/
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
            List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
            actors = map.GetActorsArea(sActor, 2000, false);//获取sActor周围10格内的所有Actor，并装在actors里
            byte count = 0;//黑洞计数
            foreach (var item in actors)//遍历刚刚获得的actors
            {
                if(item != null && item.type == ActorType.SKILL && item.Name == "灵魂漩涡" )
                {
                    count++;
                    byte x = SagaLib.Global.PosX16to8(item.X, map.Width);//取得技能体的坐标，并转换成2D坐标
                    byte y = SagaLib.Global.PosY16to8(item.Y, map.Height);//取得技能体的坐标，并转换成2D坐标
                    SkillHandler.Instance.ShowEffect(map, sActor, x, y, 5044);

                    活死人 timer = new 活死人(sActor, x, y);
                    timer.Activate();
                }
            }
            if(count >0)//如果有黑洞计数
            {
                //活死人自爆计时 timer = new 活死人自爆计时(sActor);
                //timer.Activate();//启动计时器
            }
        }
        private class 活死人 : MultiRunTask
        {
            Actor caster;
            Map map;
            byte x, y;
            public 活死人(Actor caster, byte x, byte y)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 3000;
                this.x = x;
                this.y = y;
            }

            public override void CallBack()
            {
                try
                {
                    ActorMob mob = map.SpawnCustomMob(10560000, map.ID, 10690500, 0, 0, x, y, 0, 1, 0, Info(caster.TInt["难度"]), AI(), null, 0)[0];

                    ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = caster;
                    if (caster.type == ActorType.MOB)
                        ((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)caster.e).AI.Hate;
                    mob.Owner = caster;
                    caster.Slave.Add(mob);
                    SkillHandler.Instance.ShowEffectByActor(mob, 4111);
                    活死人自爆计时 timer = new 活死人自爆计时(mob);
                    timer.Activate();
                    Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            ActorMob.MobInfo Info(int diff)//怪物属性
            {
                ActorMob.MobInfo info = new ActorMob.MobInfo();
                info.name = "活死人";
                if (diff>1)//困难难度
                {
                    info.maxhp = 30000;
                    info.speed = 390;
                    if (diff > 2)//困难难度组队
                    {
                        info.maxhp = 50000;
                        info.speed = 750;
                    }
                    info.atk_min = 240;
                    info.atk_max = 484;
                    info.matk_min = 11400;
                    info.matk_max = 13400;
                    info.def = 21;
                    info.def_add = 220;
                    info.mdef = 21;
                    info.mdef_add = 118;
                    info.hit_critical = 33;
                }
                else
                {
                    info.maxhp = 10050;
                    info.speed = 390;
                    info.atk_min = 120;
                    info.atk_max = 242;
                    info.matk_min = 5700;
                    info.matk_max = 6700;
                    info.def = 21;
                    info.def_add = 121;
                    info.mdef = 21;
                    info.mdef_add = 118;
                    info.hit_critical = 23;
                }

                info.hit_magic = 118;
                info.hit_melee = 118;
                info.hit_ranged = 120;
                info.avoid_critical = 24;
                info.avoid_magic = 59;
                info.avoid_melee = 60;
                info.avoid_ranged = 60;
                info.Aspd = 540;
                info.Cspd = 540;
                info.elements[SagaLib.Elements.Neutral] = 0;
                info.elements[SagaLib.Elements.Fire] = 50;
                info.elements[SagaLib.Elements.Water] = 0;
                info.elements[SagaLib.Elements.Wind] = 0;
                info.elements[SagaLib.Elements.Earth] = 0;
                info.elements[SagaLib.Elements.Holy] = 40;
                info.elements[SagaLib.Elements.Dark] = 0;
                info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 30;
                info.baseExp = 100;
                info.jobExp = 100;
                /*---------物理掉落---------*/

                return info;
            }

            AIMode AI()//怪物AI
            {
                AIMode ai = new AIMode(1);//1為主動，0為被動
                ai.AI = 1;
                ai.MobID = 10111302;//怪物ID
                ai.isNewAI = true;
                ai.Distance = 1;
                ai.ShortCD = 3;
                ai.LongCD = 3;
                AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

                /*---------自爆---------*/
                //skillinfo.CD = 3;//技能CD
                //skillinfo.Rate = 100;//釋放概率
                //skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
                //skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
                //ai.SkillOfShort.Add(31005, skillinfo);//將這個技能加進進程技能表
                return ai;
            }
        }


        class 活死人自爆计时 : MultiRunTask
        {
            Actor sActor;
            public 活死人自爆计时(Actor sActor)
            {
                this.sActor = sActor;
                this.dueTime = 10000;
            }
            public override void CallBack()
            {
                try
                {
                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
                    List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
                    actors = map.GetActorsArea(sActor, 300, false);//获取sActor周围10格内的所有Actor，并装在actors里
                    int damage = (int)sActor.HP;//定一个总伤害
                    if (damage > 0)//如果有伤害
                    {
                        foreach (var item in actors)//再次遍历刚刚获得的actors
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                            {
                                SkillHandler.Instance.CauseDamage(sActor, item, damage);
                                SkillHandler.Instance.ShowVessel(item, damage);
                                //要显示伤害特效
                            }
                        }
                    }
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4030);
                    map.DeleteActor(sActor);
                    this.Deactivate();//结束计时器
                }
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();//结束计时器
                }
            }
        }


    }
}
