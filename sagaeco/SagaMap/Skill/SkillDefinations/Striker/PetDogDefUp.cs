
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 野獸加護（グリッターボディ）[接續技能]
    /// </summary>
    public class PetDogDefUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 - 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "PetDogDefUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //左防禦
            int def_add = (int)(4 + level);
            if (skill.Variable.ContainsKey("DogHpUp_def"))
                skill.Variable.Remove("DogHpUp_def");
            skill.Variable.Add("DogHpUp_def", def_add);
            actor.Status.def_skill += (short)def_add;

            //右防禦
            int def_add_add = (int)( 8 + 2 * level);
            if (skill.Variable.ContainsKey("DogHpUp_def_add"))
                skill.Variable.Remove("DogHpUp_def_add");
            skill.Variable.Add("DogHpUp_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

            //左魔防
            int mdef_add = (int)(4 + 2 * level);
            if (skill.Variable.ContainsKey("DogHpUp_mdef"))
                skill.Variable.Remove("DogHpUp_mdef");
            skill.Variable.Add("DogHpUp_mdef", mdef_add);
            actor.Status.mdef_skill += (short)mdef_add;

            //右魔防
            int mdef_add_add = (int)( 5 + 2 * level);
            if (skill.Variable.ContainsKey("DogHpUp_mdef_add"))
                skill.Variable.Remove("DogHpUp_mdef_add");
            skill.Variable.Add("DogHpUp_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["DogHpUp_def"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["DogHpUp_def_add"];

            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["DogHpUp_mdef"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["DogHpUp_mdef_add"];
             
        }
        #endregion
    }
}
