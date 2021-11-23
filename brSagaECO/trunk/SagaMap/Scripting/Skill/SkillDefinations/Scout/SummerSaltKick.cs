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
    public class SummerSaltKick:ISkill
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
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if (((args.flag[0] & SagaLib.AttackFlag.HP_DAMAGE) != 0))
            {
                SkillHandler.Instance.PushBack(sActor, dActor, 3);
                Additions.Global.硬直 skill = new SagaMap.Skill.Additions.Global.硬直(args.skill, dActor, 2000);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        #endregion
    }
}
