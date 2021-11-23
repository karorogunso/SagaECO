using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 3連發精密射擊（バーストショット）
    /// </summary>
    public class BurstShot : ISkill 
    { 
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
                return 0;
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.8f + 0.2f * level;
            int rate = 5 * level;
            args.argType = SkillArg.ArgType.Attack;
            List<Actor> affected = new List<Actor>();
            for (int i = 0; i < 3; i++)
            {
                affected.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "BurstShot", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "BurstShot", 15000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //近命中
            actor.Status.hit_melee_skill = (short)(-20);

            //遠命中
            actor.Status.hit_ranged_skill = (short)(-20);
 
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)(-20);

            //遠命中
            actor.Status.hit_ranged_skill -= (short)(-20);
                   
        }
        #endregion
    }
}
