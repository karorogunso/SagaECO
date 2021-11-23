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
    public class S31144 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short X = SagaLib.Global.PosX8to16(args.x, map.Width);
            short Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            List<Actor> actors = map.GetActorsArea(X, Y, 300, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    if (!i.Status.Additions.ContainsKey("Stun"))
                    {
                        Stun stun = new Stun(null, i, 3000);
                        SkillHandler.ApplyAddition(i, stun);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, Elements.Earth, 3f);

            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.Name = "雷霆万钧";
            actor.MapID = sActor.MapID;
            actor.X = X;
            actor.Y = Y;
            actor.Speed = 600;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, actor);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Map map;
            Actor caster;
            ActorSkill skill;
            Actor dActor;
            int count = 0;
            int maxcount = 10000;
            public Activator(Actor sactor, ActorSkill skill)
            {
                caster = sactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 2000;
                period = 200;
                this.skill = skill;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count < maxcount)
                    {
                        //获取目标
                        if (dActor == null)
                        {
                            List<Actor> actors = map.GetActorsArea(skill, 2000, false);
                            List<Actor> affected = new List<Actor>();
                            foreach (var item in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                    affected.Add(item);
                            }
                            if (affected.Count > 0)
                                dActor = affected[SagaLib.Global.Random.Next(0, affected.Count - 1)];
                            else dActor = null;
                        }
                        //技能释放
                        if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID && dActor!=null &&dActor.HP > 0)
                        {
                            if (count % 11 == 0)
                            {
                                List<Actor> actors = map.GetActorsArea(skill, 300, false);
                                List<Actor> affected = new List<Actor>();
                                SkillHandler.Instance.ShowEffectByActor(skill, 4137);
                                bool KillSelf = false;
                                foreach (var item in actors)
                                {
                                    if (item.type == ActorType.SKILL && item.Name == "深蓝领域")
                                        KillSelf = true;
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                        affected.Add(item);
                                }

                                if(KillSelf)
                                {
                                    Deactivate();
                                    map.DeleteActor(skill);
                                    SkillHandler.Instance.ShowEffectOnActor(caster, 5003);
                                    int damage = (int)(caster.MaxHP * 0.1f);
                                    SkillHandler.Instance.CauseDamage(caster, caster, damage);
                                    SkillHandler.Instance.ShowVessel(caster, damage);
                                    Stun stun = new Stun(null, caster, 20000);
                                    SkillHandler.ApplyAddition(caster, stun);
                                }

                                if (affected.Count > 0)
                                {
                                    float factor = 8f;
                                    if (KillSelf)
                                        factor = 8f;

                                    actors = map.GetActorsArea(skill, 200, false);
                                    affected = new List<Actor>();
                                    foreach (var item in actors)
                                    {
                                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                        {
                                            affected.Add(item);
                                            if(KillSelf)
                                                SkillHandler.Instance.ShowEffectOnActor(item, 5003);
                                            if(item.type ==  ActorType.MOB && item != caster)
                                            {
                                                if(!item.Status.Additions.ContainsKey("Stun"))
                                                {
                                                    Stun skill = new Stun(null, item, 5000);
                                                    SkillHandler.ApplyAddition(item, skill);
                                                }
                                            }
                                        }
                                    }
                                    foreach (Actor j in actors)
                                    {
                                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                                        {
                                            int damage = SkillHandler.Instance.CalcDamage(true, caster, j, null, SkillHandler.DefType.MDef, Elements.Wind, 1, factor);
                                            SkillHandler.Instance.CauseDamage(caster, j, damage);
                                            SkillHandler.Instance.ShowVessel(j, damage);
                                            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 5165);
                                        }
                                    }
                                }
                            }
                            if (count % 3 == 0)
                            {
                                Mob.MobAI ai = new MobAI(skill, true);
                                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(skill.X, map.Width), SagaLib.Global.PosY16to8(skill.Y, map.Height),
                                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                                short[] pos = new short[2];
                                pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                                pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                                map.MoveActor(Map.MOVE_TYPE.START, skill, pos, 0, 200);
                            }
                        }
                        else
                        {
                            Deactivate();
                            map.DeleteActor(skill);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Deactivate();
                    map.DeleteActor(skill);
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
