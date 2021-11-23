using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 大家安靜點
    /// </summary>
    public class MobTrSilenceAtk : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 20;
            int lifetime = 30000;
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                foreach (Actor act in pc.PossesionedActors)
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
                    {
                        Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
        }
        #endregion
    }
}