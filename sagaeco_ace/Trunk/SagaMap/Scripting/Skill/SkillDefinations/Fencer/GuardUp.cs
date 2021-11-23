
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// 提升防禦力（防御力上昇）
    /// </summary>
    public class GuardUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "GuardUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //上昇上限
            int value = 0;
            switch (level)
            {
                case 1:
                    value = 1;
                    break;
                case 2:
                    value =2;
                    break;
                case 3:
                    value = 3;
                    break;
                case 4:
                    value = 4;
                    break;
                case 5:
                    value = 5;
                    break;
            }
            //右防禦
            if (skill.Variable.ContainsKey("GuardUp_def_add"))
                skill.Variable.Remove("GuardUp_def_add");
            skill.Variable.Add("GuardUp_def_add", value);
            actor.Status.mdef_skill += (short)value;
                    
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //右防禦
            actor.Status.mdef_skill -= (short)skill.Variable["GuardUp_def_add"];
                          
        }
        #endregion
    }
}