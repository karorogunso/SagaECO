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
    public class S30022 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args, level);
            timer.Activate();
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 105, count = 0;
            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 80;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if(count == 10)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "感受本小姐魔力的强大吧...出来吧，雷冰球！");
                    }
                    if (count < countMax)
                    {
                        ActorSkill actor = new ActorSkill(this.skill.skill, caster);
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        actor.MapID = caster.MapID;
                        short[] pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 3000);
                        actor.X = pos[0];
                        actor.Y = pos[1];
                        actor.Speed = 1400;
                        actor.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(actor);
                        actor.invisble = false;
                        map.OnActorVisibilityChange(actor);
                        actor.Stackable = false;
                        Activator2 timer = new Activator2(caster, actor, this.skill);
                        timer.Activate();
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            int countMax = 5, count = 0;
            public Activator2(Actor caster, ActorSkill skillActor, SkillArg args)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 140;
                this.dueTime = 3000;
                List<Actor> actors = map.GetActorsArea(actor, 2000, false);
                List<Actor> affected = new List<Actor>();
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        affected.Add(item);
                }
                if (affected.Count > 0)
                    this.dactor = affected[SagaLib.Global.Random.Next(0, affected.Count - 1)];
                else this.dactor = null;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    List<Actor> actors = map.GetActorsArea(actor, 100, false);
                    List<Actor> affected = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            affected.Add(item);
                    }
                    if (affected.Count > 0 || dactor == null || count == countMax)
                    {
                        byte x = SagaLib.Global.PosX16to8(actor.X, map.Width);
                        byte y = SagaLib.Global.PosY16to8(actor.Y, map.Height);
                        SkillArg s = this.skill.Clone();
                        s.x = x;
                        s.y = y;
                        if(actors.Count > 0)
                        {
                            EffectArg arg = new EffectArg();
                            arg.effectID = 4433;
                            arg.actorID = 0xFFFFFFFF;
                            arg.x = s.x;
                            arg.y = s.y;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
                        }
                        List<Actor> affected2 = new List<Actor>();
                        foreach (Actor j in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                            {
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, j, skill, SkillHandler.DefType.MDef, Elements.Dark, 1, 8f);
                                SkillHandler.Instance.CauseDamage(caster, j, damage);
                                SkillHandler.Instance.ShowVessel(j, damage);
                                SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 4321);
                                Freeze f = new Freeze(s.skill, j, 7000);
                                SkillHandler.ApplyBuffAutoRenew(j, f);
                            }
                        }
                        this.Deactivate();
                        map.DeleteActor(actor);
                        count = countMax;
                    }
                    else
                    {
                        MobAI ai = new MobAI(actor, true);
                        List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                            SagaLib.Global.PosX16to8(dactor.X, map.Width), SagaLib.Global.PosY16to8(dactor.Y, map.Height));
                        if (path.Count >= 1)
                        {
                            int deltaX = path[0].x;
                            int deltaY = path[0].y;
                            MapNode node = new MapNode();
                            node.x = (byte)deltaX;
                            node.y = (byte)deltaY;
                            path.Add(node);
                            short[] pos = new short[2];
                            pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                            map.MoveActor(Map.MOVE_TYPE.START, actor, pos, 0, 200);
                        }

                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}
