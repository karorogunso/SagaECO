
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    /// <summary>
    /// 邪靈知識（死霊知識）圣印术
    /// </summary>
    public class UndeadInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill2 = new DefaultPassiveSkill(args.skill, sActor, "Seals", true);
            skill2.OnAdditionEnd += sss;
                skill2.OnAdditionStart +=sss;
            //对指定Actor附加技能效果
            SkillHandler.ApplyAddition(sActor, skill2);

            Knowledge skill = new Knowledge(args.skill, sActor, "UndeadInfo", SagaDB.Mob.MobType.UNDEAD, SagaDB.Mob.MobType.UNDEAD_BOSS
               , SagaDB.Mob.MobType.UNDEAD_BOSS_BOMB_SKILL, SagaDB.Mob.MobType.UNDEAD_BOSS_CHAMP_BOMB_SKILL_NOTPTDROPRANGE
               , SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL, SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL_CHAMP
               , SagaDB.Mob.MobType.UNDEAD_BOSS_SKILL_NOTPTDROPRANGE, SagaDB.Mob.MobType.UNDEAD_NOTOUCH
               , SagaDB.Mob.MobType.UNDEAD_SKILL);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
        void sss(Actor actor, DefaultPassiveSkill skill)
        {
        }
    }
}

