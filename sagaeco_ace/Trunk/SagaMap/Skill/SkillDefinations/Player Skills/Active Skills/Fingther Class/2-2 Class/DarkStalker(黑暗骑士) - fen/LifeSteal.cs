using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 迷魂吸血（ライフスティール）
    /// </summary>
    public class LifeSteal : ISkill 
    {
        bool MobUse;
        public LifeSteal()
        {
            this.MobUse = false;
        }
        public LifeSteal(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor = 1.0f + 0.2f * level;

            args.type = ATTACK_TYPE.BLOW;
            
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            uint hp_recovery=0;
            foreach (int hp in args.hp)
            {
                hp_recovery += (uint)(hp * 0.8f);
            }
            SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Holy, -hp_recovery);
        }
        #endregion
    }
}
