
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    public class LionPower : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPet p = SkillHandler.Instance.GetPet(sActor);
            if (p != null)
            {
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, p, "LionPower", true);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大HP10%上昇 
            int hit_melee_add = (int)(actor.MaxHP * 0.1f);
            if (skill.Variable.ContainsKey("Encouragement_MaxHP"))
                skill.Variable.Remove("Encouragement_MaxHP");
            skill.Variable.Add("Encouragement_MaxHP", hit_melee_add);
            actor.MaxHP += (uint)hit_melee_add;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["Encouragement_MaxHP"];
        }
        #endregion
    }
}

