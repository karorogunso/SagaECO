using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 你已經弱了
    /// </summary>
    public class MobStrVitAgiDownOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 30;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "MobStrVitAgiDownOne", rate))
            {
                int lifetime = 24000;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobStrVitAgiDownOne", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //STR
            int str_add = -11;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_str"))
                skill.Variable.Remove("MobStrVitAgiDownOne_str");
            skill.Variable.Add("MobStrVitAgiDownOne_str", str_add);
            actor.Status.str_skill += (short)str_add;

            //AGI
            int agi_add = -18;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_agi"))
                skill.Variable.Remove("MobStrVitAgiDownOne_agi");
            skill.Variable.Add("MobStrVitAgiDownOne_agi", agi_add);
            actor.Status.agi_skill += (short)agi_add;

            //VIT
            int vit_add = -12;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_vit"))
                skill.Variable.Remove("MobStrVitAgiDownOne_vit");
            skill.Variable.Add("MobStrVitAgiDownOne_vit", vit_add);
            actor.Status.vit_skill += (short)vit_add;
   
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //STR
            actor.Status.str_skill -= (short)skill.Variable["MobStrVitAgiDownOne_str"];

            //AGI
            actor.Status.agi_skill -= (short)skill.Variable["MobStrVitAgiDownOne_agi"];

            //VIT
            actor.Status.vit_skill -= (short)skill.Variable["MobStrVitAgiDownOne_vit"];
           
        }
        #endregion
    }
}