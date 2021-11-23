
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Shaman
{
    /// <summary>
    /// 精靈知識（精霊知識）
    /// </summary>
    public class ElementIInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            Knowledge skill = new Knowledge(args.skill, sActor, "ElementIInfo", SagaDB.Mob.MobType.ELEMENT, SagaDB.Mob.MobType.ELEMENT_BOSS_SKILL
               , SagaDB.Mob.MobType.ELEMENT_MATERIAL_NOTOUCH_SKILL, SagaDB.Mob.MobType.ELEMENT_NOTOUCH
               , SagaDB.Mob.MobType.ELEMENT_NOTOUCH_SKILL, SagaDB.Mob.MobType.ELEMENT_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.ELEMENT_SKILL, SagaDB.Mob.MobType.ELEMENT_SKILL_BOSS);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}

