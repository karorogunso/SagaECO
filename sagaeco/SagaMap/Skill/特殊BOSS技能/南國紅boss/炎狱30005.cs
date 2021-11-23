using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30005 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public Map map;
        public static List<Actor> actors;
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            actors = new List<Actor>();
            map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actor2 = map.GetActorsArea(sActor, 1300, false);
            if (actor2.Count == 0) return;
            foreach (Actor i in actor2)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    actors.Add(i);
                }
            }
            if (actors.Count == 0) return;
            int ran = SagaLib.Global.Random.Next(0, actors.Count-1);
            Actor tactor = actors[ran];

            Stun stun = new Stun(args.skill, tactor, 5000);
            SkillHandler.ApplyAddition(tactor, stun);

            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.MapID = tactor.MapID;
            actor.X = tactor.X;
            actor.Y = tactor.Y;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, tactor, actor, args, level);
            timer.Activate();
        }


        private class Activator : MultiRunTask
        {

            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Actor dactor;
            Map map;
            int count = 0;
            byte x, y;
            public Activator(Actor caster, Actor dactor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.dactor = dactor;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                x = SagaLib.Global.PosX16to8(actor.X, map.Width);
                y = SagaLib.Global.PosY16to8(actor.Y, map.Height);
                this.skill.x = x;
                this.skill.y = y;
                this.skill.dActor = 0xffffffff;
                this.period = 1000;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < 10)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 500, false);
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                if (i != dactor)
                                    SkillHandler.Instance.PushBack(actor, i, 2);
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Fire, 0, 2f);
                                SkillHandler.Instance.CauseDamage(caster, i, damage);
                                SkillHandler.Instance.ShowVessel(i, damage);
                                SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(i.MapID), i, 8053);
                            }
                        }

                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
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
    }
}
