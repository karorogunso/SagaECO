using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;
using SagaMap.Scripting;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31050 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);
            Actor target = actors[SagaLib.Global.Random.Next(0, actors.Count - 1)];

            SkillHandler.Instance.MagicAttack(sActor, target, args, Elements.Dark, 5f);

            byte x, y;
            x = SagaLib.Global.PosX16to8(target.X, map.Width);
            y = SagaLib.Global.PosY16to8(target.Y, map.Height);

            SkillHandler.Instance.ShowEffect(map, sActor, x, y, 5003);

            Activator timer = new Activator(sActor,x,y);
            timer.Activate();

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            byte x, y;
            byte mark = 0;
            public Activator(Actor caster,byte x,byte y)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                period = 10000;
                dueTime = 3000;
                this.x = x;
                this.y = y;
            }
            public override void CallBack()
            {
                try
                {
                    if (mark == 0)
                    {
                        if (caster.type == ActorType.MOB)
                        {
                            ((ActorMob)caster).TInt["鱼人加护"] = 1;
                        }
                        ActorMob mob = map.SpawnCustomMob(10560000, map.ID, 15180000, 0,0, 0, x, y, 0, 1, 0, 天兵鱼人info((uint)(caster.MaxHP * 0.3f)), 天兵鱼人ai(), (MobCallback)死亡Ondie, 1)[0];
                        ((MobEventHandler)mob.e).AI.Master = caster;
                        if (caster.type == ActorType.MOB)
                            ((MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)caster.e).AI.Hate;
                        mob.Owner = caster;
                        caster.Slave.Add(mob);
                        SkillHandler.Instance.ShowEffectByActor(mob, 4111);
                        mark = 1;
                    }
                    else
                    {
                        ((ActorMob)caster).TInt["鱼人加护"] = 0;
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            ActorMob.MobInfo 天兵鱼人info(uint hp)
            {
                ActorMob.MobInfo info = new ActorMob.MobInfo();
                info.name = "天兵鱼人";
                info.maxhp = 350000;
                info.speed = 500;
                info.atk_min = 700;
                info.atk_max = 1200;
                info.matk_min = 1;
                info.matk_max = 1;
                info.def = 10;
                info.def_add = 10;
                info.mdef = 10;
                info.mdef_add = 10;
                info.hit_critical = 23;
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
                info.elements[SagaLib.Elements.Fire] = 0;
                info.elements[SagaLib.Elements.Water] = 0;
                info.elements[SagaLib.Elements.Wind] = 0;
                info.elements[SagaLib.Elements.Earth] = 0;
                info.elements[SagaLib.Elements.Holy] = 0;
                info.elements[SagaLib.Elements.Dark] = 30;
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

                return info;
            }

            AIMode 天兵鱼人ai()
            {
                AIMode ai = new AIMode(1);
                ai.MobID = 10560000;//怪物ID
                ai.isNewAI = true;//使用的是TT AI
                ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
                ai.ShortCD = 7;//進程技能表最短釋放間隔，3秒一次
                ai.LongCD = 7;//遠程技能表最短釋放間隔，3秒一次
                AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

                /*---------鬼之影---------*/
                skillinfo = new AIMode.SkilInfo();
                skillinfo.CD = 5;//技能CD
                skillinfo.Rate = 20;//釋放概率
                skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
                skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
                ai.SkillOfShort.Add(31047, skillinfo);//將這個技能加進進程技能表

                return ai;
            }
            void 死亡Ondie(MobEventHandler e, ActorPC pc)
            {
                if (e.mob != null)
                {
                    ActorMob mob = e.mob;
                    mob.TInt["鱼人加护"] = 0;
                }
                
            }
        }
    }
}
