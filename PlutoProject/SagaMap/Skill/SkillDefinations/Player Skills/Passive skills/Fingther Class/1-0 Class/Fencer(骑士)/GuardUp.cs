
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
            
            int def_add_arm = new int[] { 0, 2, 5, 7, 9, 12 }[skill.skill.Level];
            int def_add_arm_end = (int)(actor.Status.def_add_item * (float)(def_add_arm / 100.0f));
            if (skill.Variable.ContainsKey("GuardUp_arm"))
                skill.Variable.Remove("GuardUp_arm");
            skill.Variable.Add("GuardUp_arm", def_add_arm_end);
            actor.Status.def_add_skill += (short)def_add_arm_end;

            int def_add = new int[] { 0, 2, 4, 6, 8, 10 }[skill.skill.Level];
            if (skill.Variable.ContainsKey("GuardUp_num"))
                skill.Variable.Remove("GuardUp_num");
            skill.Variable.Add("GuardUp_num", def_add);
            actor.Status.def_add_skill += (short)def_add;
            //actor.Status.MagicRuduceRate = 0.02f * skill.skill.Level;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.def_add_skill -= (short)skill.Variable["GuardUp_arm"];
            actor.Status.def_add_skill -= (short)skill.Variable["GuardUp_num"];
            
            //actor.Status.MagicRuduceRate = 0;
        }
        #endregion
    }
}