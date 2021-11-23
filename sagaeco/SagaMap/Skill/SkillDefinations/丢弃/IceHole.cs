using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.X
{
    class IceHole : MobISkill
    {
        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            throw new NotImplementedException();
        }
        #region ISkill Members

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
        }
        #endregion
    }
}
