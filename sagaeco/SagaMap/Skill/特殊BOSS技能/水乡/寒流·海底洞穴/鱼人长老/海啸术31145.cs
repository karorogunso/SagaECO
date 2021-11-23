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
    public class S31145 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            for (int i = 0; i < 5; i++)
            {
                short[] pos;
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2500);

                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.MapID = sActor.MapID;
                actor.X = pos[0];
                actor.Y = pos[1];
                actor.Speed = 500;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                actor.Stackable = false;
                Activator timer = new Activator(sActor, dActor, actor, args ,pos);
                timer.Activate();
            }
        }
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            int countMax = 25, count = 0;
            short[] pos;
            public Activator(Actor caster, Actor dActor, ActorSkill skillActor, SkillArg args, short[] pos)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = SagaLib.Global.Random.Next(300, 450);
                this.dueTime = SagaLib.Global.Random.Next(1000, 2500);
                this.dactor = dActor;
                this.pos = pos;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster,actor, 200);
                    if (dactor == null || count == countMax || (caster.X == actor.X && caster.Y == actor.Y))//临界消除
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                        count = countMax;
                    }
                    else
                    {
                        count++;
                        foreach (Actor j in actors)//伤害
                        {
                            SkillHandler.Instance.DoDamage(true, caster, j, skill, SkillHandler.DefType.MDef, Elements.Water, 0, 8f);
                            SkillHandler.Instance.PushBack(actor, j, 1);
                        }
                        //移动
                        Mob.MobAI ai = new MobAI(actor, true);
                        List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                            SagaLib.Global.PosX16to8(this.caster.X, map.Width), SagaLib.Global.PosY16to8(this.caster.Y, map.Height));
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
                catch(Exception ex)
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
