using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 空中迴旋腿
    /// </summary>
    public class SummerSaltKick : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = 0.75f + 0.25f * level;
            ActorPC actorPC = (ActorPC)sActor;
            if (level == 6)
            {
                factor = 3f;
                List<Actor> target = new List<Actor>();
                for (int i = 0; i < 3; i++)
                {
                    target.Add(dActor);
                }
                SkillHandler.Instance.PushBack(sActor, dActor, 7);
                Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, skill);
                SkillHandler.Instance.PhysicalAttack(sActor, target, args, sActor.WeaponElement, factor);

            }
            else
            {
                SkillHandler.Instance.PushBack(sActor, dActor, 3);
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            }
        }
        #endregion
    }
}
