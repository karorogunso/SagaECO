using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30003 : ISkill
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
                    map.SendEffect(i, 5051);
                }
            }
            炼狱审判 sc = new 炼狱审判(args.skill, sActor, sActor, 10000, 0, args);
            SkillHandler.ApplyAddition(sActor, sc);
        }

        class 炼狱审判 : DefaultBuff
        {
            int count = 0;
            public 炼狱审判(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "炼狱审判", lifetime, 100, damage, arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                int maxcount = actors.Count;
                if (maxcount > 20) maxcount = 20;
                
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        Actor actor = actors[count];
                        List<Addition> WillBeRemove = new List<Addition>();

                        foreach (KeyValuePair<string, Addition> s in actor.Status.Additions)
                        {
                            Addition addition = (Addition)s.Value;
                            WillBeRemove.Add(addition);
                        }
                        foreach (Addition i in WillBeRemove)
                        {
                            if (i.Activated)
                            {
                                SkillHandler.RemoveAddition(actor, i);
                            }
                        }

                        damage = damage = SkillHandler.Instance.CalcDamage(true, sActor, actor, arg, SkillHandler.DefType.Def, SagaLib.Elements.Fire, 0, 1f);
                        SkillHandler.Instance.CauseDamage(sActor, actor, damage);
                        SkillHandler.Instance.ShowVessel(actor, damage);
                        SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), actor, 5001);

                        Stun stun = new Stun(this.skill, actor, 1500);
                        SkillHandler.ApplyAddition(actor, stun);
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
    }
}
