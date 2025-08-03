
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 各種種族傷害增加
    /// </summary>
    public class SomeKindDamUp : ISkill
    {
        List<MobType> SomeKind;
        string name;

        #region Constructers
        public SomeKindDamUp(string PassiveSkillName,List<MobType> kind)
        {
            SomeKind=kind;
            name = PassiveSkillName;
        }
        public SomeKindDamUp(string PassiveSkillName,params MobType[] types)
        {
            name = PassiveSkillName;
            SomeKind=new List<MobType>();
            foreach(var t in types)
            {
                SomeKind.Add(t);
            }
        }
        #endregion

        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, name, active);
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

                                                          