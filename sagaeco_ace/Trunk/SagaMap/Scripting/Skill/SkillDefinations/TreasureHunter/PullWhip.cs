
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 草鞭（プルウィップ）
    /// </summary>
    public class PullWhip : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
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
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.1f + 0.3f * level;
            uint CATCH_SkillID = 2337;
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.Skills2.ContainsKey(CATCH_SkillID))
            {
                int CATCH_Level= sActorPC.Skills2[CATCH_SkillID].Level;
                if (CATCH_Level <= 2)
                {
                    factor = 1.15f + 0.35f * level;
                }
                else if (CATCH_Level > 2 && CATCH_Level < 4)
                {
                    factor = 1.35f + 0.35f * level;
                }
                else
                {
                    factor = 1.40f + 0.35f * level;
                }
            }
            if (sActorPC.SkillsReserve.ContainsKey(CATCH_SkillID))
            {
                int CATCH_Level = sActorPC.SkillsReserve[CATCH_SkillID].Level;
                if (CATCH_Level <= 2)
                {
                    factor = 1.15f + 0.35f * level;
                }
                else if (CATCH_Level > 2 && CATCH_Level < 4)
                {
                    factor = 1.35f + 0.35f * level;
                }
                else
                {
                    factor = 1.40f + 0.35f * level;
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if (dActor.type == ActorType.MOB)
            {
                if(!SkillHandler.Instance.isBossMob((ActorMob)dActor))
                {
                    硬直 skill = new 硬直(args.skill, dActor, 1000);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }
        #endregion
    }
}