using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 特攻武術修練（体術マスタリー）
    /// </summary>
    public class MartialArtDamUp : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MartialArtDamUp", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            short add = (short)(10 * level);
            if (skill.Variable.ContainsKey("MartialArtDamUp"))
                skill.Variable.Remove("MartialArtDamUp");
            skill.Variable.Add("MartialArtDamUp", add);
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (skill.Variable.ContainsKey("MartialArtDamUp"))
                skill.Variable.Remove("MartialArtDamUp");
        }
        #endregion
    }
}
