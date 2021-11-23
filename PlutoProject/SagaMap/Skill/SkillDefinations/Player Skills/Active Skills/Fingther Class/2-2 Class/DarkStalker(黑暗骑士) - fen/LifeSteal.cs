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
            int elements;
            //args.type = ATTACK_TYPE.BLOW;
            if (sActor.WeaponElement != SagaLib.Elements.Neutral)
            {
                elements = sActor.Status.attackElements_item[sActor.WeaponElement]
                                    + sActor.Status.attackElements_skill[sActor.WeaponElement]
                                    + sActor.Status.attackelements_iris[sActor.WeaponElement];
            }
            else
            {
                elements = 0;
            }

            int dmg = SkillHandler.Instance.CalcDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, sActor.WeaponElement, elements, factor);
            SkillHandler.Instance.CauseDamage(sActor, dActor, dmg);
            SkillHandler.Instance.ShowVessel(dActor, dmg);
            //SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            //uint hp_recovery=0;
            //foreach (int hp in args.hp)
            //{
            //    hp_recovery += (uint)(hp * 0.8f);
            //}
            int dmgheal = (int)(-(dmg * 0.8f));
            SkillHandler.Instance.CauseDamage(sActor, sActor,dmgheal);
            SkillHandler.Instance.ShowVessel(sActor, dmgheal);

        }
        #endregion
    }
}
