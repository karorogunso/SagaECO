
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 各種屬性魂
    /// </summary>
    public class ElementAddUp : ISkill
    {
        private SagaLib.Elements theElement = SagaLib.Elements.Neutral;
        private string name;
        public ElementAddUp(SagaLib.Elements e, string Name)
        {
            name = Name;
            theElement = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, name , active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        public void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}


