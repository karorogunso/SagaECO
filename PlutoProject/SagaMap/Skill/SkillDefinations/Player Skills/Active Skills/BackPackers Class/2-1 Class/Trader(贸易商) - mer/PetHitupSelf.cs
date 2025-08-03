
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 賞金（チップ）[接續技能]
    /// </summary>
    public class PetHitupSelf : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 - 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PetHitupSelf", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = (int)(actor.Status.hit_melee * 8 * level);
            if (skill.Variable.ContainsKey("PetAtkupSelf_hit_melee"))
                skill.Variable.Remove("PetAtkupSelf_hit_melee");
            skill.Variable.Add("PetAtkupSelf_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["PetAtkupSelf_hit_melee"];

        }
        #endregion
    }
}
