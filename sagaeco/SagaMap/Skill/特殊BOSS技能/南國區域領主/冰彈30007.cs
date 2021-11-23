using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30007 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(30007, 1), sActor);
            byte X = 0;
            byte Y = 0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y, 1);
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(X, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(Y, map.Height);
            actor.Speed = 800;
            actor.e = new ActorEventHandlers.NullEventHandler();
            actor.Name = "NOT_SHOW_DISAPPEAR";
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            ActivatorA timer = new ActivatorA(actor, sActor, dActor, args, level);
            timer.Activate();
        }
        class ActivatorA : MultiRunTask
        {
            ActorSkill Boll;
            Actor sActor;
            Actor dActor;
            Map map;
            SkillArg arg;
            int count = 0;
            public ActivatorA(ActorSkill actor, Actor sActor, Actor dActor, SkillArg args, byte level)
            {
                this.Boll = actor;
                this.sActor = sActor;
                this.dActor = dActor;
                this.arg = args;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 200;
                this.dueTime = 1000;
            }
            public override void CallBack()
            {
                
                try
                {
                    Mob.MobAI ai = new MobAI(Boll, true);
                    List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(Boll.X, map.Width), SagaLib.Global.PosY16to8(Boll.Y, map.Height),
                        SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                    if (path.Count > 1)
                    {
                        int deltaX = path[0].x;
                        int deltaY = path[0].y;
                        if (path.Count == 1)
                        {
                            deltaX = SagaLib.Global.PosX16to8(dActor.X, map.Width);
                            deltaY = SagaLib.Global.PosY16to8(dActor.Y, map.Height);
                        }
                        MapNode node = new MapNode();
                        node.x = (byte)deltaX;
                        node.y = (byte)deltaY;
                        path.Add(node);
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, Boll, pos, 0, 200);
                        //取得当前格子内的Actor
                        List<Actor> actors = new List<Actor>();
                        if (count % 2 == 0)
                        {
                            actors = map.GetActorsArea(Boll, 100, false);
                            foreach (var a in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, a))
                                {
                                    Freeze freeze = new Freeze(arg.skill, a, 3000, 10);
                                    SkillHandler.ApplyAddition(a, freeze);
                                    map.SendEffect(Boll, SagaLib.Global.PosX16to8(Boll.X,map.Width),SagaLib.Global.PosY16to8(Boll.Y,map.Height),4093);
                                }
                            }
                        }
                    }
                    else
                    {
                        List<Actor> actors = new List<Actor>();
                        actors = map.GetActorsArea(Boll, 400, false);
                        Skill.SkillHandler.Instance.ShowEffect(map, dActor, 8006);
                        foreach (var a in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, a))
                            {
                                int damaga = SkillHandler.Instance.CalcDamage(false, sActor, a, arg, SkillHandler.DefType.MDef, Elements.Water, 50, 6f);
                                SkillHandler.Instance.CauseDamage(sActor, a, damaga);
                                SkillHandler.Instance.ShowVessel(a, damaga);
                                鈍足 s = new 鈍足(arg.skill, a, 6000);
                                SkillHandler.ApplyAddition(a, s);
                                Skill.SkillHandler.Instance.ShowEffect(map, a, 5050);
                            }
                        }
                        Freeze freeze = new Freeze(arg.skill, dActor, 6000);
                        SkillHandler.ApplyAddition(dActor, freeze);
                        this.Deactivate();
                        map.DeleteActor(Boll);
                        return;
                    }
                    count++;
                }
                catch (Exception ex)
                { Logger.ShowError(ex); }
            }
        }
    }
}
