using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 沖天踢（タックル）
    /// </summary>
    public class Tackle:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = { 0f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f };
            uint MartialArtDamUp_SkillID = 125;
            ActorPC actorPC = (ActorPC )sActor;
            if (actorPC.Skills2.ContainsKey(MartialArtDamUp_SkillID))
            {
                if (actorPC.Skills2[MartialArtDamUp_SkillID].Level == 3)
                {
                    factors[1]=1.82f;
                    factors[2]=1.94f;
                    factors[3]=2.08f;
                    factors[4]=2.20f;
                    factors[5]=2.34f;
                }
            }
            if (actorPC.SkillsReserve .ContainsKey(MartialArtDamUp_SkillID))
            {
                if (actorPC.SkillsReserve[MartialArtDamUp_SkillID].Level == 3)
                {
                    factors[1] = 1.82f;
                    factors[2] = 1.94f;
                    factors[3] = 2.08f;
                    factors[4] = 2.20f;
                    factors[5] = 2.34f;
                }
            }
            float factor =factors[level];
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            SkillHandler.Instance.PushBack(sActor, dActor, 1+level);
        }       
        #endregion
    }
}
