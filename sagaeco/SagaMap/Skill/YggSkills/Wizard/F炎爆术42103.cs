using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S42103 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int count = 3;
            if (sActor.type == ActorType.GOLEM)
                count = 35;
            if(sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).Name == "羽川柠")
                    count = 100;
            }
            sActor.EP += 500;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            Activator timer = new Activator(sActor, dActor, args, count);
            timer.Activate();

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            Map map;
            int countMax = 3, count = 0;
            public Activator(Actor caster, Actor dActor, SkillArg args,int count)
            {
                this.period = 50;
                this.dueTime = 100;
                this.caster = caster;
                this.dActor = dActor;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.countMax = count;
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(7700, 1), caster);
                        byte X = 0;
                        byte Y = 0;
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        SkillHandler.Instance.GetTRoundPos(map, caster, out X, out Y, 1);
                        actor.MapID = caster.MapID;
                        actor.X = SagaLib.Global.PosX8to16(X, map.Width);
                        actor.Y = SagaLib.Global.PosY8to16(Y, map.Height);
                        actor.Speed = 800;
                        actor.e = new ActorEventHandlers.NullEventHandler();
                        actor.Name = "NOT_SHOW_DISAPPEAR";
                        map.RegisterActor(actor);
                        actor.invisble = false;
                        map.OnActorVisibilityChange(actor);
                        ActivatorA timer = new ActivatorA(actor, caster, dActor, skill);
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
                //ClientManager.LeaveCriticalArea();
            }
        }
    }

    class ActivatorA : MultiRunTask
    {
        ActorSkill Boll;
        Actor sActor;
        Actor dActor;
        Map map;
        SkillArg arg;
        float factor =2.6f;

        public ActivatorA(ActorSkill actor, Actor sActor, Actor dActor, SkillArg args)
        {
            this.Boll = actor;
            this.sActor = sActor;
            this.dActor = dActor;
            this.arg = args;
            map = Manager.MapManager.Instance.GetMap(actor.MapID);
            this.period = 200;
            this.dueTime = 230;

        }
        public override void CallBack()
        {
            try
            {
                Mob.MobAI ai = new MobAI(Boll, true);
                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(Boll.X, map.Width), SagaLib.Global.PosY16to8(Boll.Y, map.Height),
                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                List<Actor> actors = map.GetActorsArea(Boll, 100, false);
                List<Actor> targets = new List<Actor>();
                foreach (var item in actors)
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        targets.Add(item);
                if (path.Count > 1 || targets.Count < 1)
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
                }
                else
                {
                    SkillHandler.Instance.ShowEffect(map, dActor, 5000);
                    SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                    foreach (var item in targets)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        {
                            int damaga = SkillHandler.Instance.CalcDamage(false, sActor, item, arg, SkillHandler.DefType.MDef, Elements.Fire, 50, factor, out res);
                            SkillHandler.Instance.CauseDamage(sActor, item, damaga);
                            SkillHandler.Instance.ShowVessel(item, damaga, 0, 0, res);
                        }
                    }

                    this.Deactivate();
                    map.DeleteActor(Boll);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.ShowError(ex);
                this.Deactivate();
                map.DeleteActor(Boll);
                return;
            }
        }
    }
}
