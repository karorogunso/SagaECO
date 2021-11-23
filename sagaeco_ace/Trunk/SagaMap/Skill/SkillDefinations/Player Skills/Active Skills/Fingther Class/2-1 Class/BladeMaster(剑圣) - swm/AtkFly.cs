
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
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
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
            float factor = 1.4f + 0.2f * level;
            int times = 1;
            if (dActor.type == ActorType.MOB)
            {
                if(SkillHandler.Instance.isBossMob(dActor))
                    times =2;
            }
            if (level == 6)
            {
                times = 3;
                factor = 3f;
            }
            List<Actor> Affected = new List<Actor>();
            for (int i = 0; i < times; i++)
            {
                Affected.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, Affected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}