
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  狂戰士（バーサーク）
    /// </summary>
    public class P_BERSERK : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lowHP = (int)(sActor.MaxHP * (0.05f + 0.05f * level));
            bool active = false;
            if (sActor.HP <= lowHP)
            {
                active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "P_BERSERK", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (skill.Activated)
            {
                Berserk bs = new Berserk(skill.skill, actor, 30000);
                SkillHandler.ApplyAddition(actor, bs);
            }
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}
