using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31066 : ISkill
    {
        //ActorMob 暗鬼;
        //ActorMob 明鬼;
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);


            硬直 yz = new 硬直(args.skill, sActor, 5000);
            SkillHandler.ApplyAddition(sActor, yz);

            ActorMob 暗鬼 = map.SpawnCustomMob(10680900, sActor.MapID, x, y, 5, 1, 0, 暗鬼Info(sActor), 暗鬼AI())[0];
            ((ActorEventHandlers.MobEventHandler)暗鬼.e).AI.Master = sActor;
            if(sActor.type == ActorType.MOB)
            ((ActorEventHandlers.MobEventHandler)暗鬼.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            暗鬼.Owner = sActor;
            sActor.Slave.Add(暗鬼);
            SkillHandler.Instance.ShowEffectByActor(暗鬼, 4111);

            ActorMob 明鬼 = map.SpawnCustomMob(10680300, sActor.MapID, x, y, 5, 1, 0, 明鬼Info(sActor), 明鬼AI())[0];
            ((ActorEventHandlers.MobEventHandler)明鬼.e).AI.Master = sActor;
            if (sActor.type == ActorType.MOB)
                ((ActorEventHandlers.MobEventHandler)明鬼.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            明鬼.Owner = sActor;
            sActor.Slave.Add(明鬼);
            SkillHandler.Instance.ShowEffectByActor(明鬼, 4111);

            Activator timer = new Activator(sActor, args);
            timer.Activate();

            /*MultiRunTask mrt = null;
            if (sActor.Tasks.TryGetValue("鬼魂统御", out mrt))
            {
                mrt.Deactivate();
            }
            sActor.Tasks["鬼魂统御"] = timer;*/
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 22000;
                this.dueTime = 22000;
            }
            public override void CallBack()
            {
                if (caster.HP == 0 || caster.Slave.Count == 0)
                {
                    this.Deactivate();
                    return;
                }
                Deactivate();
                if (caster.Slave.Count > 0)
                {
                    foreach (var item in caster.Slave)
                    {
                        ActorMob mob = (ActorMob)item;
                        if (mob.MobID == 10680900 && mob.HP > 0 && !mob.Buff.Dead)//暗鬼
                        {
                            SkillHandler.Instance.ActorSpeak(mob, "唔噢噢噢——居然让我活了这么久，为了答谢你，请吃我一记重拳！");
                            List<Actor> actors = map.GetActorsArea(mob, 1200, false);
                            foreach (var i in actors)
                            {
                                if(SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damage = (int)(i.MaxHP * 0.95f);
                                    SkillHandler.Instance.CauseDamage(mob, i, damage);
                                    SkillHandler.Instance.ShowVessel(i, damage);
                                    SkillHandler.Instance.ShowEffectOnActor(i, 4151);
                                }
                            }
                            SkillHandler.Instance.ShowEffectOnActor(mob,4086);
                        }
                        if (mob.MobID == 10680300 && mob.HP > 0 && !mob.Buff.Dead)
                        {
                            SkillHandler.Instance.ActorSpeak(mob, "略略略略——！主人，这群家伙活不长了！我已吸收了他们的精气，奉献给您，他们暂时无法治疗了，任您宰割——！");
                            List<Actor> actors = map.GetActorsArea(mob, 1200, false);
                            foreach (var i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    OtherAddition oa = new OtherAddition(skill.skill, i, "Sacrifice", 10000);
                                    SkillHandler.ApplyAddition(i, oa);
                                }
                            }
                            int hpheal = (int)(caster.MaxHP * 0.1f);
                            SkillHandler.Instance.CauseDamage(caster, caster, -hpheal);
                            SkillHandler.Instance.ShowVessel(caster, -hpheal);

                            SkillHandler.Instance.ShowEffectOnActor(mob, 4086);

                        }
                    }
                    this.Deactivate();
                }
                for (int i = 0; i < caster.Slave.Count; i++)
                {
                    Actor act = caster.Slave[i];
                    SkillHandler.Instance.CauseDamage(caster, act, 666666666);
                    SkillHandler.Instance.ShowVessel(act, 666666666);
                }
            }
        }
        ActorMob.MobInfo 暗鬼Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "暗鬼";
            info.maxhp = (uint)(boss.MaxHP * 0.05f);
            info.speed = 500;
            info.atk_min = (ushort)(boss.Status.min_atk1 * 0.3f);
            info.atk_max = (ushort)(boss.Status.max_atk1 * 0.3f);
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

            return info;
        }
        AIMode 暗鬼AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10110100;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;
        }
        ActorMob.MobInfo 明鬼Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "明鬼";
            info.maxhp = (uint)(boss.MaxHP * 0.05f);
            info.speed = 500;
            info.atk_min = (ushort)(boss.Status.min_atk1 * 0.3f);
            info.atk_max = (ushort)(boss.Status.max_atk1 * 0.3f); ;
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

            return info;
        }
        AIMode 明鬼AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10110100;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;
        }
    }
}
