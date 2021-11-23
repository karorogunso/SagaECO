using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.X
{
    class BlackHoleOfPP : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 900, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            foreach (Actor a in realAffected)
            {
                short[] pos = new short[2] { SagaLib.Global.PosX8to16(args.x,map.Width), SagaLib.Global.PosY8to16(args.y,map.Height) };
                map.MoveActor(Map.MOVE_TYPE.START, a, pos, 1000, 1000, true, MoveType.QUICKEN);
                Skill.Additions.Global.鈍足 钝足 = new 鈍足(args.skill, a, 4000);
                SkillHandler.ApplyAddition(a, 钝足);
            }

            ActorSkill actor = new ActorSkill(args.skill, sActor);
            map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 3.0f;
            int countMax = 5, count = 0;
            byte x, y;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 0;
                x = args.x;
                y = args.y;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        count++;
                    }
                    else
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Dark, factor);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        SkillHandler.Instance.ShowEffect(map,actor, x, y, 5300);

                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
