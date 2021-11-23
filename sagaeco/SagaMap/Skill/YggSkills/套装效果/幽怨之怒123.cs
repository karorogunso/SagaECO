using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class RapierMastery : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "幽怨之怒", true);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (skill.skill.Level>=1)
            {
                if (actor.TInt["幽怨之怒暴击"] == 0)
                {
                    actor.TInt["幽怨之怒暴击"] = 20;
                    actor.Status.hit_critical_skill += 20;
                }
            }
            
            if (skill.skill.Level >= 2)
            {
                if (((ActorPC)actor).TInt["幽怨之怒MaxHPUP"] == 0)
                {
                    ((ActorPC)actor).TInt["幽怨之怒MaxHPUP"] = 300;
                    actor.Status.hp_skill += (short)300;
                }
            }
            if (skill.skill.Level >= 3)
                actor.TInt["幽怨之怒破防"] = 15;
            if (skill.skill.Level >= 4)
                actor.TInt["幽怨之怒暴击伤害"] = 30;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.hit_critical_skill -= (short)actor.TInt["幽怨之怒暴击"];
            ((ActorPC)actor).TInt["幽怨之怒暴击"] = 0;
            actor.Status.hp_skill -= (short)actor.TInt["幽怨之怒MaxHPUP"];
            actor.TInt["幽怨之怒MaxHPUP"] = 0;
            actor.TInt["幽怨之怒破防"] = 0;
            actor.TInt["幽怨之怒暴击伤害"] = 0;
        }

        #endregion
    }
}
