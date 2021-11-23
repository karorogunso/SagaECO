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
    public class S31032: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args, level);
            timer.Activate();
            map.Announce("零件太多了就要丢掉。");
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4375);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5029);
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            ActorMob mob;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 35, count = 0;
            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 500;
                this.dueTime = 4000;
                mob = (ActorMob)caster;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count == 3)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "零件太多了就要丢掉。");
                    }
                    if (count == 0 || (count < countMax && mob.TInt["零件数"] > 10))
                    {
                        List<Actor> actors = this.map.GetActorsArea(caster, 3000, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                ActorSkill actor = new ActorSkill(this.skill.skill, caster);
                                Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                                actor.MapID = caster.MapID;
                                actor.Dir = (ushort)(SagaLib.Global.Random.Next(0, 7) * 45);
                                actor.X = caster.X;
                                actor.Y = caster.Y;
                                actor.Speed = 900;
                                actor.e = new ActorEventHandlers.NullEventHandler();
                                map.RegisterActor(actor);
                                actor.invisble = false;
                                map.OnActorVisibilityChange(actor);
                                actor.Stackable = false;
                                Activator3 timer = new Activator3(caster, actor, skill, item);
                                timer.Activate();
                            }
                        }
                        count++;
                        if (mob.TInt["零件数"] > 10)
                            mob.TInt["零件数"] -= 10;
                        SkillHandler.Instance.ShowEffectOnActor(caster, 5097);
                        SkillHandler.Instance.ShowVessel(mob, 0, 10, 0);
                    }
                    else
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "似乎零件不够丢刀片了呢。。");
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
        private class Activator3 : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            bool follow = true;
            int countMax = 25, count = 0;
            public Activator3(Actor caster, ActorSkill skillActor, SkillArg args, Actor dactor)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.dactor = dactor;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 270;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax && dactor != null)
                    {
                        short[] pos = new short[2];
                        if (follow)
                        {
                            MobAI ai = new MobAI(actor, true);
                            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                                SagaLib.Global.PosX16to8(dactor.X, map.Width), SagaLib.Global.PosY16to8(dactor.Y, map.Height));
                            int deltaX = path[0].x;
                            int deltaY = path[0].y;
                            MapNode node = new MapNode();
                            node.x = (byte)deltaX;
                            node.y = (byte)deltaY;
                            path.Add(node);
                            pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                            if (path.Count < 3) follow = false;
                        }
                        else
                        {
                            byte x, y;
                            SkillHandler.Instance.GetTFrontPos(map, actor, out x, out y);
                            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
                        }
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, actor.Dir, 200);

                        List<Actor> actors = map.GetActorsArea(actor, 100, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                affected.Add(item);
                        }
                        if (affected.Count > 0 || dactor != null)
                        {
                            foreach (Actor j in affected)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                                {
                                    int damage = 4000;
                                    SkillHandler.Instance.CauseDamage(caster, j, damage);
                                    SkillHandler.Instance.ShowVessel(j, damage);
                                    SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 5270);
                                    if (caster.type == ActorType.MOB)
                                    {
                                        ActorMob mob = (ActorMob)caster;
                                        mob.TInt["零件数"] += 5;
                                        SkillHandler.Instance.ShowVessel(mob, 0, -mob.TInt["零件数"], 0);
                                    }
                                    零件回收 skill = new 零件回收(null, j, 10000);
                                    SkillHandler.ApplyAddition(j, skill);
                                }
                            }
                        }
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
