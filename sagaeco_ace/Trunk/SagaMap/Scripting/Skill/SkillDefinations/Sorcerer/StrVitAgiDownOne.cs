using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 弱化（ディビリテイト）
    /// </summary>
    public class StrVitAgiDownOne:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 10 + 10 * level;
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "StrVitAgiDownOne", 30000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;

            //STR
            int str_add = -(5 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_str"))
                skill.Variable.Remove("StrVitAgiDownOne_str");
            skill.Variable.Add("StrVitAgiDownOne_str", str_add);
            actor.Status.str_skill = (short)str_add;

            //AGI
            int agi_add =  -(8 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_agi"))
                skill.Variable.Remove("StrVitAgiDownOne_agi");
            skill.Variable.Add("StrVitAgiDownOne_agi", agi_add);
            actor.Status.agi_skill = (short)agi_add;

            //VIT
            int vit_add = -(7 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_vit"))
                skill.Variable.Remove("StrVitAgiDownOne_vit");
            skill.Variable.Add("StrVitAgiDownOne_vit", vit_add);
            actor.Status.vit_skill = (short)vit_add;
     

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //STR
            actor.Status.str_skill -= (short)skill.Variable["StrVitAgiDownOne_str"];

            //AGI
            actor.Status.agi_skill -= (short)skill.Variable["StrVitAgiDownOne_agi"];

            //VIT
            actor.Status.vit_skill -= (short)skill.Variable["StrVitAgiDownOne_vit"];
           
        }
        #endregion
    }
}
