
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    /// バイオロジー（バイオロジー）
    /// </summary>
    public class Biology : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (SkillHandler.Instance.GetPet(sActor) != null)
            {
                active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "Biology", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //提升七種修練技能效果
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}

