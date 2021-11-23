
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 祭司聖言（ディフィートカース）
    /// </summary>
    public class UndeadMdefDownOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * level;
            int rate = 70 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor,"UndeadMdefDownOne", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "UndeadMdefDownOne", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //左魔防
            int mdef_add = -(int)(actor.Status.mdef_bs * 0.5f);
            if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef"))
                skill.Variable.Remove("UndeadMdefDownOne_mdef");
            skill.Variable.Add("UndeadMdefDownOne_mdef", mdef_add);
            actor.Status.mdef_skill += (short)mdef_add;

            //右魔防
            int mdef_add_add = -(int)(actor.Status.mdef_add_bs * 0.5f);
            if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef_add"))
                skill.Variable.Remove("UndeadMdefDownOne_mdef_add");
            skill.Variable.Add("UndeadMdefDownOne_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef_add"];
           
        }
        #endregion
    }
}
