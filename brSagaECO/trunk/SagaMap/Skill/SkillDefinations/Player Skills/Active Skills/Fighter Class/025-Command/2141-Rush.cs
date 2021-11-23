using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 極速連擊（ラッシュ）
    /// </summary>
    public class Rush:ISkill 
    {
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
            float factor = 0.72f + 0.6f * level;
            uint MartialArtDamUp_SkillID = 125;
            ActorPC actorPC = (ActorPC)sActor;
            if (actorPC.Skills2.ContainsKey(MartialArtDamUp_SkillID))
            {
                if (actorPC.Skills2[MartialArtDamUp_SkillID].Level == 3)
                {
                    factor = 0.98f + 0.32f * level;
                }
            }
            if (actorPC.SkillsReserve.ContainsKey(MartialArtDamUp_SkillID))
            {
                if (actorPC.SkillsReserve[MartialArtDamUp_SkillID].Level == 3)
                {
                    factor = 0.98f + 0.32f * level;
                }
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 4; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
        }
         #endregion
    }
}
