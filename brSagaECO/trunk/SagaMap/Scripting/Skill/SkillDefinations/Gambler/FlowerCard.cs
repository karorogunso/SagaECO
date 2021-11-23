
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 猪鹿蝶（猪鹿蝶）
    /// </summary>
    public class FlowerCard : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.CARD) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
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
            float factor = 1.0f + 0.5f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2440, level, 1000));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2441, level, 1000));
        }
        #endregion

    }
}



