
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
    public class PetAtkupSelf : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 - 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PetAtkupSelf", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //最大攻擊
            int max_atk1_add = (int)(10 * level);
            if (skill.Variable.ContainsKey("PetAtkupSelf_max_atk1"))
                skill.Variable.Remove("PetAtkupSelf_max_atk1");
            skill.Variable.Add("PetAtkupSelf_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)( 10 * level);
            if (skill.Variable.ContainsKey("PetAtkupSelf_max_atk2"))
                skill.Variable.Remove("PetAtkupSelf_max_atk2");
            skill.Variable.Add("PetAtkupSelf_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(10 * level);
            if (skill.Variable.ContainsKey("PetAtkupSelf_max_atk3"))
                skill.Variable.Remove("PetAtkupSelf_max_atk3");
            skill.Variable.Add("PetAtkupSelf_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["PetAtkupSelf_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["PetAtkupSelf_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["PetAtkupSelf_max_atk3"];
                    
        }
        #endregion

    }
}