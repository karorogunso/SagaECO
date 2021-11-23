using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaDB.Skill;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class ChainLightning:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Thunder thunder = new Thunder(args.skill, sActor, sActor, 10000, 0);
            SkillHandler.ApplyAddition(sActor, thunder);
        }

        class Thunder : DefaultBuff
        {
            public Thunder(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "Thunder", (int)(lifetime * (1f + Math.Max((dActor.Status.debuffee_bonus / 100), -0.9f))), 500, damage)
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
            Actor LastHit = null;
            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> actors = map.GetActorsArea(sactor, 500, false);
                Actor target = null;
                if (actors.Count > 0)
                {
                    bool get = false;
                    while (!get)
                    {
                        int i = actors.Count;
                        int index = SagaLib.Global.Random.Next(0, i - 1);
                        target = actors[index];
                        if (target != LastHit)
                        {
                            LastHit = null;
                            get = true;
                        }
                        else if (i == 1)
                        {
                            LastHit = null;
                            target = null;
                            get = true;
                        }
                    }
                    float factor = 1.2f + 0.3f * skill.skill.Level;
                    if (target != null)
                    {
                        int damage1 = SagaMap.Skill.SkillHandler.Instance.CalcDamage(false, sActor, target, null, SkillHandler.DefType.MDef, Elements.Wind, 50, factor);
                        if (target.HP > 0 && !target.Buff.Dead)
                        {
                            SkillHandler.Instance.CauseDamage(sActor, target, damage1);
                            SkillHandler.Instance.ShowVessel(target, damage1);
                            SkillHandler.Instance.ShowEffect(map, target, 5601);
                        }
                    }
                    LastHit = target;
                }
            }
        }
        #endregion
    }
}
