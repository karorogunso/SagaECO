
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 打傘
    /// </summary>
    public class DefMdefUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DefMdefUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //右防禦
            int def_add_add =10;
            if (skill.Variable.ContainsKey("DefMdefUp_def_add"))
                skill.Variable.Remove("DefMdefUp_def_add");
            skill.Variable.Add("DefMdefUp_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

            //右魔防
            int mdef_add_add = 10;
            if (skill.Variable.ContainsKey("DefMdefUp_mdef_add"))
                skill.Variable.Remove("DefMdefUp_mdef_add");
            skill.Variable.Add("DefMdefUp_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["DefMdefUp_def_add"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["DefMdefUp_mdef_add"];
         
        }
        #endregion
    }
}
