using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    public class DelayOut : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("DelayOut"))
                return -30;
            else
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "DelayOut", 75000 - level * 15000);
            SkillHandler.ApplyAddition(sActor, skill);

            //ÒÆ³ýÓ¢ÐÛ¼Ó»¤
            if (dActor.Status.Additions.ContainsKey("HerosProtection"))
                dActor.Status.Additions.Remove("HerosProtection");

            if (dActor.Status.Additions.ContainsKey("Demacia"))
                dActor.Status.Additions.Remove("Demacia");

            if (dActor.Status.Additions.ContainsKey("ShadowSeam"))
                dActor.Status.Additions.Remove("ShadowSeam");

            if (dActor.Status.Additions.ContainsKey("Sacrifice"))
                dActor.Status.Additions.Remove("Sacrifice");

            if (dActor.Status.Additions.ContainsKey("SpeedHit"))
                dActor.Status.Additions.Remove("SpeedHit");

            if (dActor.Status.Additions.ContainsKey("Teleport"))
                dActor.Status.Additions.Remove("Teleport");

            if (dActor.Status.Additions.ContainsKey("ElementalWrath"))
                dActor.Status.Additions.Remove("ElementalWrath");

        }
    }
}
