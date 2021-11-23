
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 飛燕劍（燕返し）
    /// </summary>
    public class AtkFly : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
                {
                    return 0;
                }
            }
            else
            {
                return -14;
            }
            return -14;
            
        }
        Actor sActor;
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.sActor = sActor;
            Skill.Additions.Global.DefaultBuff 燕返 = new Additions.Global.DefaultBuff(args.skill, sActor, dActor, "燕返式", 5000, 0, 0, args);
            燕返.OnAdditionStart += 燕返_OnAdditionStart;
            燕返.OnAdditionEnd += 燕返_OnAdditionEnd;
            SkillHandler.ApplyAddition(dActor, 燕返);
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Holy, 1.2f + 0.3f * level);
        }
        void 燕返_OnAdditionStart(Actor actor, Additions.Global.DefaultBuff skill)
        {
            //SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID), actor, 5204);
            skill.Variable["燕返"] = 0;
        }
        void 燕返_OnAdditionEnd(Actor actor, Additions.Global.DefaultBuff skill)
        {
            int damage = (int)(skill.Variable["燕返"] * (0.25f + 0.05f * skill.skill.Level));
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                SkillHandler.Instance.CauseDamage(sActor, actor, damage);
                SkillHandler.Instance.ShowVessel(actor, damage);
                SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID), actor, 8057);
            }
           skill.Variable["燕返"] = 0;
        }
        #endregion
    }
}