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
    public class S31067 : ISkill
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
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5399);
            硬直 y = new 硬直(args.skill, sActor, 12000);
            SkillHandler.ApplyAddition(sActor, y);
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 45, count = 0;
            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 20;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count == 10)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "吾之刀刃，破妄为狱！");
                    }
                    if (count < countMax)
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
                this.period = 90;
                this.dueTime = 0;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        MobAI ai = new MobAI(actor, true);
                        short[] pos1 = map.GetRandomPosAroundPos(caster.X, caster.Y, 600);
                        List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
    SagaLib.Global.PosX16to8(pos1[0], map.Width), SagaLib.Global.PosY16to8(pos1[1], map.Height));
                        int deltaX = path[0].x;
                        int deltaY = path[0].y;
                        MapNode node = new MapNode();
                        node.x = (byte)deltaX;
                        node.y = (byte)deltaY;
                        path.Add(node);
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, actor.Dir, 200);
                        count++;
                    }
                    else
                    {
                        Activator3 timer = new Activator3(caster, actor,skill);
                        timer.Activate();
                        this.Deactivate();
                    }
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
        private class Activator3 : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            bool follow = true;
            int countMax = 50, count = 0;
            List<MapNode> path;
            byte x, y;
            public Activator3(Actor caster, ActorSkill skillActor,SkillArg args)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 220;
                this.dueTime = 2300;

                List<Actor> actors = map.GetActorsArea(actor, 3000, false);
                List<Actor> affected = new List<Actor>();
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        affected.Add(item);
                }
                if (affected.Count > 0)
                    this.dactor = affected[SagaLib.Global.Random.Next(0, affected.Count - 1)];
                else this.dactor = null;

                x = SagaLib.Global.PosX16to8(dactor.X, map.Width);
                y = SagaLib.Global.PosY16to8(dactor.Y, map.Height);
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax && dactor != null)
                    {
                        short[] pos = new short[2];
                        if (follow)
                        {
                            MobAI ai = new MobAI(actor, true);
                            path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                                 SagaLib.Global.PosX16to8(dactor.X, map.Width), SagaLib.Global.PosY16to8(dactor.Y, map.Height));
                            int deltaX = path[0].x;
                            int deltaY = path[0].y;
                            MapNode node = new MapNode();
                            node.x = (byte)deltaX;
                            node.y = (byte)deltaY;
                            path.Add(node);
                            pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                            if (path.Count <= 7 || count >= 30) follow = false;
                        }
                        else
                        {
                            byte x, y;
                            SkillHandler.Instance.GetTFrontPos(map, actor, out x, out y);
                            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
                        }
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, actor.Dir, 200);

                        List<Actor> actors = map.GetActorsArea(actor, 150, false);
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
                                    int damage = (int)j.MaxHP;
                                    SkillHandler.Instance.CauseDamage(caster, j, damage);
                                    SkillHandler.Instance.ShowVessel(j, damage);
                                    SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 8077);
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
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}
