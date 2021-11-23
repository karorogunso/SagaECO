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
    public class S31140 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            for (int i = 0; i < 6; i++)
            {
                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.Name = "星原之握";
                actor.MapID = sActor.MapID;
                short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 600);
                actor.X = pos[0];
                actor.Y = pos[1];
                actor.Speed = 600;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                actor.Stackable = false;
                Activator timer = new Activator(sActor, actor);
                timer.Activate();
                SkillHandler.Instance.ShowEffectByActor(actor, 7007);
            }
            List<Actor> mobs = map.GetActorsArea(sActor, 3000, false);
            foreach (var item in mobs)
            {
                if (item.type == ActorType.MOB && item.HP > 0)
                {
                    ActorMob mob = (ActorMob)item;
                    ActorEventHandlers.MobEventHandler e = (ActorEventHandlers.MobEventHandler)mob.e;
                    e.AI.Master = sActor;
                    SkillHandler.Instance.ShowEffectOnActor(mob, 5181);
                }
            }
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
                period = SagaLib.Global.Random.Next(150,200);
                this.skill = skill;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    //被激活星河急涌时
                    if (skill.TInt["星河急涌"] == 1)
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
                        if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID && dActor != null && dActor.HP > 0)//dActor == null时意味着没有目标，也爆炸
                        {
                            List<Actor> actors = map.GetActorsArea(skill, 100, false);
                            List<Actor> affected = new List<Actor>();
                            foreach (var item in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                    affected.Add(item);
                            }
                            if (affected.Count > 0 || dActor == null || count == maxcount)
                            {
                                SkillHandler.Instance.ShowEffectByActor(skill, 4433);
                                actors = map.GetActorsArea(skill, 200, false);
                                affected = new List<Actor>();
                                foreach (var item in actors)
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                        affected.Add(item);
                                }
                                foreach (Actor j in actors)
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                                    {
                                        int damage = SkillHandler.Instance.CalcDamage(true, caster, j, null, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 1, 8f);
                                        SkillHandler.Instance.CauseDamage(caster, j, damage);
                                        SkillHandler.Instance.ShowVessel(j, damage);
                                        SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 4321);
                                        if (!j.Status.Additions.ContainsKey("Frosen"))
                                        {
                                            Freeze f = new Freeze(null, j, 7000);
                                            SkillHandler.ApplyAddition(j, f);
                                        }
                                    }
                                }
                                Deactivate();
                                map.DeleteActor(skill);
                            }
                            else if (count % 3 == 0)
                            {
                                Mob.MobAI ai = new MobAI(skill, true);
                                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(skill.X, map.Width), SagaLib.Global.PosY16to8(skill.Y, map.Height),
                                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                                short[] pos = new short[2];
                                pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                                pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                                map.MoveActor(Map.MOVE_TYPE.START, skill, pos, 0, 200);
                                SkillHandler.Instance.ShowEffectOnActor(skill, 5293);
                            }
                        }
                        else
                        {
                            map.DeleteActor(skill);
                            Deactivate();
                        }
                    }
                    //未激活星河急涌时
                    if (skill.TInt["星河急涌"] != 1)
                    {
                        if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID)
                        {
                            //移动前检查可攻击目标并造成伤害
                            List<Actor> actors = map.GetActorsArea(skill, 100, false);
                            List<Actor> affected = new List<Actor>();
                            foreach (var item in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                    affected.Add(item);
                            }
                            if (affected.Count > 0)
                            {
                                foreach (Actor j in actors)
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                                    {
                                        int damage = SkillHandler.Instance.CalcDamage(true, caster, j, null, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 1, 1f);
                                        SkillHandler.Instance.CauseDamage(caster, j, damage);
                                        SkillHandler.Instance.ShowVessel(j, damage);
                                        SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 4321);
                                        if (j.Status.Additions.ContainsKey("星原之握"))
                                        {
                                            j.TInt["星原之握连击"]++;
                                            if (!j.Status.Additions.ContainsKey("Frosen") && j.TInt["星原之握连击"] >8)
                                            {
                                                Freeze f = new Freeze(null, j, 5000);
                                                SkillHandler.ApplyAddition(j, f);
                                            }
                                        }
                                        else
                                        {
                                            j.TInt["星原之握连击"] = 0;
                                            OtherAddition skill = new OtherAddition(null, j, "星原之握", 3000);
                                            SkillHandler.ApplyAddition(j, skill);
                                        }
                                    }
                                }
                            }
                            //随机在BOSS周围移动
                            if (count % 10 == 0)
                            {
                                short[] pos = map.GetRandomPosAroundPos(skill.X, skill.Y, 200);
                                int trycount = 0;
                                while (!SkillHandler.Instance.isInRange(skill, caster, 1000) && trycount < 200)
                                {
                                    trycount++;
                                    pos = map.GetRandomPosAroundPos(skill.X, skill.Y, 200);
                                }
                                if (trycount == 200)
                                {
                                    pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 100);
                                }
                                Mob.MobAI ai = new MobAI(skill, true);
                                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(skill.X, map.Width), SagaLib.Global.PosY16to8(skill.Y, map.Height),
                                    SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                                pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                                pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                                map.MoveActor(Map.MOVE_TYPE.START, skill, pos, 0, 3000);
                            }
                        }
                        else
                        {
                            map.DeleteActor(skill);
                            Deactivate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    map.DeleteActor(skill);
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
        #endregion

    }
}
